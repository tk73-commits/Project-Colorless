using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    public static RewardsController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GiveQuestReward(Quest quest)
    {
        // no rewards? leave
        if (quest?.questRewards == null) return;

        foreach (var reward in quest.questRewards)
        {
            switch (reward.type)
            {
                case RewardType.Item:
                    //give item reward; values set up in Scriptable Object
                    GiveItemReward(reward.rewardID, reward.amount);
                    break;
                case RewardType.Gold:
                    //give gold reward
                    break;
                case RewardType.Experience:
                    //give EXP reward
                    break;
                case RewardType.Custom:
                    //give custom reward
                    break;
            }
        }
    }

    public void GiveItemReward(int itemID, int amount)
    {
        var itemPrefab = FindAnyObjectByType<ItemDictionary>()?.GetItemPrefab(itemID);

        if (itemPrefab == null) return;

        for (int i = 0; i < amount; i++)
        {
            // .AddItem returns a Bool; if true, item is successfully added; if false, couldn't add item (inv was full maybe)
            if (!InventoryController.Instance.AddItem(itemPrefab))
            {
                GameObject dropItem = Instantiate(itemPrefab, transform.position = Vector3.down, Quaternion.identity);
                //dropItem.GetComponent<BounceEffect>().StartBounce();    // she has a bounce effect on item prefabs
            }
            else
            {
                // show notif popup of item pickup
                itemPrefab.GetComponent<Item>().Pickup();
            }
        }

    }
}
