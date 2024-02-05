using System;
using System.Collections.Generic;
using UnityEngine;

public class ClearOneRow : ISkillState
{
    private Board _board;
    private List<Block> _actionBlocks;
    private Action _callback;
    private const int _price = 400;
    public void Enter(Board board, List<Block> actionBlocks, Action callback)
    {
        _board = board;
        _actionBlocks = actionBlocks;
        _callback = callback;
    }

    public void Execute(Vector2Int? inputCoor)
    {
        if (!_board.Block_Coor_Dic.ContainsKey(inputCoor.Value))
        {
            GameplayManager.Instance.IsBlockMoving = false;
            GameplayManager.Instance.ChangeSkillState(null);
            return;
        }
        RuntimeDataManager.Instance.PlayerData.Gems -= _price;
        SoundManager.Instance.PlaySFX(SFXType.Skill);
        _board.ClearOneRow(inputCoor, _actionBlocks, _callback);
    }

    public void Exit()
    {
    }
}
