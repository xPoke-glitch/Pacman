using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerMovements : MonoBehaviour
{
    public bool IsMoving { get; private set; }
    public Vector3 MovementDirection { get; private set; }

    [Header("Settings")]
    [SerializeField]
    private float movementSpeed = 2.0f;
    [SerializeField]
    private LayerMask obstacleLayer;

    private Vector3 _nextDirection = Vector3.zero;

    private Vector3 _lastPosition = Vector3.zero;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lastPosition = transform.position;
        MovementDirection = Vector3.zero;
    }

    private void Update()
    {
        if (InputManager.Instance.IsLeftPressed())
            SetDirection(Vector3.left);

        if (InputManager.Instance.IsRightPressed())
            SetDirection(Vector3.right);

        if (InputManager.Instance.IsUpPressed())
            SetDirection(Vector3.up);

        if (InputManager.Instance.IsDownPressed())
            SetDirection(Vector3.down);

        if (_nextDirection != Vector3.zero)
            SetDirection(_nextDirection);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + MovementDirection * Time.fixedDeltaTime * movementSpeed);

        if (_lastPosition == transform.position)
            IsMoving = false;
        else
            IsMoving = true;

        _lastPosition = transform.position;
    }

    private bool OccupiedInDirection(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }

    private void SetDirection(Vector3 direction)
    {
        if (!OccupiedInDirection(direction))
        {
            MovementDirection = direction;
            _nextDirection = Vector3.zero;
        }
        else
        {
            _nextDirection = direction;
        }
    }

}
