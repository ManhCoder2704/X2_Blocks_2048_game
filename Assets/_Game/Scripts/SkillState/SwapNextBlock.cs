using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapNextBlock : ISkillState
{
    private Board _board;
    private List<Block> _actionBlocks;
    private Action _callback;
    public void Enter(Board t, List<Block> actionBlocks, Action callback)
    {
        _board = t;
        _actionBlocks = actionBlocks;
        _callback = callback;
        this.Execute(null);
    }

    public void Execute(Vector2Int? inputCoor)
    {
        _board.SwapNextBlock();
        GameplayManager.Instance.ChangeSkillState(null);
    }

    public void Exit()
    {

    }
}
