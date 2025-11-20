using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestProgress;

[CreateAssetMenu(menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string description;
    public List<QuestObjective> objectives;
    public List<QuestReward> questRewards;	// it's a list in case you want multiple rewards from one quest

    // Auto-generates a Quest ID to ensure the quest is unique; called when a scriptable object is edited
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(questID))
        {
            questID = questName + Guid.NewGuid().ToString();
        }
    }
}

[System.Serializable] // saves to JSON, so needs to be serializable
public class QuestObjective
{
    // ID used to match w/ item IDs / enemy IDs – things that are required to fulfill the quest conditions; unique identifier for objectives
    public string objectiveID;
    public string description;
    public ObjectiveType type;
    public int requiredAmount;  // for how many of x you need to collect / defeat / whatever
    public int currentAmount;

    // if you currently have equal to or more than the required amount, it will return true and say the objective is complete
    public bool IsCompleted => currentAmount >= requiredAmount;
}

public enum ObjectiveType { CollectItem, DefeatEnemy, ReachLocation, TalkNPC, Custom }

[System.Serializable]
public class QuestProgress
{
    public Quest quest;
    public List<QuestObjective> objectives;
    // ^ used to create a constructor; when a new one of these is made, this (class?) will be called

    public QuestProgress(Quest quest)
    {
        // store accepted quest into quest slot
        this.quest = quest;
        objectives = new List<QuestObjective>();

        // deep copy to avoid modifying the original; quest itself shouldn't change, the only thing about objective that changes is currentAmount
        foreach (var obj in quest.objectives)
        {
            objectives.Add(new QuestObjective
            {
                objectiveID = obj.objectiveID,
                description = obj.description,
                type = obj.type,
                requiredAmount = obj.requiredAmount,
                currentAmount = 0   // when a new quest is started, this is always 0
            });
        }
    }

    public bool IsCompleted => objectives.TrueForAll(o => o.IsCompleted);

    public string QuestID => quest.questID;
}

[System.Serializable]
public class QuestReward
{
    public RewardType type;
    public int rewardID;    // can store different IDs, like an ItemID, an EXP type ID, cutscene ID, etc.
    public int amount = 1;
}

public enum RewardType { Item, Gold, Experience, Custom }