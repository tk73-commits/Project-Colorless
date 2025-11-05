using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthPoint;
    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    private List<Image> hearts = new List<Image>();

    public void SetMaxHealth(int maxHearts)
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }

        hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(healthPoint, transform); //transform sets parent of the prefab to be the 'heart container' (or whatever you're using for the initial health bar)
            newHeart.sprite = fullHealthSprite;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHealthSprite;
            }
            else
            {
                hearts[i].sprite = emptyHealthSprite;
                hearts[i].color = Color.black;
            }
        }
    }
}
