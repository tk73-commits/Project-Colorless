using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : ScriptableObject
{
    // base class for all items in the game
    public int itemID;  // used for save data and quests
    public string itemName;
    public string itemDescription;
    public Image itemIcon;  // does this need to be Sprite or Image??
}
