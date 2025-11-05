using System;
using UnityEngine;

public class MiscEvents
{
    public event Action onBlessingCollect;
    public void BlessingCollect()
    {
        if (onBlessingCollect != null)
            onBlessingCollect();
    }
}
