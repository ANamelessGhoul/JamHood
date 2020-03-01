using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCombat : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    
    private void Awake()
    {
        GetComponent<PlayerMovement>().AttackEvent += ExecuteAttack;
    }

    public void ExecuteAttack(Vector3 dir)
    {
        for (int i = -60; i <= 60; i += 10)
        {
            
            RaycastHit hit;
            Debug.DrawRay(transform.position, Quaternion.Euler(0, i, 0) * dir * attackRange, Color.red, 1f);
            Physics.Raycast(transform.position, Quaternion.Euler(i, 0, 0) * dir, out hit, attackRange, enemyLayer);
            
            //Debug.Log(hit);

        }

    }
}
