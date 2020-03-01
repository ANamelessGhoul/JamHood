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
        StartCoroutine(ExecuteAttackA(dir));

    }

    public IEnumerator ExecuteAttackA(Vector3 dir)
    {
        var hitObjects = new List<GameObject>();
        
        yield return new WaitForSeconds(0.5f);
        
        for (int i = -60; i <= 60; i += 10)
        {
            
            RaycastHit hit;
            Debug.DrawRay(transform.position, Quaternion.Euler(0, i, 0) * dir.normalized * attackRange, Color.red, 1f);
            Physics.Raycast(transform.position, Quaternion.Euler(0, i, 0) * dir, out hit, attackRange, enemyLayer);
            
            if(hit.collider != null && !hitObjects.Contains(hit.collider.gameObject))
                hitObjects.Add(hit.collider.gameObject);
            Debug.Log(hitObjects.Count);

        }

        foreach (var hit in hitObjects)
        {
            var hitRb = hit.GetComponent<Rigidbody>();
            if(hitRb == null)
                continue;

            hitRb.velocity = (hit.transform.position - transform.position).normalized * 10;
            
            
        }
        
        
        yield return new WaitForSeconds(0.2f);

        foreach (var hit in hitObjects)
        {
            Destroy(hit);
        }

    }
}
