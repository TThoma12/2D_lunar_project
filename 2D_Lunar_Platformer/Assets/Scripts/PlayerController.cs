using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float reverseAccel;
    public float driveSpeed;
    public float reverseSpeed;
    public float turnSpeed = 0.1f;

    private Rigidbody2D _rb2D;
    private int _driveDir = 0;
    private int _turnDir = 0;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_rb2D.velocity != Vector2.zero)
        {
            transform.eulerAngles = new(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z - (turnSpeed * Time.deltaTime * _turnDir)
            );
        }

        if (_driveDir != 0)
        {
            float max = driveSpeed;
            float accel = acceleration;
            if (_driveDir == -1)
            {
                max = -reverseSpeed;
                accel = -reverseAccel;
            }

            _rb2D.velocity = new Vector2(
                Mathf.Clamp(_rb2D.velocity.x + accel, -max, max),
                Mathf.Clamp(_rb2D.velocity.y + accel, -max, max)
            ) * Time.deltaTime;
        }
    }

    public void TurnLeft(InputAction.CallbackContext context)
    {
        _turnDir = -1;

        if (context.canceled)
        {
            _turnDir = 0;
        }
    }
    public void TurnRight(InputAction.CallbackContext context)
    {
        _turnDir = 1;

        if (context.canceled)
        {
            _turnDir = 0;
        }
    }

    public void MoveForwards(InputAction.CallbackContext context)
    {
        _driveDir = 1;

        if (context.canceled)
        {
            _driveDir = 0;
        }
    }
    
    public void MoveBackwards(InputAction.CallbackContext context)
    {
        _driveDir = -1;

        if (context.canceled)
        {
            _driveDir = 0;
        }
    }
}
