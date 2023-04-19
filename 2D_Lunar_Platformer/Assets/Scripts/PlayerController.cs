using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public HealthManager health;

    private bool SpeedOn=false;
    public bool DoubleOn = false;
    public bool ShieldOn = false;

    private float timePowerUp = 10f;

    private float acceleration =250;
    private float reverseAccel=180;
    public float driveSpeed;
    private float turnSpeed = 180;

    private Rigidbody2D _rb2D;
    private int _driveDir = 0;
    private int _turnDir = 0;
    private bool _turningLeft = false;
    private bool _turningRight = false;
    private bool _drivingForwards = false;
    private bool _drivingBackwards = false;
    private InputMaster _inputMaster;


    SpriteRenderer sprite;
    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _inputMaster = new InputMaster();
        sprite = GetComponent<SpriteRenderer>();
        _inputMaster.Movement.MoveForwards.performed += (ctx) =>
        {
            _drivingForwards = true;
            _driveDir = 1;
        };
        _inputMaster.Movement.MoveForwards.canceled += (ctx) => _drivingForwards = false;

        _inputMaster.Movement.MoveBackwards.performed += (ctx) =>
        {
            _drivingBackwards = true;
            _driveDir = -1;
        };
        _inputMaster.Movement.MoveBackwards.canceled += (ctx) => _drivingBackwards = false;

        _inputMaster.Movement.TurnRight.performed += (ctx) =>
        {
            _turnDir = 1;
            _turningRight = true;
        };
        _inputMaster.Movement.TurnRight.canceled += (ctx) => _turningRight = false;

        _inputMaster.Movement.TurnLeft.performed += (ctx) =>
        {
            _turnDir = -1;
            _turningLeft = true;
        };
        _inputMaster.Movement.TurnLeft.canceled += (ctx) => _turningLeft = false;
    }

    private void Update()
    {
        if (!_turningLeft && !_turningRight)
        {
            _turnDir = 0;
        }

        if (!_drivingBackwards && !_drivingForwards)
        {
            _driveDir = 0;
        }

        if (SpeedOn == true && timePowerUp >0)
        {
            acceleration = 450f;
            timePowerUp -= Time.deltaTime;
        }
        if (ShieldOn == true && timePowerUp > 0)
        {
            sprite.color = Color.blue;
            timePowerUp -= Time.deltaTime;
        }


        if (timePowerUp<=0)
        {
            acceleration = 250;
            SpeedOn = false;
            DoubleOn = false;
            ShieldOn = false;
            sprite.color = Color.white;
        }



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
            float accel = acceleration;
            if (_driveDir == -1)
            {
                accel = reverseAccel;
            }

            _rb2D.velocity = new Vector2(
                Mathf.Clamp(_rb2D.velocity.x + accel, -driveSpeed, driveSpeed),
                Mathf.Clamp(_rb2D.velocity.y + accel, -driveSpeed, driveSpeed)
            ) * Time.deltaTime * transform.up * _driveDir;
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable() 
    { 
        _inputMaster.Disable(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Projectile")
        {
            if (ShieldOn)
            {
                return;
            }
            else
            {
                health.TakeDamage(1);
            }
            
        }
        if (collision.gameObject.tag == "Speed")
        {
            
            timePowerUp = 10f;
            Destroy(collision.gameObject);
            SpeedOn = true;
        }
        if (collision.gameObject.tag == "Double")
        {

            timePowerUp = 10f;
            Destroy(collision.gameObject);
            DoubleOn = true;
        }
        if (collision.gameObject.tag == "Shield")
        {

            timePowerUp = 10f;
            Destroy(collision.gameObject);
            ShieldOn = true;
        }
    }
}
