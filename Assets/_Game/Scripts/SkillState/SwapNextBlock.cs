using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapNextBlock : ISkillState
{
    private Board _board;
    private List<Block> _actionBlocks;
    private Action _callback;
    private const int _price = 100;
    public void Enter(Board t, List<Block> actionBlocks, Action callback)
    {
        if (RuntimeDataManager.Instance.PlayerData.Gems < _price)
        {
            GameplayManager.Instance.ChangeSkillState(null);
            return;
        }
        _board = t;
        _actionBlocks = actionBlocks;
        _callback = callback;
        this.Execute(null);
    }

    public void Execute(Vector2Int? inputCoor)
    {
        RuntimeDataManager.Instance.PlayerData.Gems -= _price;
        SoundManager.Instance.PlaySFX(SFXType.Skill);
        _board.SwapNextBlock();
        GameplayManager.Instance.ChangeSkillState(null);
    }

    public void Exit()
    {
    }
}
