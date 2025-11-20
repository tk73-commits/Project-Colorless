using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public int quantity = 1;

    private TMP_Text quantityText;

    private void Awake()
    {
        quantityText = GetComponentInChildren<TMP_Text>();
        UpdateQuantityDisplay();
    }

    public void UpdateQuantityDisplay()
    {
        // is the item qt greater than 1? if so, set the quantity text; otherwise, set quantity text as null
        if (quantityText != null)
        {
            quantityText.text = quantity > 1 ? quantity.ToString() : "";
        }
    }

    public void AddToStack(int amount = 1)
    {
        quantity += amount;
        UpdateQuantityDisplay();
    }

    public int RemoveFromStack(int amount = 1)
    {
        int removed = Mathf.Min(amount, quantity);
        quantity -= removed;
        UpdateQuantityDisplay();
        return removed;
    }

    // clone item w/ new quantity for splits
    public GameObject CloneItem(int newQuantity)
    {
        GameObject clone = Instantiate(gameObject);
        Item cloneItem = clone.GetComponent<Item>();
        cloneItem.quantity = newQuantity;
        cloneItem.UpdateQuantityDisplay();
        return clone;
    }

    // virtual, so you can extend this and change it as you like if you want different behaviors to happen upon pickup
    public virtual void Pickup()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        // note: if you don't have an image, use GetComponent<SpriteRenderer>() instead
        if (ItemPopupUIController.Instance != null)
        {
            ItemPopupUIController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }
}
