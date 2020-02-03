using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCollectibleHandler
{
    private Vector3 centerPoint;
    private float raycastHitDistance = 2.0f;
    private GameObject colliderHitGameObject;
    private Ray rayToCollectibleObject;
    private RaycastHit raycastHitInfo;

    [Inject]private CollectiblesMessage collectiblesMessage;
    [Inject]private PlayerMountHandler playerMountController;

    public PlayerCollectibleHandler()
    {
        //collectiblesMessage = new CollectiblesMessage();
    }

    public void RaycastForCollectibleObjects()
    {
        centerPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

        rayToCollectibleObject = Camera.main.ScreenPointToRay(centerPoint);

        colliderHitGameObject = null;

        if (Physics.Raycast(rayToCollectibleObject, out raycastHitInfo, raycastHitDistance))
        {
            colliderHitGameObject = raycastHitInfo.collider.gameObject;

            if (colliderHitGameObject.GetComponent<PickAndMountItem>())
            {
                collectiblesMessage?.ShowMessage(null);

                //Check the input keyboard E button pressed or not.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    raycastHitInfo.collider.gameObject.SetActive(!colliderHitGameObject.GetComponent<PickAndMountItem>().PickUpItem());
                }
            }
        }
        else
        {
            collectiblesMessage?.HideMessage();
        }
    }

    public bool CanCollectCollectibleItem(CollectibleTypesEnum itemType)
    {
        bool canCollect = false;

        if (playerMountController.CurrentMountedItemPickUp == null)
        {
            return !canCollect;
        }

        switch (playerMountController.CurrentMountedItemPickUp.mountableType)
        {
            case MountableTypesEnum.TYPE_A:
                if (itemType == CollectibleTypesEnum.TYPE_X || itemType == CollectibleTypesEnum.TYPE_Y)
                    canCollect = true;
                break;

            case MountableTypesEnum.TYPE_B:
                if (itemType == CollectibleTypesEnum.TYPE_Z)
                    canCollect = true;
                break;

            case MountableTypesEnum.TYPE_C:
                if (itemType == CollectibleTypesEnum.TYPE_X || itemType == CollectibleTypesEnum.TYPE_Y || itemType == CollectibleTypesEnum.TYPE_W)
                    canCollect = true;
                break;

            case MountableTypesEnum.TYPE_D:
                if (itemType == CollectibleTypesEnum.TYPE_Z || itemType == CollectibleTypesEnum.TYPE_W)
                    canCollect = true;
                break;
        }

        if (canCollect)
        {
            playerMountController.CurrentMountedItemPickUp.ItemCollectedWhileBeingMounted++;
        }

        return canCollect;
    }
}