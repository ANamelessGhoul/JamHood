﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2;
    [SerializeField] private float minDist = 2;
    [SerializeField] private float maxDist = 15;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(transform.position, target.position);
        Debug.Log(dist);
        if (dist < maxDist)
        {
            if (dist > minDist)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
            }
            else
            {
                Debug.Log("<color=red>Hit! </color>");
            }
        }
        
    }
}