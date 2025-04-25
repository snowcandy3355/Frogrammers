using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointUI : MonoBehaviour
{
    public GameObject savePanel;
    public float displayTime = 2f;

    public void ShowSaveMessage()
    {
        savePanel.SetActive(true);
        CancelInvoke();
        Invoke("HidePanel", displayTime);
    }

    void HidePanel()
    {
        savePanel.SetActive(false);
    }
}