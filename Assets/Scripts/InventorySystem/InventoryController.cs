using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;    // public for testing

    private void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
    }

    // this is used for save functionality, which I will not implement yet until I know how it works ;;
    /**
    public List<InventorySaveData> GetInventoryItems()
    {
            List<InventorySaveData> invData = new List<InventorySaveData>();

            foreach(Transform slotTransform in inventoryPanel.transform)
            {
                Slot slot = slotTransform.GetComponent<Slot>();
                if(slot.currentItem != null)
                {
                    Item item = slot.currentItem.GetComponent<Item>();
                    invData.Add(new InventorySaveData { itemID = item.ID, slot Index = slotTransform.GetSiblingIndex() });
                }
            }
            return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
            // clear inventory panel - avoid duplicates
            foreach(Transform child in inventoryPanel.transform)
                Destroy(gameObject);

            // create new slots
            for(int i = 0; i < slotCount; i++)
            {
                Instantiate(slotPrefab, inventoryPanel.transform);
            }

            // populate slots with saved items
            foreach(InventorySaveData data in inventorySaveData)
            {
                if(data.slotIndex < slotCount)
                {
                    Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                    GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                    if(itemPrefab != null)
                    {
                        GameObject item = Instantiate(itemPrefab, slot.transform);
                        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        slot.currentItem = item;
                    }
                }
            }
    }
    **/

    public bool AddItem(GameObject itemPrefab)
    {
        // look for empty slot
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                slot.currentItem = newItem;
                return true;
            }    
        }

        Debug.Log("Inventory is full");
        return false;
    }
}
