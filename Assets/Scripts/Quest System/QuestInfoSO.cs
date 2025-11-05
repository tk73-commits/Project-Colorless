using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }
        // will use this to reference any specific quest across the entire system; id needs to be unique to each quest

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public int levelRequirement;    // if applicable
    public QuestInfoSO[] questPrerequisites;    // references corresponding quest info SO for any prereq quest

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int experienceReward;

    // ensures the ID is always the name of the Scriptable Object asset
    private void OnValidate()
    {
		id = this.name;
		UnityEditor.EditorUtility.SetDirty(this);
    }
}

