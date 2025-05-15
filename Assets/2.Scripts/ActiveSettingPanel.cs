using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSettingPanel : MonoBehaviour
{
    [SerializeField] private GameObject SettingPanel;
    // Start is called before the first frame update
    void Start()
    {
        SettingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
        /*SettingPanel.SetActive(true);
        SettingPanel.GetComponentInChildren<SettingPanelsController>().Start();*/
    }
}
