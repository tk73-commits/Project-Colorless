using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    public List<QuestProgress> activateQuests = new();
    private QuestUIController questUI;
    public List<string> handinQuestIDs = new();
        // list for remembering and saving which questIDs have been handed in

    public void Awake()
    {
        // ensures there is only one QuestController in the game
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        questUI = FindObjectOfType<QuestUIController>();
        // When InventoryController triggers OnInventoryChanged, calls the function below
        InventoryController.Instance.OnInventoryChanged += CheckInventoryForQuests;
    }

    // NPC will pass in quest they are holding
    public void AcceptQuest(Quest quest)
    {
        if (IsQuestActive(quest.questID)) return;   // if a quest is already active, then it returns out of the function so that it never accepts a new quest that already exists

        // constructs a new active quest based on the quest template
        activateQuests.Add(new QuestProgress(quest));

        questUI.UpdateQuestUI();
    }

    public bool IsQuestComplete(string questID)
    {
        // if we have [this quest] in active quests, and if [quest's objectives] are all completed, return true; else, return false
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        return quest != null && quest.objectives.TrueForAll(o => o.IsCompleted);
    }

    public void HandInQuest(string questID)
    {
        // try remove required items
        if (!RemoveRequiredItemsFromInventory(questID))
            return; // quest couldn't be completed – missing items


        // remove quest from quest log
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        if (quest != null)
        {
            handinQuestIDs.Add(questID);
            activateQuests.Remove(quest);
            questUI.UpdateQuestUI();
        }
    }

    // if quest has been handed in, will be in list & returned true; if not, returned false
    public bool IsQuestHandedIn(string questID)
    {
        return handinQuestIDs.Contains(questID);
    }

    public bool RemoveRequiredItemsFromInventory(string questID)
    {
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        if (quest == null) return false;

        Dictionary<int, int> requiredItems = new();

        // item requirements from objectives
        foreach (QuestObjective objective in quest.objectives)
        {
            // if objective is collecting an item, and we can pass objective ID to an int –
            if (objective.type == ObjectiveType.CollectItem && int.TryParse(objective.objectiveID, out int itemID))
            {
                requiredItems[itemID] = objective.requiredAmount;    // stores itemID/requiredAmount
            }
        }

        // verify that player has items in inventory
        Dictionary<int, int> itemCounts = InventoryController.Instance.GetItemCount();
        foreach (var item in requiredItems)
        {
            // if what is present in inventory is less than the required amount, then return false — not enough items to complete quest
            if (itemCounts.GetValueOrDefault(item.Key) < item.Value)
                return false;
        }

        // remove items from inventory if met requirements
        foreach (var itemRequirement in requiredItems)
            InventoryController.Instance.RemoveItemsFromInventory(itemRequirement.Key, itemRequirement.Value);  // item ID + item amount you want to remove

        return true;
    }

    // does this quest ID already exist in the active quests? pass back T / F
    public bool IsQuestActive(string questID) => activateQuests.Exists(q => q.QuestID == questID);

    public void CheckInventoryForQuests()
    {
        Dictionary<int, int> itemCounts = InventoryController.Instance.GetItemCount();

        foreach (QuestProgress quest in activateQuests)
        {
            foreach (QuestObjective questObjective in quest.objectives)
            {
                if (questObjective.type != ObjectiveType.CollectItem) continue; // if quest objective is NOT to collect an item, skip this objective and move on to the next one
                if (!int.TryParse(questObjective.objectiveID, out int itemID)) continue;    // if objective ID cannot be parsed out to an int, skip objective

                int newAmount = itemCounts.TryGetValue(itemID, out int count) ? Mathf.Min(count, questObjective.requiredAmount) : 0;
                // TryGetValue returns a bool; check if successful based on count—also caps out amount if the actual amount goes past the requirement (i.e., displays the lower amount between current count & required count; if there are no items in item count cache, returns zero

                if (questObjective.currentAmount != newAmount)
                    questObjective.currentAmount = newAmount;
            }
        }

        questUI.UpdateQuestUI();
    }
}
