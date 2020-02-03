using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHandler : MonoBehaviour
{
    private PlayerMovementHandler playerMovementHandler;
    private PlayerMouseMovementHandler playerMouseMovementHandler;
    private PlayerCollectibleHandler playerCollectibleHandler;
    private HoverCraftMovementHandler hoverCraftMovement;

    [Inject]
    private void Construct(PlayerMovementHandler movement, PlayerMouseMovementHandler mouseMovement, PlayerCollectibleHandler collectibleHandler,
                            HoverCraftMovementHandler hoverCraft)
    {
        playerMovementHandler = movement;
        playerMouseMovementHandler = mouseMovement;
        playerCollectibleHandler = collectibleHandler;
        hoverCraftMovement = hoverCraft;
    }

    private void Update()
    {
        hoverCraftMovement.UpdateHoverCraftMovement(transform);
        playerMovementHandler.MovePlayer(transform);
        playerMouseMovementHandler.MouseLook(transform);
        playerCollectibleHandler.RaycastForCollectibleObjects();
    }
}