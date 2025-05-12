using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingPanelsController : MonoBehaviour
{
    public GameObject[] settingPanels;
    // Start is called before the first frame update
    public void Start()
    {
        foreach (var settingPanel in settingPanels)
        {
            settingPanel.SetActive(false);
        }
        settingPanels[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettingPanel()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        
        int index = clickedButton.transform.GetSiblingIndex();
        foreach (var settingPanel in settingPanels)
        {
            settingPanel.SetActive(false);
        }
        settingPanels[index].SetActive(true);
    }

    public void CloseSettingPanel()
    {
        this.transform.parent.gameObject.SetActive(false);   
    }
}
