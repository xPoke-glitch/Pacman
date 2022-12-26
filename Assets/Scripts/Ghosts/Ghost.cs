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
    protected float FrightenedTime = 5.0f;
    [SerializeField]
    protected float ChaseTime = 20.0f;
    [SerializeField]
    protected float ScatterTime = 7.0f;
    [SerializeField]
    protected LayerMask ObstacleLayer;

    [Header("Ghost States Settings")]
    [SerializeField]
    protected Transform ScatterTarget;
    [SerializeField]
    protected Transform EatenTarget;

    // FSM
    protected FSMSystem FSM;
    protected ScatterState FSM_scatterState;
    protected FrightenedState FSM_frightenedState;
    protected EatenState FSM_eatenState;

    protected Player Player;
    protected float ChaseTimer = 0.0f;
    protected float ScatterTimer = 0.0f;

    private Rigidbody2D _rb;

    public void SetDirection(Vector3 direction)
    {
        MovementDirection = direction;
    }

    protected abstract IEnumerator COWaitFirghtenedTimer();

    protected virtual void SetupFSM()
    {
        FSM = new FSMSystem();
        FSM_scatterState = new ScatterState(this, ScatterTarget.position);
        FSM_frightenedState = new FrightenedState(this);
        FSM_eatenState = new EatenState(this, EatenTarget.position);

        FSM.AddState(FSM_scatterState);
        FSM.AddState(FSM_frightenedState);
        FSM.AddState(FSM_eatenState);
    }
   
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

    protected virtual void OnEnable()
    {
        BigPoint.OnBigPointCollected += SwitchToFrightened;
    }

    protected virtual void OnDisable()
    {
        BigPoint.OnBigPointCollected -= SwitchToFrightened;
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

        Player p;
        if(collision.gameObject.TryGetComponent(out p) && FSM.CurrentState == FSM_frightenedState)
        {
            StopAllCoroutines();

            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), p.GetComponent<Collider2D>(), true);
            FSM.GoToState(FSM_eatenState);
            ScoreManager.Instance.AddPoint(300);
        }
    }

    private void SwitchToFrightened()
    {
        if (FSM.CurrentState == FSM_frightenedState || FSM.CurrentState == FSM_eatenState)
            return;

        FSM.GoToState(FSM_frightenedState);
        StartCoroutine(COWaitFirghtenedTimer());
    }
}
