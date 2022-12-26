using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateInky : FSMState
{
    private Ghost _ghost;
    private Vector3 _target;
    private PlayerMovements _playerTarget;
    private Transform _blinkyTarget;

    public ChaseStateInky(Ghost ghost, Transform target, Transform blinkyTarget)
    {
        _ghost = ghost;
        _playerTarget = target.gameObject.GetComponent<PlayerMovements>();
        _blinkyTarget = blinkyTarget;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // 180 rotation on enter state
        _ghost.SetDirection(_ghost.MovementDirection * -1);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        UpdateTargetPosition();
    }

    private Vector3 ChooseDirection(List<Vector3> directions)
    {
        // The directions are already in priority order
        // and the directions where the ghost is able to move

        // Remove the Opposite to the MovingDirection
        directions.RemoveAll((Vector3 direction) => direction == (-1 * _ghost.MovementDirection));

        // Find the Best from target Distance
        int bestIndex = 0;
        float bestDistance = Mathf.Infinity;
        for (int i = 0; i < directions.Count; ++i)
        {
            float distance = Vector3.Distance(_ghost.transform.position + directions[i], _target);
            if (distance < bestDistance)
            {
                bestIndex = i;
                bestDistance = distance;
            }
            else if (distance == bestDistance)
            {
                // Priority check
                if (i < bestIndex)
                {
                    bestIndex = i;
                }
            }
        }
        return directions[bestIndex];
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

    private void UpdateTargetPosition()
    {
        if (_playerTarget.MovementDirection == Vector3.up)
        {
            _target = _playerTarget.transform.position + new Vector3Int(2, 2, 0);
        }
        else
        {
            _target = _playerTarget.transform.position + _playerTarget.MovementDirection * 2f;
        }

        // Vector from the target with offset 2 and Blinky position with 180 rotation
        _target = (_blinkyTarget.position - _target)*-1;
    }
}
