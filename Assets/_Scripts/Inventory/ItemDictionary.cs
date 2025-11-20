using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary; // store itemPrefab GameObjects by their int ID

    private void Awake()    // use awake instead of start because it loads sooner
    {
        itemDictionary = new Dictionary<int, GameObject>();

        // creates item IDs
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs[i] != null)
                itemPrefabs[i].ID = i + 1;
        }

        foreach (Item item in itemPrefabs)
        {
            itemDictionary[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if (prefab == null)
            Debug.LogWarning($"Item with ID {itemID} not found in dictionary");

        return prefab;
    }
}
