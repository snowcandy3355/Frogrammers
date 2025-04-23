using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;
    public float displayTime = 5f;

    void Start()
    {
        tutorialPanel.SetActive(true);
        Invoke("HideTutorial", displayTime);
    }

    void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.anyKeyDown)
            tutorialPanel.SetActive(false);
    }
}