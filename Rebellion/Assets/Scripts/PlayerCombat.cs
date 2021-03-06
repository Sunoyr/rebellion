﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask enemyLayers;

    public Transform attackPoint;
    public float attackRange = 0.5f;

    public int attackDamage = 10;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }/* else if(rb.velocity.y!=0f)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log("HEY IM HERE JMP ATTACK");
                    Jump_Attack();
                }
            }*/
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Detect ennemies in range
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
        }
    }

    void Jump_Attack()
    {
        // Play jump attack animation
        animator.SetTrigger("JumpAttack");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
