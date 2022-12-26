using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : FSMState
{
    private Ghost _ghost;
    public GameOverState(Ghost ghost)
    {
        _ghost = ghost;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _ghost.SetDirection(Vector3.zero);
    }
}
