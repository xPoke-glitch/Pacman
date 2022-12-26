using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private ScatterState _scatterState;
    private ChaseStateBlinky _chaseStateBlinky;

    protected override void SetupFSM()
    {
        FSM = new FSMSystem();
        _scatterState = new ScatterState(this, ScatterTarget.position);
        _chaseStateBlinky = new ChaseStateBlinky(this, Player.transform);
        FSM.AddState(_scatterState);
        FSM.AddState(_chaseStateBlinky);

        FSM.GoToState(_chaseStateBlinky);
    }
}
