using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    //public Image portraitImage;
    public Transform choiceContainer; // the choice panel
    public GameObject choiceButtonPrefab; // the button(s)

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowDialogueUI(bool show)
        { dialoguePanel.SetActive(show); }

    public void SetNPCInfo(string npcName) // add Sprite portrait as another parameter if you have it
    {
        nameText.text = npcName;
    }

    public void SetDialogueText(string text)
        { dialogueText.text = text; }

    public void ClearChoices()
    {
        foreach(Transform child in choiceContainer) Destroy(child.gameObject);
    }

    // i don't want it to be bound to clicky buttons, but for now, we're operating like this...
    public GameObject CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
        return choiceButton;
    }
}
