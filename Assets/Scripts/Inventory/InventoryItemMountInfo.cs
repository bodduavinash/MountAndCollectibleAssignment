using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class InventoryItemMountInfo : MonoBehaviour
{
    private PlayerMountHandler playerMountHandler;
    private DialogueManager dialogueManager;
    private ToastMessage toastMessage;
    private InventoryUI inventoryUI;

    [Inject]
    private void Construct(PlayerMountHandler mountHandler, DialogueManager dialogue, ToastMessage toast, InventoryUI inventory)
    {
        playerMountHandler = mountHandler;
        dialogueManager = dialogue;
        toastMessage = toast;
        inventoryUI = inventory;
    }


    private void OnEnable()
    {
        if(inventoryUI.currentItemSlot.itemPickUpType == playerMountHandler.CurrentMountedItemPickUp)
        {
            ClearAndReassignListener(GetComponentInChildren<Button>(), OnUnMountButtonClicked);
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "UnMount";
        }
        else
        { 
            ClearAndReassignListener(GetComponentInChildren<Button>(), OnMountButtonClicked);
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Mount";
        }
    }

    public void OnMountButtonClicked()
    {
        if(inventoryUI.currentItemSlot?.itemPickUpType.mountableType == MountableTypesEnum.TYPE_E)
        {
            toastMessage.ShowMessage($"Item {inventoryUI.currentItemSlot.itemPickUpType.mountableType} not mountable!");
            dialogueManager.ShowMountDialoguePanel(false);
            return;
        }

        //Check if other item slot is being mounted, if true, unmount previous item slot first 
        //and proceed for current item slot mounting
        if(playerMountHandler.CurrentMountedItemPickUp != null)
        {
            OnUnMountItem(inventoryUI.GetItemSlotForPickAndMountItem(playerMountHandler.CurrentMountedItemPickUp));
        }

        inventoryUI.currentItemSlot?.SetItemMounted(true);
        inventoryUI.currentItemSlot?.itemPickUpType.MountItem();
        dialogueManager.ShowMountDialoguePanel(false);

        //After Mounted reassign listener to OnUnMountButtonClicked
        ClearAndReassignListener(GetComponentInChildren<Button>(), OnUnMountButtonClicked);
    }

    public void OnUnMountButtonClicked()
    {
        OnUnMountItem(inventoryUI.currentItemSlot);

        //After UnMounted reassign listener to OnMountButtonClicked
        ClearAndReassignListener(GetComponentInChildren<Button>(), OnMountButtonClicked);
    }

    private void OnUnMountItem(ItemSlot itemSlot)
    {
        itemSlot?.SetItemMounted(false);
        itemSlot?.itemPickUpType.UnMountItem();
        dialogueManager.ShowMountDialoguePanel(false);
    }

    private void ClearAndReassignListener(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}