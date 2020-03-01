using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2;
    [SerializeField] private float minDist = 2;
    [SerializeField] private float maxDist = 15;
    private GameObject[] targets;

    private Animator _animator;

    private float remainingTime;
    [SerializeField] private float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        targets = GameObject.FindGameObjectsWithTag("Player");

        remainingTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        var target = targets[0].transform;
        if ((target.position - transform.position).magnitude > (targets[1].transform.position - transform.position).magnitude)
            target = targets[1].transform;


        float dist = Vector3.Distance(transform.position, target.position);
        //Debug.Log(dist);
        if (dist < maxDist)
        {
            if (dist > minDist)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
            }
            else
            {
                remainingTime -= Time.deltaTime;

                if (remainingTime <= 0)
                {
                    _animator.SetBool("Attack1", true);
                    Debug.Log("<color=red>Hit! </color>");
                    
                    
                    var hitRb = target.gameObject.GetComponent<Rigidbody>();
                    if (hitRb == null)
                    {
                        Debug.Log("error");
                        return;
                    }

                    var dest = (target.transform.position - transform.position);

                    dest.y = 0;

                    hitRb.MovePosition(target.transform.position + dest.normalized);
                    
                    remainingTime = timer;

                }

            }


        }
    }
}