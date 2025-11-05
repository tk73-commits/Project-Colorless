using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public int blessingCount = 0;
    public TextMeshProUGUI blessingText;

    private void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        BlessingUnit.OnBlessingCollect += IncreaseProgressAmount;
        UpdateBlessingText();
    }

    private void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100)
        {
            blessingCount++;
            UpdateBlessingText();
            ResetProgressMeter();
        }
    }

    private void UpdateBlessingText()
    {
        blessingText.text = $"{blessingCount}";
    }

    private void ResetProgressMeter()
    {
        progressAmount = 0;
        progressSlider.value = 0;
    }
}
