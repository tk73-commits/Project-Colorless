using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    //[SerializeField] private AudioClip damageSound;

    private SpriteRenderer spriteRenderer;

    public HealthUI healthUI;

    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHealth(maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hazard")
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHealth(currentHealth);
        //SoundFXManager.instance.PlaySoundFXClip(damageSound, transform, 1f);

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            //LevelController.Instance.LoseCondition();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
