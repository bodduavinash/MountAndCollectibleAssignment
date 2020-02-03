using System;
using System.Collections.Generic;
using Zenject;

public class PlayerMountHandler : IResetToDefaults
{
    private PickAndMountItem currentMountedItemPickUp;

    public bool isPlayerMountedForType_C = false;
    public bool isPlayerMountedForType_D = false;

    public PickAndMountItem CurrentMountedItemPickUp { get => currentMountedItemPickUp; set => currentMountedItemPickUp = value; }

    [Inject] private PlayerMovementHandler playerMovement;
    [Inject] private PlayerHealthHandler playerHealth;
    [Inject] private PlayerHUD playerHUD;


    public void OnItemMounted(PickAndMountItem mountedItemPickUp)
    {
        CurrentMountedItemPickUp = mountedItemPickUp;
        isPlayerMountedForType_C = false;

        playerHUD.UpdatePlayerMountWithItemCollectedStats(CurrentMountedItemPickUp.ItemCollectedWhileBeingMounted);

        switch (mountedItemPickUp.mountableType)
        {
            case MountableTypesEnum.TYPE_A:
                {
                    playerMovement.PlayerSpeed += GameConstants.playerControlledAmountSpeed;
                    playerHealth.UpdateHealth(GameConstants.healthStaminaAmount_Type_A);
                }
                break;

            case MountableTypesEnum.TYPE_B:
                {
                    playerMovement.PlayerSpeed -= GameConstants.playerControlledAmountSpeed;
                    playerHealth.UpdateHealth(GameConstants.healthStaminaAmount_Type_B);
                }
                break;

            case MountableTypesEnum.TYPE_C:
                {
                    isPlayerMountedForType_C = true;

                    //where L->0 and K->T and increase stamina by adding the value from K to L.
                    // Updating health based on the player speed is updated in PlayerHealth tickable method.
                }
                break;

            case MountableTypesEnum.TYPE_D:
                {
                    //Hovercraft charactestics
                    isPlayerMountedForType_D = true;
                }
                break;

            case MountableTypesEnum.TYPE_E:
                {
                    CurrentMountedItemPickUp = null;
                }
                break;
        }
    }
    public void OnItemUnMounted()
    {
        ResetToDefaults();
    }

    public void ResetToDefaults()
    {
        CurrentMountedItemPickUp = null;
        isPlayerMountedForType_C = false;
        isPlayerMountedForType_D = false;
        playerMovement.ResetToDefaults();
        playerHUD.ResetToDefaults();
    }
}