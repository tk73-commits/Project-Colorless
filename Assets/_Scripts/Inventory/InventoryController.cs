using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount; // if you want to upgrade your bag space or something
    public GameObject[] itemPrefabs; // public for testing

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            ItemSlot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<ItemSlot>();
            // Slot gameobject is grabbed and stored here so it can be worked with and populated with items; if you just want the slot, just remove the slot variable and only Instantiate

            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        // look for empty slot
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            ItemSlot slot = slotTransform.GetComponent<ItemSlot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }

        Debug.Log("Inventory is full!");
        return false;
    }

}
