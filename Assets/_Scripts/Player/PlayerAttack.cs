using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public PlayerAttackStats AttackStats;

    [Header("Cooldown Timer")]
    private bool _isMeleeAttacking;
    [SerializeField] private float _meleeAttackCooldown = 0.2f;

    [Header("Attack Hitbox")]
    public Transform attackPos;
    public LayerMask enemyLayers;
    public float attackRange;

    private void FixedUpdate()
    {
        if (InputManager.AttackWasPressed)
        {
            if (!_isMeleeAttacking && _meleeAttackCooldown > 0)
            {
                MeleeAttack();
            }

            ResetCooldown();
        }
    }

    public void MeleeAttack()
    {
        _isMeleeAttacking = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);
        for(int i = 0; i < hitEnemies.Length; i++)
        {
            //hitEnemies[i].GetComponent<Enemy>().TakeDamage(AttackStats.MeleeDamage);
        }
        _meleeAttackCooldown -= Time.fixedDeltaTime;
    }

    public void RangedAttack()
    {
        _isMeleeAttacking = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);
        for(int i = 0; i < hitEnemies.Length; i++)
        {
            //hitEnemies[i].GetComponent<Enemy>().TakeDamage(AttackStats.MeleeDamage);
        }
        _meleeAttackCooldown -= Time.fixedDeltaTime;
    }

    public void ResetCooldown()
    {
        _isMeleeAttacking = false;
        _meleeAttackCooldown = AttackStats.MeleeCooldown;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
