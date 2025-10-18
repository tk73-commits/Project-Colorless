using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Took damage!");

        if (currentHealth <= 0f)
        {
            Death();
            Debug.Log("Defeated enemy!");
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
