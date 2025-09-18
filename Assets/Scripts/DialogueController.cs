using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    //public Image portraitImage;

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
}
