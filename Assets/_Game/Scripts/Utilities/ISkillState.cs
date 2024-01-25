using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillState
{
    void Enter(Board board, List<Block> _actionBlocks, Action callback);
    void Execute(Vector2Int? inputCoor);
    void Exit();
}
