using System;
using System.Threading.Tasks;
using UnityEngine.UI;
using Zenject;

public class ToastMessage : IShowMessage, IHideMessage
{
    [Inject] private UIManager uiManager;

    public void ShowMessage(string message)
    {
        uiManager.toastMessageGameObject.SetActive(true);
        uiManager.toastMessageGameObject.GetComponentInChildren<Text>().text = message;

        RunTask();
    }

    public void HideMessage()
    {
        uiManager.toastMessageGameObject.SetActive(false);
        uiManager.toastMessageGameObject.GetComponentInChildren<Text>().text = "";
    }

    async void RunTask()
    {
        await HideMessageAfterFewSeconds();
    }

    async Task HideMessageAfterFewSeconds()
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        HideMessage();
    }
}
