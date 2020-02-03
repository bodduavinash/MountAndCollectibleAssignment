using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemSlot[] itemSlots;
    [HideInInspector] public ItemSlot currentItemSlot;

    public void ShowInventory(List<PickAndMountItem> pickupItems)
    {
        //Hide All item slots in UI
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < pickupItems.Count; i++)
        {
            var slot = itemSlots[i];
            slot.InitSlot(pickupItems[i].mountableType.ToString(), pickupItems[i]);
            slot.gameObject.SetActive(true);
        }

        this.gameObject.SetActive(true);
    }

    public void HideInventory()
    {
        this.gameObject.SetActive(false);
    }

    public ItemSlot GetItemSlotForPickAndMountItem(PickAndMountItem pickAndMountItem)
    {
        foreach(var itemSlot in itemSlots)
        {
            if(itemSlot.itemPickUpType == pickAndMountItem)
            {
                return itemSlot;
            }
        }

        return null;
    }
}
