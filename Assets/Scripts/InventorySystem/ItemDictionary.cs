using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<ItemBase> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;

    private void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs[i] != null)
                itemPrefabs[i].itemID = i + 1;
        }

        //foreach (ItemBase item in itemPrefabs)
           //itemDictionary[item.itemID] = item.gameObject;
          // this has an error because ItemBase is a ScriptableObject, not a MonoBehaviour
          // so...dang
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if (prefab == null)
            Debug.LogWarning($"Item with ID {itemID} not found in dictionary.");
        return prefab;
    }
}
