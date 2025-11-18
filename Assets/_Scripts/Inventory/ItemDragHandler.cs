using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent; // this is for snapping back the item to the slot if it's dragged somewhere other than another slot
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // puts above other canvases
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; // semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // follows the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // can click on it again
        canvasGroup.alpha = 1f;

        ItemSlot dropSlot = eventData.pointerEnter?.GetComponent<ItemSlot>(); // slot where item dropped
                                                                      // if your mouse enters a slot (pointerEnter)...
        if (dropSlot == null) // if there's no slot, it'll check for an item instead
        {
            GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                dropSlot = item.GetComponentInParent<ItemSlot>();
            }
        }
        ItemSlot originalSlot = originalParent.GetComponent<ItemSlot>();

        if (dropSlot != null)
        {
            // if the dropSlot is filled with something, it swaps the items
            if (dropSlot.currentItem != null)
            {
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                // snaps into the middle
            }
            else
            { originalSlot.currentItem = null; }

            // move item into dropSlot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            // no slot under drop point
            transform.SetParent(originalParent);
        }

        // anchors it in the middle of the slot
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
