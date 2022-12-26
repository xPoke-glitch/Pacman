using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Ghost : MonoBehaviour
{
    public Vector3 MovementDirection { get; private set; }

    [Header("General Settings")]
    [SerializeField]
    protected float MovementSpeed = 2.0f;
    [SerializeField]
    protected LayerMask ObstacleLayer;

    [Header("Ghost States Settings")]
    [SerializeField]
    protected Transform ScatterTarget;
    [SerializeField]
    protected Transform EatenTarget;

    protected FSMSystem FSM;
    protected Player Player;

    private Rigidbody2D _rb;

    public void SetDirection(Vector3 direction)
    {
        MovementDirection = direction;
    }

    protected abstract void SetupFSM();
   
    protected virtual void Awake()
    {
        MovementDirection = Vector3.zero;
        _rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        SetupFSM();
    }

    protected virtual void Update()
    {
        FSM.CurrentState.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        _rb.MovePosition(transform.position + MovementDirection * Time.fixedDeltaTime * MovementSpeed);
        FSM.CurrentState.OnFixedUpdate();
    }
   
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        FSM.CurrentState.OnTriggerEnter2D(collision);
    }
}
