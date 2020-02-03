using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovementHandler
{    
    private float playerSpeed;

    [Inject] private UserInputManager inputManager;
    [Inject] private PlayerMountHandler playerMount;
    [Inject] private PlayerHUD playerHUD;

    public float PlayerSpeed
    {
        get => playerSpeed;
        set
        {
            playerSpeed = value;
            playerHUD.UpdatePlayerSpeedStats(value);
        }
    }

    public void MovePlayer(Transform transform)
    {
        if(!inputManager.CheckPlayerInput(transform))
        {
            return;
        }

        if(playerMount.isPlayerMountedForType_C)
        {
            SetPlayerSpeed();
        }
        else
        {
            PlayerSpeed = GameConstants.defaultPlayerSpeedAmount;
        }

        transform.position += inputManager.moveDirection * PlayerSpeed * Time.deltaTime;
    }

    private PlayerMovementModes GetCurrentPlayerMovementMode()
    {
        return inputManager.verticalDirection >= 0 ? PlayerMovementModes.FORWARD : (inputManager.verticalDirection <= 0 ? PlayerMovementModes.REVERSE : PlayerMovementModes.IDLE);
    }

    private void SetPlayerSpeed()
    {
        switch (GetCurrentPlayerMovementMode())
        {
            case PlayerMovementModes.FORWARD:
            {
                    PlayerSpeed += GameConstants.accelerationAmount;//Mathf.Lerp(PlayerSpeed, GameConstants.maxSpeedAmount, Time.deltaTime);
                    
                    if (PlayerSpeed >= GameConstants.maxSpeedAmount)
                        PlayerSpeed = GameConstants.maxSpeedAmount;
            }
            break;

            case PlayerMovementModes.REVERSE:
            {
                    PlayerSpeed -= GameConstants.deAccelerationAmount; //Mathf.Lerp(PlayerSpeed, GameConstants.minSpeedAmount, Time.deltaTime);
                    if (PlayerSpeed <= 0)
                        PlayerSpeed = 0;
                }
            break;

            /*case PlayerMovementModes.IDLE:
            {
                    PlayerSpeed -= GameConstants.deAccelerationAmount;//Mathf.Lerp(PlayerSpeed, 0, Time.deltaTime);
                    if (PlayerSpeed <= 0)
                        PlayerSpeed = 0;
            }
            break;*/
        }

    }

    public void ResetToDefaults()
    {
        PlayerSpeed = GameConstants.defaultPlayerSpeedAmount;
    }
}
