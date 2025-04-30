using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    private GameObject displayController;
    private void Start()
    { 
        SoundManager.Instance.bgmType = BGMType.Main;
        SoundManager.Instance.PlayBGM();
        StartDisplaySetting(UserInformations.DisplayState);
    }
    
    
    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit!!");
        
        int bgmState = Convert.ToInt32(SoundManager.Instance.bgmOn);
        int seState = Convert.ToInt32(SoundManager.Instance.seOn);

        UserInformations.BgmState = bgmState;
        UserInformations.SeState = seState;
        UserInformations.BgmVolume = SoundManager.Instance.bgmAudioSource.volume;
        UserInformations.SeVolume = SoundManager.Instance.seAudioSource.volume;
    }


    void StartDisplaySetting(int index)
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.MaximizedWindow);
                break;
            case 2:
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
                break;
        }
    }
}