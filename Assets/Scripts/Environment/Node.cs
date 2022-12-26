using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector3> Directions { get; private set; }

    [Header("Other Settings")]
    [SerializeField]
    private LayerMask obstacleLayer;

    // Priority order
    [Header("Available Positions")]
    [SerializeField]
    private bool canUp = true;
    [SerializeField]
    private bool canLeft = true;
    [SerializeField]
    private bool canDown = true;
    [SerializeField]
    private bool canRight = true;

    private void Awake()
    {
        Directions = new List<Vector3>();
    }

    private void Start()
    {
        Debug.Log("Start Called for tile " + this.GetHashCode());
        if (canUp && !OccupiedInDirection(Vector3.up))
            Directions.Add(Vector3.up);
        if (canLeft && !OccupiedInDirection(Vector3.left))
            Directions.Add(Vector3.left);
        if (canDown && !OccupiedInDirection(Vector3.down))
            Directions.Add(Vector3.down);
        if (canRight && !OccupiedInDirection(Vector3.right))
            Directions.Add(Vector3.right);
    }

    private void OnEnable()
    {
        EatenState.OnGhostEaten += RefreshDirections;
        EatenState.OnGhostRestored += RefreshDirections;
        Gate.OnFirstClose += RefreshDirections;
    }

    private void OnDisable()
    {
        EatenState.OnGhostEaten -= RefreshDirections;
        EatenState.OnGhostRestored -= RefreshDirections;
        Gate.OnFirstClose -= RefreshDirections;
    }

    private bool OccupiedInDirection(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.6f, obstacleLayer);
        return hit.collider != null;
    }

    private void RefreshDirections()
    {
        Directions.Clear();
        if (canUp && !OccupiedInDirection(Vector3.up))
            Directions.Add(Vector3.up);
        if (canLeft && !OccupiedInDirection(Vector3.left))
            Directions.Add(Vector3.left);
        if (canDown && !OccupiedInDirection(Vector3.down))
            Directions.Add(Vector3.down);
        if (canRight && !OccupiedInDirection(Vector3.right))
            Directions.Add(Vector3.right);
    }
}
