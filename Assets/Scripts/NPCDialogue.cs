using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    //public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public bool[] endDialogueLines; // marks where dialogue ends
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    //public AudioClip voiceSound;
    //public float voicePitch = 1f;

    public DialogueChoice[] choices;
}

[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex;   // line where choices appear
    public string[] choices;    // player response options
    public int[] nextDialogueIndexes;   // where choice leads
}
