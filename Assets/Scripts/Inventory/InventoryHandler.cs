using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryHandler
{
    private List<PickAndMountItem> inventoryLists;
    [Inject] private PlayerHUD playerHUD;
    [Inject] private ToastMessage toastMessage;

    private InventoryHandler()
    {
        inventoryLists = new List<PickAndMountItem>(GameConstants.MaxInventoryList);
    }

    public bool AddItemToInventory(PickAndMountItem item)
    {
        if(inventoryLists.Count >= GameConstants.MaxInventoryList)
        {
            toastMessage.ShowMessage("Max items reached in inventory!!");
            return false;
        }

        inventoryLists.Add(item);

        playerHUD.UpdatePlayerCollectedStats(inventoryLists.Count);

        return true;
    }

    public bool RemoveItemInventory(PickAndMountItem item)
    {
        if(inventoryLists.Count <= 0)
        {
            toastMessage.ShowMessage("No items to remove from inventory, as it is empty!!");
            return false;
        }

        inventoryLists.Remove(item);

        playerHUD.UpdatePlayerCollectedStats(inventoryLists.Count);

        return true;
    }

    public List<PickAndMountItem> GetInventoryLists()
    {
        return inventoryLists;
    }

    public float GetInventoryListsCount()
    {
        return inventoryLists.Count;
    }
}