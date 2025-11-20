using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupUIController : MonoBehaviour
{
    // create instance of this script; enables other scripts to access this script without setting a reference to it—this script will instead call out to others
    public static ItemPopupUIController Instance { get; private set; }

    public GameObject popupPrefab;
    public int maxPopups = 5;
    public float popupDuration = 3f;

    private readonly Queue<GameObject> activePopups = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple ItemPickupUIManager instances detected; destroying extra(s).");
            Destroy(gameObject);
        }
    }

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();       // this needs to be the exact name you use for the item icon game object; also, checks to see if it's null or not before grabbing image component
        if (itemImage)
        {
            itemImage.sprite = itemIcon;
        }

        activePopups.Enqueue(newPopup);
        // when picking up items that exceed the max number of popups, destroy the bottom one and add the most recent one to the top
        if (activePopups.Count > maxPopups)
        {
            Destroy(activePopups.Dequeue());
        }

        // call enumerator to fade out and destroy popups
        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration);
        // crash prevention
        if (popup == null) yield break;

        // if popup isn't destroyed and will fade out
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for (float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            // as time passes, keep checking to see that popups are rendered null
            if (popup == null) yield break;
            canvasGroup.alpha = 1f - timePassed;    // gradually fades out image
            yield return null;
        }

        Destroy(popup);
    }

}
