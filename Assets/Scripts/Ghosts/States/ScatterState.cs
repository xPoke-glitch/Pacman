using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScatterState : FSMState
{
    private Ghost _ghost;
    private Vector3 _target;

    private float currentTime = 0.0f;
    private float timer = 0.05f;
    public ScatterState(Ghost ghost, Vector3 target)
    {
        _ghost = ghost;
        _target = target;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        currentTime += Time.deltaTime; 

        if(currentTime > timer)
        {
            Vector3 direction = ChooseDirection();
            _ghost.SetDirection(direction);
            currentTime = 0.0f;
        }
        
    }

    private Vector3 ChooseDirection()
    {
        // They are in priority order
        List<Vector3> directions = new List<Vector3>();
        directions.Add(Vector3.up);
        directions.Add(Vector3.left);
        directions.Add(Vector3.down);
        directions.Add(Vector3.right);

        // Remove the Opposite to the MovingDirection
        directions.RemoveAll((Vector3 direction) => direction == (-1 * _ghost.MovementDirection));

        Debug.Log("Remaining directions - currently moving "+_ghost.MovementDirection);
        foreach (Vector3 direction in directions)
        {
            Debug.Log(direction);
        }

        Debug.Log("Directions count after removed opposite " + directions.Count);

        // Remove Occupied
        directions.RemoveAll((Vector3 direction) => _ghost.OccupiedInDirection(direction));

        Debug.Log("Directions count after removed occupied " + directions.Count);

        // Find the Best from target Distance
        int bestIndex = 0;
        float bestDistance = Mathf.Infinity;
        for(int i=0; i<directions.Count; ++i)
        {
            float distance = Vector3.Distance(_ghost.transform.position+directions[i], _target);
            if (distance < bestDistance)
            {
                bestIndex = i;
                bestDistance = distance;
            }
            else if(distance == bestDistance)
            {
                // Priority check
                if(i < bestIndex)
                {
                    bestIndex = i;
                }
            }
        }

        Debug.Log("Chosen " + directions[bestIndex]);
        return directions[bestIndex];
    }
}
