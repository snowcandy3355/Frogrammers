using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    
    private void Start()
    { 
        SoundManager.Instance.bgmType = BGMType.Main;
        SoundManager.Instance.PlayBGM();
 
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
    
}