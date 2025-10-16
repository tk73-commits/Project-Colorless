using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Cooldown Timer")]
    private float attackCooldown;
    public float startAttackCooldown;

    [Header("Attack Hitbox")]
    public Transform attackPos;
    public LayerMask enemyLayers;
    public float attackRange;

    [Header("Attack Damage Value")]
    public int attackDamage = 40;

    void Update()
    {
        if(attackCooldown > 0)
        {
            //Attack();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);
            for(int i = 0; i < hitEnemies.Length; i++)
            {
                hitEnemies[i].GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            attackCooldown = startAttackCooldown;
        }
        else
        {
            attackCooldown = Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
