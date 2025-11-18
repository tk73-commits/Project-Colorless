using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    [Header("Health Stats")]
    public int maxHealth = 5;

    [Header("Combat Stats")]
    public float attackDamage = 1f;
    public float attackCooldown = 1f;
    public bool ableToRangedAttack;

    [Header("Movement Stats")]
    public bool ableToDoubleJump;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}