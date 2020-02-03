using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIManager : MonoBehaviour
{
    public GameObject CollectibleMessage;
    public GameObject toastMessageGameObject;
    public InventoryUI inventoryUI;

    [Inject] private InventoryHandler inventory;

    public void ShowInventry()
    {
        inventoryUI.ShowInventory(inventory.GetInventoryLists());
    }
}