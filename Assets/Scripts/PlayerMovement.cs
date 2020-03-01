using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum PlayerType
{
    Player1,
    Player2
}

public enum PlayerState
{
    Normal,
    Attacking,
    Special
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerType playerType;


    private float moveX;
    private float moveY;

    //private Vector3 moveDir;
    [SerializeField] private float movementSpeedX = 1f;
    [SerializeField] private float movementSpeedY = 1f;

    [SerializeField] private Vector3 dif;

    public Action<Vector3> AttackEvent;
    public Action<Vector3> SpecialEvent;

    public Transform tf_cam;
    private Camera _camera;

    private Rigidbody rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool isAttack;
    private bool isSpecial;

    private PlayerState _state = PlayerState.Normal;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _camera = tf_cam.GetComponent<Camera>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        MoveCamera();

        switch (_state)
        {
            case PlayerState.Normal:
                HandleInput();
                break;
            case PlayerState.Attacking:
            case PlayerState.Special:
                moveX = 0;
                moveY = 0;
                break;
        }

        
        
        _spriteRenderer.flipX = moveX > 0.2f;


        _animator.SetFloat("velocityH", Mathf.Abs(moveX));

        

        _animator.SetFloat("velocityV", moveY);

        _animator.SetBool("Special", isSpecial);

        HandleAttack();
    }

    private void HandleInput()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");
                isAttack = Input.GetMouseButtonDown(0);
                isSpecial = Input.GetMouseButton(1);
                break;
            case PlayerType.Player2:
                moveX = Input.GetAxis("HorizontalP2");
                moveY = Input.GetAxis("VerticalP2");
                isAttack = Input.GetKeyDown(KeyCode.JoystickButton2);
                isSpecial = Input.GetKeyDown(KeyCode.JoystickButton1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void MoveCamera()
    {
        tf_cam.position = transform.position + dif;
    }


    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var moveDir = new Vector3(moveX, 0, moveY).normalized;
        moveDir = new Vector3(moveDir.x * movementSpeedX, moveDir.y, moveDir.z * movementSpeedY);
        rb.velocity = moveDir;
    }

    private void HandleAttack()
    {
        if (isAttack)
        {
            _state = PlayerState.Attacking;

            

            var direction = GetDirection();

            _animator.SetTrigger("Attack");


            AttackEvent.Invoke(direction);
            isAttack = false;
            _state = PlayerState.Normal;
        }

        if (isSpecial)
        {
            _state = PlayerState.Special;
            
            
            var direction = GetDirection();
            
            _animator.SetTrigger("Special");


            //SpecialEvent.Invoke(direction);
            isSpecial = false;
            _state = PlayerState.Normal;
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction;
        switch (playerType)
        {
            case PlayerType.Player1:

                direction = new Vector3((Input.mousePosition.x / Screen.width - 0.5f) * 32, 0,
                    (Input.mousePosition.y / Screen.height - 0.5f) * 18);
                break;
            case PlayerType.Player2:
                direction = new Vector3(-Input.GetAxisRaw("HorizontalAimP2"), 0, Input.GetAxisRaw("VerticalAimP2"));
                break;
            default:
                direction = Vector3.zero;
                break;
        }

        _animator.SetFloat("DirectionX", direction.x);
        _animator.SetFloat("DirectionY", direction.z);
        
        //_spriteRenderer.flipX = direction.x > 0.2f;
        
        return direction;
    }
}