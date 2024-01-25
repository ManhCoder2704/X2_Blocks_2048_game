using System;
using System.Collections.Generic;
using UnityEngine;

public class ClearOneRow : ISkillState
{
    private Board _board;
    private List<Block> _actionBlocks;
    private Action _callback;
    public void Enter(Board board, List<Block> actionBlocks, Action callback)
    {
        _board = board;
        _actionBlocks = actionBlocks;
        _callback = callback;
    }

    public void Execute(Vector2Int? inputCoor)
    {
        _board.ClearOneRow(inputCoor, _actionBlocks, _callback);
    }

    public void Exit()
    {

    }
}
