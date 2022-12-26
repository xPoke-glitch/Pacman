using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightenedState : FSMState
{
    private Ghost _ghost;

    public FrightenedState(Ghost ghost)
    {
        _ghost = ghost;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // 180 rotation on enter state
        _ghost.SetDirection(_ghost.MovementDirection * -1);
    }

    private Vector3 ChooseDirection(List<Vector3> directions)
    { 
        int randIndex = Random.Range(0, directions.Count);
        return directions[randIndex];
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Node node = null;
        if (collision.gameObject.TryGetComponent(out node))
        {
            // Copy the Direction list
            // You don't want to modify the Node.Directions list
            List<Vector3> directions = new List<Vector3>();
            for (int i = 0; i < node.Directions.Count; ++i)
            {
                directions.Add(node.Directions[i]);
            }

            // Choose Direction and Set it
            Vector3 direction = ChooseDirection(directions);
            _ghost.SetDirection(direction);
        }
    }
}
