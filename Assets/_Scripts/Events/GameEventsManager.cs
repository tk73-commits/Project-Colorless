using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance {  get; private set; }

    public PlayerEvents playerEvents;
    public MiscEvents miscEvents;
    //public QuestEvents questEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize events

        playerEvents = new PlayerEvents();
        miscEvents = new MiscEvents();
        //questEvents = new QuestEvents();
    }
}
