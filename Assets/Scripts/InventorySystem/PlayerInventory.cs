using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    private InventoryController inventoryController;

    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                // add to inventory
                bool itemAdded = inventoryController.AddItem(collision.gameObject);
                if (itemAdded)
                    Destroy(collision.gameObject);
            }
        }
    }
}
