using UnityEngine;
using Zenject;

public class PlayerHealthHandler : ITickable
{
    private float health = GameConstants.PlayerMaxHealth;
    [Inject] private UserInputManager inputManager;
    [Inject] private PlayerMountHandler playerMount;
    [Inject] private PlayerMovementHandler playerMovement;
    [Inject] private PlayerHUD playerHUD;

    public float Health
    {
        get
        {
            return health;
        }
    }

    public void UpdateHealth(float healthStamina)
    {
        health += healthStamina;

        health = Mathf.Clamp(health, 0, GameConstants.PlayerMaxHealth);
        playerHUD.UpdatePlayerHealthStats(Mathf.CeilToInt(health));
    }

    void ITickable.Tick()
    {
        //where L->0 and K->T and increase stamina by adding the value from K to L i.e. T->0.
        if (playerMount != null && playerMount.isPlayerMountedForType_C && inputManager.InputKeyPressed
            && playerMovement.PlayerSpeed > 0)
        {
            UpdateHealth(Mathf.Abs(playerMovement.PlayerSpeed * Time.deltaTime));
        }
    }
}
