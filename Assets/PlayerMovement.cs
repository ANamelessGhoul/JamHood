using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private Vector3 moveDir;
    [SerializeField] private float speed;
    public Transform tf_cam;
    [SerializeField] private Vector3 dif;
    private Transform tf;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tf_cam.position = tf.transform.position + dif;
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        moveDir = new Vector3(moveX,0,moveY).normalized;
        Debug.Log(moveDir);
        //moveY = ((int)Input.GetKey(KeyCode.W) - (int)Input.GetKey(KeyCode.S));
        //moveX = Input.GetKey(KeyCode.D) - Input.GetKey(KeyCode.A);
        /*
        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        } */
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * speed;
        //tf.transform.position += new Vector3(moveX * horSpeed, 0 , moveY * verSpeed);
    }
}
