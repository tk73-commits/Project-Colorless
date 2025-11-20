using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIController : MonoBehaviour
{
    public Transform questListContent;  // contents in scroll list
    public GameObject questEntryPrefab;
    public GameObject objectiveTextPrefab;

    // for testing purposes
    //public Quest testQuest;
    //public int testQuestAmount;
    //private List<QuestProgress> testQuests = new();

    void Start()
    {
        //for (int i = 0; i < testQuestAmount; i++)
        //    testQuests.Add(new QuestProgress(testQuest));
    

        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        // destroy existing quest entries
        foreach (Transform child in questListContent)
            Destroy(child.gameObject);

        // build quest entries
        foreach (var quest in QuestController.Instance.activateQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContent);
            TMP_Text questNameText = entry.transform.Find("QuestNameText").GetComponent<TMP_Text>();    // needs to match the name of the gameobject exactly
            Transform objectiveList = entry.transform.Find("ObjectiveList");

            questNameText.text = quest.quest.name;  //for the quest that's in progress, we get its quest details and then grab the name

            foreach (var objective in quest.objectives)
            {
                GameObject objTextGO = Instantiate(objectiveTextPrefab, objectiveList);
                TMP_Text objText = objTextGO.GetComponent<TMP_Text>();
                objText.text = $"{objective.description} ({objective.currentAmount}/{objective.requiredAmount})";
                // would look like this: Collect 5 Potions (0/5)
            }
        }
    }
}
