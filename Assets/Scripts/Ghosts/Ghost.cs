using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class Ghost : MonoBehaviour
{
    public Vector3 MovementDirection { get; private set; }

    [Header("General Settings")]
    [SerializeField]
    private float movementSpeed = 2.0f;
    [SerializeField]
    private LayerMask obstacleLayer;

    [Header("Ghost States Settings")]
    [SerializeField]
    private Transform scatterTarget;

    private Rigidbody2D _rb;

    // FSM
    private FSMSystem _FSM;
    private ScatterState _scatterState;

    public bool OccupiedInDirection(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 0.2f, obstacleLayer);
        return hit.collider != null;
    }

    public void SetDirection(Vector3 direction)
    {
        MovementDirection = direction;
    }

    private void Awake()
    {
        MovementDirection = Vector3.right;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetupFSM();
    }

    private void Update()
    {
        _FSM.CurrentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + MovementDirection * Time.fixedDeltaTime * movementSpeed);
        _FSM.CurrentState.OnFixedUpdate();
    }

    private void SetupFSM()
    {
        _FSM = new FSMSystem();
        _scatterState = new ScatterState(this,scatterTarget.position);
        
        _FSM.AddState(_scatterState);
    }
}
