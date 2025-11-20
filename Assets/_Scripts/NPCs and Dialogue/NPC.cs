using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    private DialogueController dialogueUI;

    private int dialogueIndex; //keep track of which line of dialogue you're on
    private bool isTyping, isDialogueActive;

    private enum QuestState { NotStarted, InProgress, Completed }
    // for NPCs that don't give quests, you just ignore all the Quest bits of this process
        // so just don't tick the Give Quest button on any choices
    private QuestState questState = QuestState.NotStarted;  // default state

    void Start()
    {
        dialogueUI = DialogueController.Instance;
    }

    public bool CanInteract()
    {
        // if dialogue box is inactive, then NPC can be interacted with
        return !isDialogueActive;
    }

    public void Interact()
    {
        // if a paused state / menu is open, dialogue will not appear
        if (dialogueData == null)
            return;

        if (isDialogueActive)
            NextLine();
        else
            StartDialogue();
    }

    void StartDialogue()
    {
        // Sync with quest data – whether the quest is started or completed, etc.; need to know from QuestController
        SyncQuestState();

        // Set dialogue line based on questState
        if (questState == QuestState.NotStarted)
            dialogueIndex = 0;
        else if (questState == QuestState.InProgress)
            dialogueIndex = dialogueData.questInProgressIndex;
        else if (questState == QuestState.Completed)
            dialogueIndex = dialogueData.questCompletedIndex;

        isDialogueActive = true;
        //dialogueIndex = 0;    removed according to notes

        dialogueUI.SetNPCInfo(dialogueData.npcName);
        dialogueUI.ShowDialogueUI(true);

        // pause the game so the player can't move while NPC is talking
        PauseController.SetPause(true);

        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (isTyping)
        {
            // skip typing animation and show full line
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        
        // Clear choices
        dialogueUI.ClearChoices();

        // Check endDialogueLines
        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        // Check if choices & display
        foreach(DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
                // display choices
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // is there another line to go to? if so, then:
            DisplayCurrentLine();
        }
        else // no more lines or not typing to autocomplete
            EndDialogue();
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for(int i = 0;  i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            bool givesQuest = choice.givesQuest[i];
            dialogueUI.CreateChoiceButton(choice.choices[i], ()  => ChooseOption(nextIndex, givesQuest));
        }
    }

    void ChooseOption(int nextIndex, bool givesQuest)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();

        if (givesQuest)
        {
            QuestController.Instance.AcceptQuest(dialogueData.quest);
            questState = QuestState.InProgress;
        }
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText(""); // clean it out

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            // as the code executes, it adds each letter of the line to the text
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            // takes a short pause for every letter so the dialogue is not speedran
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            // is that index ticked to auto-progress? if so...
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        if (questState == QuestState.Completed && !QuestController.Instance.IsQuestHandedIn(dialogueData.quest.questID))
        {
            // complete & hand in the quest only when it has not already been handed in
            HandleQuestCompletion(dialogueData.quest);
        }

        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        PauseController.SetPause(false);
    }

    private void SyncQuestState()
    {
        if (dialogueData.quest == null) return;     // if there's no quest in the dialogue data, return out of the function

        string questID = dialogueData.quest.questID;

        if (QuestController.Instance.IsQuestComplete(questID) || QuestController.Instance.IsQuestHandedIn(questID))
            questState = QuestState.Completed;

        // add completing quest & handing in later
        else if (QuestController.Instance.IsQuestActive(questID))
            questState = QuestState.InProgress;
        else
            questState = QuestState.NotStarted;
    }

    void HandleQuestCompletion(Quest quest)
    {
        RewardsController.Instance.GiveQuestReward(quest);
        QuestController.Instance.HandInQuest(quest.questID);
    }
}
