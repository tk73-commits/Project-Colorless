using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Health")]
    public int maxHealth = 100;
    int currentHealth;

    [Header("Hitstun")]
    private float stunTime;
    public float startStunTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took damage!");

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
