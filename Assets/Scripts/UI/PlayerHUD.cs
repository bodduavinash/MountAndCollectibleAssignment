using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHUD : MonoBehaviour, IResetToDefaults
{
    public Slider playerHealthSlider;
    public Text playerHealthText;
    public Text playerSpeedText;
    public Text playerCollectedText;
    public Text playerCollectedWhileMountedText;

    private PlayerMountHandler playerMountController;

    [Inject]
    private void Construct(PlayerMountHandler mountController)
    {
        playerMountController = mountController;
    }

    public void UpdatePlayerHealthStats(float health)
    {
        playerHealthSlider.value = health;

        SetStatusText(playerHealthText, $"Health: {health}");
    }

    public void UpdatePlayerSpeedStats(float speed)
    {
        SetStatusText(playerSpeedText, $"Speed: {speed}");
    }

    public void UpdatePlayerCollectedStats(float collectedObjects)
    {
        SetStatusText(playerCollectedText, $"Collected: {collectedObjects}");
    }

    public void UpdatePlayerMountWithItemCollectedStats(float mountWithCollectedObjects)
    {
        var text = playerMountController.CurrentMountedItemPickUp ? $"Mount and Collected: {mountWithCollectedObjects}" : "";
        SetStatusText(playerCollectedWhileMountedText, text);
    }

    private void SetStatusText(Text texting, string value)
    {
        texting.text = value;
    }

    public void ResetToDefaults()
    {
        var resetValue = 0;
        UpdatePlayerMountWithItemCollectedStats(resetValue);
    }
}