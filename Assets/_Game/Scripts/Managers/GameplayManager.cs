using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Numerics;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] private Board _board;
    [SerializeField]
    private List<Block> _actionBlocks = new List<Block>();

    private Line _currentSelectLine;
    private Block _currentPendingBlock;
    private bool _isBlockMoving = false;
    private int _quantityBlock = 0;
    private int _comboCount = 0;
    private ISkillState _currentSkillState;
    private BigInteger _point = 0;
    private BigInteger _maxPoint;
    private int _highestBlock = 0;

    public Action OnReset;
    public Action<BigInteger> OnGetPoint;
    public Action<Line> OnMouseDown;
    public Action<Line> OnMouseEnter;
    public Action<int> OnCombineBlock;
    public Action<int> OnGetCombo;
    public Action OnUseSkill;
    public GameStateEnum CurrentState;

    public int QuantityBlock { get => _quantityBlock; set => _quantityBlock = value; }
    public bool IsBlockMoving { get => _isBlockMoving; set => _isBlockMoving = value; }
    public BigInteger Point
    {
        get => _point;
        set
        {
            _point = value;
            OnGetPoint?.Invoke(_point);
        }
    }
    public Board Board { get => _board; }
    public BigInteger MaxPoint { get => _maxPoint; set => _maxPoint = value; }
    public int HighestBlock { get => _highestBlock; set => _highestBlock = value; }

    private void Awake()
    {
        OnMouseDown += OnLineMouseDown;
        OnMouseEnter += OnLineMouseEnter;
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void OnLineMouseDown(Line line)
    {
        if (_isBlockMoving || CurrentState != GameStateEnum.Playing) return;
        if (_currentSkillState != null)
        {
            UnityEngine.Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(vector3.x);
            int y = Mathf.RoundToInt(vector3.y);

            _currentSkillState.Execute(new Vector2Int(x, y));

            _isBlockMoving = true;
            return;
        }
        _currentPendingBlock = _board.GetNextBlock();
        PendingShoot(line);
    }

    private void OnLineMouseEnter(Line line)
    {
        if (_currentSelectLine == null || _isBlockMoving || CurrentState != GameStateEnum.Playing) return;
        PendingShoot(line);
    }
    private void PendingShoot(Line line)
    {
        _currentSelectLine = line;
        _currentPendingBlock.transform.position = new Vector3Int(line.LineIndex, 0);
        _currentPendingBlock.gameObject.SetActive(line.GroundYCoordinate > 0);
        _board.SetReviewBlockCoor(new Vector3Int(line.LineIndex, _currentSelectLine.GroundYCoordinate), line.GroundYCoordinate > -1);
        _currentPendingBlock.Coordinate = new Vector2Int(line.LineIndex, _currentSelectLine.GroundYCoordinate);
        _currentPendingBlock.CurrentLine = line;

    }
    private void OnLineMouseUp()
    {
        if (_currentSelectLine == null || _isBlockMoving) return;
        if (_currentSelectLine.GroundYCoordinate == -1)
        {
            Block checkBlock = _board.Block_Coor_Dic[new Vector2Int(_currentSelectLine.LineIndex, 0)];
            if (checkBlock.BlockNum.Number == _currentPendingBlock.BlockNum.Number)
            {
                int newNumber = checkBlock.BlockNum.Number + 1;
                if (newNumber > HighestBlock)
                {
                    HighestBlock = newNumber;
                    RuntimeDataManager.Instance.PlayerData.HighestBlockIndex = HighestBlock;
                }
                checkBlock.BlockNum.Number = newNumber;
                checkBlock.SpriteRenderer.color = CacheColor.GetColor(newNumber);
                _currentPendingBlock.ReturnToPool();
                _currentPendingBlock = checkBlock;
                _quantityBlock--;
                _currentPendingBlock.CurrentLine.GroundYCoordinate = 0;
            }
            else
            {
                _currentPendingBlock.ReturnToPool();
                _currentSelectLine = null;
                _currentPendingBlock = null;
                _isBlockMoving = false;
                _board.DisableReviewBlock();
                return;
            }
        }
        _quantityBlock++;
        _board.OnBlockDrop();
        _board.DisableReviewBlock();
        _isBlockMoving = true;

        _currentPendingBlock.gameObject.SetActive(true);
        _currentPendingBlock.Coordinate = new Vector2Int(_currentPendingBlock.Coordinate.x, 0);
        _actionBlocks.Add(_currentPendingBlock);

        _currentSelectLine = null;
        _currentPendingBlock = null;

        BlockDropState();
    }
    private void BlockDropState()
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < _actionBlocks.Count; i++)
        {
            Block block = _actionBlocks[i];
            Vector2Int newCoordinate = new Vector2Int(block.Coordinate.x, block.CurrentLine.GroundYCoordinate);
            Debug.Log(block.Coordinate + " " + newCoordinate);
            if (block.Coordinate.y > newCoordinate.y)
            {
                continue;

            }
            _board.Block_Coor_Dic.Remove(block.Coordinate);
            _board.Block_Coor_Dic.Add(newCoordinate, block);

            block.Coordinate = newCoordinate;
            block.CurrentLine.GroundYCoordinate--;

            sequence.Join(block.MoveYTo(newCoordinate.y));
        }
        sequence.OnComplete(() =>
        {
            BlockCombineState();
        });
    }
    private void BlockCombineState()
    {
        HashSet<Block> pendingBlocks = new HashSet<Block>();
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < _actionBlocks.Count; i++)
        {
            //bool hasTopBlock = false;
            List<Block> combineBlocks = FindSimilarBlockAround(_actionBlocks[i]);

            Debug.Log("Combine block: " + combineBlocks.Count);
            // No similar block around then remove this block and continue
            if (combineBlocks.Count == 0)
            {
                SoundManager.Instance.PlaySFX(SFXType.Shoot);
                _actionBlocks.RemoveAt(i);
                i--;
                continue;
            }
            SoundManager.Instance.PlaySFX(SFXType.Merge);
            _comboCount++;

            // Add this block to combine list
            if (combineBlocks[0].Coordinate.y == _actionBlocks[i].Coordinate.y)
            {
                combineBlocks.Insert(0, _actionBlocks[i]);
            }
            else
            {
                //hasTopBlock = true;
                combineBlocks.Add(_actionBlocks[i]);
            }

            // Find the block that has the most similar blocks around
            int maxValue = 0;
            Block maxBlock = null;
            List<Block> maxCombineBlockRelative = new List<Block>();

            for (int j = 0; j < combineBlocks.Count; j++)
            {
                List<Block> temp = FindSimilarBlockAround(combineBlocks[j]);
                if (temp.Count > maxValue)
                {
                    maxValue = temp.Count;
                    maxBlock = combineBlocks[j];
                    maxCombineBlockRelative = temp;
                }
            }
            Debug.Log($"Max combine block is {1 << maxBlock.BlockNum.Number} with coor: {maxBlock.Coordinate}");
            Debug.Log($"Current combine block is {1 << _actionBlocks[i].BlockNum.Number} with coor: {_actionBlocks[i].Coordinate}");

            int newNumber = maxBlock.BlockNum.Number + maxValue;
            if (newNumber > HighestBlock)
            {
                HighestBlock = newNumber;
                RuntimeDataManager.Instance.PlayerData.HighestBlockIndex = HighestBlock;
            }
            Point += BigInteger.Pow(2, newNumber);
            if (Point > _maxPoint)
            {
                MaxPoint = Point;
                RuntimeDataManager.Instance.PlayerData.HighScore = MaxPoint.ToString();
            }
            //maxBlock.BlockNum.Number += maxValue;
            sequence.Join(maxBlock.ChangeColorTo(newNumber, true));

            _board.GetPointCounter().ShowPoint(newNumber, maxBlock.transform.position);

            // Setup combine sequence
            foreach (var item in maxCombineBlockRelative)
            {
                // Add drop block to pending list
                for (int k = item.Coordinate.y - 1; k >= 0; k--)
                {
                    Block block = null;
                    if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(item.Coordinate.x, k), out block))
                    {
                        Debug.Log($"Add drop block {1 << block.BlockNum.Number} with coor: {block.Coordinate}");
                        if (block == maxBlock)
                        {
                            //break;
                            continue;
                        }
                        pendingBlocks.Add(block);
                    }
                    else
                    {
                        break;
                    }
                }

                // Remove block from board info
                if (pendingBlocks.Contains(item))
                {
                    pendingBlocks.Remove(item);
                }
                else
                    item.CurrentLine.GroundYCoordinate = item.Coordinate.y;
                item.CurrentLine = maxBlock.CurrentLine;

                Debug.Log($"Remove block {1 << item.BlockNum.Number} with coor: {item.Coordinate}");
                _board.Block_Coor_Dic.Remove(item.Coordinate);
                _quantityBlock--;

                // Remove block from action list
                if (_actionBlocks.Contains(item) && item != _actionBlocks[i])
                {
                    int index = _actionBlocks.IndexOf(item);
                    if (index < i)
                    {
                        i--;
                    }
                    _actionBlocks.Remove(item);
                }

                // Combine block
                sequence.Join(item.ChangeColorTo(newNumber, false));
                sequence.Join(item.MoveTo(maxBlock.Coordinate));
            }

            // if current action block is not the max block then replace it
            if (maxBlock != _actionBlocks[i])
            {
                _actionBlocks[i] = maxBlock;
            }

        }
        sequence.OnComplete(() =>
        {
            Debug.Log("Combine complete: " + _actionBlocks.Count);
            _actionBlocks.AddRange(pendingBlocks);
            if (_actionBlocks.Count > 0)
            {
                BlockDropState();
            }
            else
            {
                Debug.Log(_comboCount);
                if (_comboCount > 2)
                {
                    SoundManager.Instance.VibrateDevice();
                    RuntimeDataManager.Instance.PlayerData.Gems += _comboCount;
                    OnGetCombo?.Invoke(_comboCount);
                    Invoke(nameof(AllowPlayerInteract), 2f);
                }
                else
                    Invoke(nameof(AllowPlayerInteract), .25f);


                _comboCount = 0;

                if (CheckLose())
                {
                    SoundManager.Instance.VibrateDevice();
                    ChangeGameState(GameStateEnum.Loose);
                    if (RuntimeDataManager.Instance.PlayerData.Gems < 700)
                    {
                        ResetBoard();
                        UIManager.Instance.OpenUI(UIType.PlayUI);
                        return;
                    }
                    UIManager.Instance.OpenUI(UIType.LooseUI);
                }

            }
        });
    }
    private List<Block> FindSimilarBlockAround(Block block)
    {
        List<Block> blocks = new List<Block>();
        Block tempBlock = null;
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x, block.Coordinate.y + 1), out tempBlock)) // above
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x, block.Coordinate.y - 1), out tempBlock)) // down
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x - 1, block.Coordinate.y), out tempBlock)) // left
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x + 1, block.Coordinate.y), out tempBlock)) // right
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        return blocks;
    }
    private bool CheckLose()
    {
        if (_quantityBlock == 35)
        {
            Block nextBlock = _board.NextBlock;
            for (int x = 0; x < 5; x++)
            {
                Block temp = _board.Block_Coor_Dic[new Vector2Int(x, 0)];
                if (temp.BlockNum.Number == nextBlock.BlockNum.Number)
                {
                    return false;
                }

            }

            return true;
        }
        return false;
    }
    public void ChangeGameState(GameStateEnum state)
    {
        if (state != GameStateEnum.Playing)
        {
            _currentSkillState = null;
        }
        CurrentState = state;
    }
    public void ResetBoard()
    {
        _quantityBlock = 0;
        _point = 0;
        _comboCount = 0;
        _board.ResetBoard();
        OnReset.Invoke();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnLineMouseUp();
        }
    }

    private void AllowPlayerInteract()
    {
        //PlayUI.Instance.ComboText.enabled = false;/////////////////////////////////////////////////////////////////////
        _isBlockMoving = false;
    }

    public void ChangeSkillState(ISkillState state)
    {
        if (state == null)
            OnUseSkill?.Invoke();
        if (_currentSkillState == state) return;
        if (_currentSkillState != null)
        {
            _currentSkillState.Exit();
        }
        _currentSkillState = state;
        if (_currentSkillState != null)
        {
            _currentSkillState.Enter(_board, _actionBlocks, BlockDropState);
        }
    }
}
