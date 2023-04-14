using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public event Action OnJump;
    public event Action OnGroundEnter;
    public event Action OnGroundLeave;

    public float movementSpeed;
    public float acceleration;
    public float deacceleration;
    public bool grounded = false;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;

    private Rigidbody2D _rb2D;
    private Vector2 _currentDirection;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        IsGrounded();
    }

    public void MovementCallback(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _currentDirection = direction;
    }

    public void JumpCallback(InputAction.CallbackContext context)
    {
        Jump(0);
    }

    private void Move()
    {
        if (_currentDirection == Vector2.zero)
        {
            return;
        }

        if (_rb2D.velocity.x * Mathf.Sign(_currentDirection.x) < movementSpeed)
        {
            float accel = acceleration;
            if (Mathf.Sign(_rb2D.velocity.x) == Mathf.Sign(_currentDirection.x))
            {
                accel = deacceleration;
            }
            accel = Mathf.Clamp(accel, 0, Mathf.Abs(movementSpeed - _rb2D.velocity.x));
            _rb2D.velocity += new Vector2(accel, 0) * _currentDirection;
        }
    }

    private void Jump(float timeHeld)
    {
        Debug.Log(timeHeld);
        if (!grounded)
        {
            return;
        }
        // Trigger the jump event for any listeners
        OnJump?.Invoke();
        _rb2D.velocity += new Vector2(0, 5);
    }

    private void IsGrounded()
    {
        if (Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius))
        {
            Debug.DrawLine((Vector2)_groundCheck.position, (Vector2)_groundCheck.position + new Vector2(0, _groundCheckRadius));
            if (!grounded)
            {
                grounded = true;
                OnGroundEnter?.Invoke();
            }
        } 
        else if (grounded)
        {
            grounded = false;
            OnGroundLeave?.Invoke();
        }
    }
}
