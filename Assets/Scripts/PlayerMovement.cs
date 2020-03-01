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

public class PlayerMovement : MonoBehaviour
{
    public PlayerType playerType;
    
    private float moveX;
    private float moveY;

    //private Vector3 moveDir;
    [SerializeField] private float movementSpeedX = 1f;
    [SerializeField] private float movementSpeedY = 1f;
    
    [SerializeField] private Vector3 dif;

    [SerializeField] public Action<Vector3> AttackEvent;

    public Transform tf_cam;

    private Rigidbody rb;
    
    private bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        MoveCamera();

        HandleInput();
        
    }

    private void HandleInput()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");
                isAttack = Input.GetMouseButtonDown(0);
                break;
            case PlayerType.Player2:
                moveX = Input.GetAxis("HorizontalP2");
                moveY = Input.GetAxis("VerticalP2");
                isAttack = Input.GetKeyDown(KeyCode.JoystickButton2);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        HandleAttack();

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
            Vector3 direction;
            
            switch (playerType)
            {
                case PlayerType.Player1:
                    //var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    
                    var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    direction = hit.point + Vector3.back * 2;
                    Debug.Log(direction);

                    direction.y = 0;
                    break;
                case PlayerType.Player2:
                    direction = new Vector3(Input.GetAxisRaw("HorizontalP2"), 0 , Input.GetAxisRaw("VerticalP2"));
                    break;
                default:
                    direction = Vector3.zero;
                    break;
            }
            
            
            AttackEvent.Invoke(direction);
            isAttack = false;

        }
    }
}