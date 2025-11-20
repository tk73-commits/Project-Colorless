using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount; // if you want to upgrade your bag space or something
    public GameObject[] itemPrefabs; // public for testing

    public static InventoryController Instance { get; private set; }
    Dictionary<int, int> itemsCountCache = new();
    // int 1 = itemsID; int 2 = count amount
    // will be updated whenever item amounts change inside inventory
    public event Action OnInventoryChanged;
    // event to notify quest system (or any other system that needs to know)
    // QuestController will subscribe to this event and when inventory changes, it pings the QuestController so it knows to update stuff

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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

        RebuildItemCounts();
            // in the case that there have already been items in inventory upon starting the game file
    }

    public void RebuildItemCounts()
    {
        itemsCountCache.Clear();

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            ItemSlot slot = slotTransform.GetComponent<ItemSlot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null)
                    itemsCountCache[item.ID] = itemsCountCache.GetValueOrDefault(item.ID, 0) + item.quantity;
                    // adds item quantity to any existing value in the cache
                    // note that this requires item.cs information in a tutorial i did not watch yet
                }
            }

            // tells anything subscribed to this event that the inventory has been updated
            // only invoked if this is subscribed to and set to an event
            OnInventoryChanged?.Invoke();
        }

    // returns items count cache; able to call this whenever you want to check item counts for quests
    public Dictionary<int, int> GetItemCount() => itemsCountCache;

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
                RebuildItemCounts();
                return true;
            }
        }

        Debug.Log("Inventory is full!");
        return false;
    }

    public void RemoveItemsFromInventory(int itemID, int amountToRemove)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            if (amountToRemove <= 0) break;

            ItemSlot slot = slotTransform.GetComponent<ItemSlot>();
            // do you have a current item in the slot? if so, grab it out w/ is Item && if itemID matches the one passed in — the correct item you want to remove
            if (slot?.currentItem?.GetComponent<Item>() is Item item && item.ID == itemID)
            {
                int removed = Mathf.Min(amountToRemove, item.quantity);
                item.RemoveFromStack(removed);  // this is a function in the Item script
                amountToRemove -= removed;

                if (item.quantity == 0)
                {
                    Destroy(slot.currentItem);
                    slot.currentItem = null;
                }
            }
        }

        RebuildItemCounts();
    }
}
