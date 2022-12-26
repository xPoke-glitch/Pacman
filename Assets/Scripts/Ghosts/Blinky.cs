using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private ChaseStateBlinky FSM_chaseStateBlinky;

    protected override IEnumerator COWaitFirghtenedTimer()
    {
        yield return new WaitForSeconds(FrightenedTime);

        if(ScatterTimer > ChaseTimer)
            FSM.GoToState(FSM_scatterState);
        else
            FSM.GoToState(FSM_chaseStateBlinky);
    }

    protected override void SetupFSM()
    {
        base.SetupFSM();

        FSM_chaseStateBlinky = new ChaseStateBlinky(this, Player.transform);
      
        FSM.AddState(FSM_chaseStateBlinky);

        FSM.GoToState(FSM_scatterState);
    }

    protected override void Update()
    {
        base.Update();
        if (FSM.CurrentState == FSM_scatterState)
        {
            ScatterTimer += Time.deltaTime;
        }
        if (FSM.CurrentState == FSM_chaseStateBlinky)
        {
            ChaseTimer += Time.deltaTime;
        }
        if (ScatterTimer >= ScatterTime)
        {
            ScatterTimer = 0;
            FSM.GoToState(FSM_chaseStateBlinky);
        }
        if (ChaseTimer >= ChaseTime)
        {
            ChaseTimer = 0;
            FSM.GoToState(FSM_scatterState);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag.Equals("SafeArea") && FSM.CurrentState == FSM_eatenState)
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), Player.GetComponent<Collider2D>(), false);
            int randIndex = Random.Range(0, 2);

            if (randIndex == 0)
                FSM.GoToState(FSM_scatterState);
            else
                FSM.GoToState(FSM_chaseStateBlinky);
        }
    }
}
