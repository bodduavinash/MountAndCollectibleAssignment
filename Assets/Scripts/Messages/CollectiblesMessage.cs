
using Zenject;

public class CollectiblesMessage : IShowMessage, IHideMessage
{
    [Inject] private UIManager uiManager;

    public void ShowMessage(string message)
    {
        uiManager.CollectibleMessage.SetActive(true);
    }

    public void HideMessage()
    {
        uiManager.CollectibleMessage.SetActive(false);
    }
}
