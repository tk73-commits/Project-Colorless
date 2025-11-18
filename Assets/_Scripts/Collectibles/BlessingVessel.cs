using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessingVessel : MonoBehaviour
{
    private int blessingVesselProgress;
    private int maxedBlessing = 100;
    private int blessingVessel;

    private void Start()
    {
        blessingVessel = 0;
        blessingVesselProgress = 0;

        BlessingUnit.OnBlessingCollect += IncreaseBlessingProgress;
    }

    private void Update()
    {
        if (blessingVesselProgress >= maxedBlessing)
        {
            CreateBlessing();
            blessingVesselProgress = 0;
        }
    }

    private void CreateBlessing()
    {
        blessingVessel++;
    }

    private void IncreaseBlessingProgress(int amount)
    {
        blessingVesselProgress += amount;
    }
}
