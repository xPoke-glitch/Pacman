using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private ScatterState _scatterState;
    private ChaseStateBlinky _chaseStateBlinky;
    private FrightenedState _frightenedState;
    private EatenState _eatenState;

    protected override void SetupFSM()
    {
        FSM = new FSMSystem();
        _scatterState = new ScatterState(this, ScatterTarget.position);
        _chaseStateBlinky = new ChaseStateBlinky(this, Player.transform);
        _frightenedState = new FrightenedState(this);
        _eatenState = new EatenState(this, EatenTarget.position);
        FSM.AddState(_scatterState);
        FSM.AddState(_chaseStateBlinky);
        FSM.AddState(_frightenedState);
        FSM.AddState(_eatenState);

        FSM.GoToState(_eatenState);
    }
}
