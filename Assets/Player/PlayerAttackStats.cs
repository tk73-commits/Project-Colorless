using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Player Attack")]
public class PlayerAttackStats : ScriptableObject
{
    [Header("Damage")]
    [Range(0.01f, 100f)] public float MeleeDamage = 40f;
    //[Range(0.01f, 100f)] public float RangedDamage = 20f;

    [Header("Cooldown Timers")]
    [Range(0f, 1f)] public float MeleeCooldown = 0.2f;
    //[Range(0f, 1f)] public float RangedCooldown = 0.2f;
    [Range(0.1f, 5f)] public float MeleeComboBufferTimer = 3f;

    [Header("Collision Checks")]
    public LayerMask EnemyLayer;
    public float MeleeEnemyDetectionRayLength = 0.7f;
    //public float RangedDetectionRayRadius = 1f;

    [Header("Limiters")]
    public bool LimitProjectiles;
    public int MaxProjectiles = 10;
}
