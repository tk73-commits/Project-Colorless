using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action<int> onBlessingGained;
    public void BlessingGained(int blessing)
    {
        if (onBlessingGained != null)
        {
            onBlessingGained(blessing);
        }
    }

    public event Action<int> onBlessingLost;
    public void BlessingLost(int blessing)
    {
        if (onBlessingLost != null)
        {
            onBlessingLost(blessing);
        }
    }

    public event Action<int> onPlayerBlessingsChange;
    public void PlayerBlessingsChange(int blessing)
    {
        if (onPlayerBlessingsChange != null)
        {
            onPlayerBlessingsChange(blessing);
        }
    }
}
