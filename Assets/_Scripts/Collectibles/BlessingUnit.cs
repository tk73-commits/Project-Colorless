using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlessingUnit : MonoBehaviour
{
    // a class that defines and contains logic for collectible Blessing units

    public static event Action<int> OnBlessingCollect;
    [SerializeField] private int blessingGained = 10;

    private void Collect()
    {
        //GameEventsManager.instance.playerEvents.BlessingGained(blessingGained);
        //GameEventsManager.instance.miscEvents.BlessingCollect();
        OnBlessingCollect.Invoke(blessingGained);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            return;
        }
        else if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
}
