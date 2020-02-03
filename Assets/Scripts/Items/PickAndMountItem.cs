using UnityEngine;
using Zenject;

public class PickAndMountItem : MonoBehaviour
{
    public CollectibleTypesEnum collectibleType;
    public MountableTypesEnum mountableType;

    private int itemCollectedWhileBeingMounted = 0;

    private PlayerMountHandler playerMountController;
    private PlayerCollectibleHandler playerCollectibleController;
    private InventoryHandler inventory;
    private PlayerHUD playerHUD;
    private ToastMessage toastMessage;

    [Inject]
    private void Construct(PlayerCollectibleHandler collectibleController, PlayerMountHandler mountController, InventoryHandler inv,
                            PlayerHUD HUD, ToastMessage message)
    {
        playerCollectibleController = collectibleController;
        playerMountController = mountController;
        inventory = inv;
        playerHUD = HUD;
        toastMessage = message;
    }

    public int ItemCollectedWhileBeingMounted
    {
        get => itemCollectedWhileBeingMounted;
        set
        {
            itemCollectedWhileBeingMounted = value;
            playerHUD.UpdatePlayerMountWithItemCollectedStats(value);
        }
    }

    private void Start()
    {
        collectibleType = Utilities.GetRandomEnumValue(collectibleType);
        mountableType = Utilities.GetRandomEnumValue(mountableType);
    }

    public bool PickUpItem()
    {
        if(collectibleType == CollectibleTypesEnum.TYPE_Q)
        {
            toastMessage.ShowMessage($"Cannot pickup item of type: {collectibleType.ToString()}!");
            return false;
        }
        else if (!playerCollectibleController.CanCollectCollectibleItem(collectibleType))
        {
            toastMessage.ShowMessage($"Cannot pickup item of type: {collectibleType.ToString()} cause Type {mountableType} is mounted!");
            return false;
        }

        Debug.Log($"Pickup item is {collectibleType.ToString()}");
        return inventory.AddItemToInventory(this);
    }

    public void DropItem()
    {
        inventory.RemoveItemInventory(this);
    }

    public bool MountItem()
    {
        if (mountableType == MountableTypesEnum.TYPE_E)
        {            
            toastMessage.ShowMessage($"Cannot mount item of type: {mountableType.ToString()}!");
            return false;
        }

        playerMountController.OnItemMounted(this);

        toastMessage.ShowMessage($"Item mounted of type: {mountableType.ToString()}!");
        Debug.Log($"Mounted item is {mountableType.ToString()}");
        return true;
    }

    public bool UnMountItem()
    {
        playerMountController.OnItemUnMounted();

        return true;
    }
}