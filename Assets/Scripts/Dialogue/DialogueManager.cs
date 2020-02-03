using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]GameObject MountDialoguePanel;

    private void Start()
    {
        DisableAllDialoguePanels(false);
    }

    private void DisableAllDialoguePanels(bool show = false)
    {
        MountDialoguePanel.SetActive(show);
    }

    public void ShowMountDialoguePanel(bool show)
    {
        MountDialoguePanel.SetActive(show);
    }
}
