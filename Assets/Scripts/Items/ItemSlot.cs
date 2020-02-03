using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

public class ItemSlot : MonoBehaviour
{
    private string itemName;
    private GameObject mountedItemIconGameObject;// need to add item icons here
    [HideInInspector] public PickAndMountItem itemPickUpType;

    [Inject] private PlayerMountHandler playerMountController;
    [Inject] private DialogueManager dialogueManager;
    [Inject] private InventoryUI inventoryUI;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnItemSlotButtonClicked);
        gameObject.transform.Find("CloseButton")?.GetComponent<Button>().onClick.AddListener(OnItemCloseButtonClicked);        
    }

    public void InitSlot(string name, PickAndMountItem pickedItem)
    {
        itemName = name;
        itemPickUpType = pickedItem;        

        GetComponentInChildren<Text>().text = name;
    }

    private void ResetSlot()
    {
        itemName = "";
        mountedItemIconGameObject = null;
        itemPickUpType = null;
    }

    public void OnItemSlotButtonClicked()
    {
        inventoryUI.currentItemSlot = this;

        dialogueManager.ShowMountDialoguePanel(true);
    }

    public void OnItemCloseButtonClicked()
    {
        //unmount current item first if already mounted.
        if (itemPickUpType == playerMountController.CurrentMountedItemPickUp)
        {
            itemPickUpType?.UnMountItem();
        }

        itemPickUpType?.DropItem();

        gameObject.SetActive(false);

        ResetSlot();
    }

    public void SetItemMounted(bool isItemMounted)
    {
        mountedItemIconGameObject = transform.Find("MountedImage").gameObject;
        mountedItemIconGameObject.SetActive(isItemMounted);
    }
}