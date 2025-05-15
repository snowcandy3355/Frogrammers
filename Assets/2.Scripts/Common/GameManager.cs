using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    None,
    Gameplay,
}
public class GameManager : Singleton<GameManager>
{
    public GameState gameState = GameState.None;
    private GameObject displayController;
    private void Start()
    { 
        gameState = GameState.None;
        SoundManager.Instance.bgmType = BGMType.Main;
        SoundManager.Instance.PlayBGM();
        StartDisplaySetting(UserInformations.DisplayState, UserInformations.DisplaySizeState);
    }

    private void Update()
    {
        if (gameState == GameState.Gameplay)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = GameState.None;
                SceneManager.LoadScene("MainMenu");
            }
            //Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else if (gameState == GameState.None)
        {
            Cursor.lockState = CursorLockMode.None;
        }
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


    void StartDisplaySetting(int index, int index2)
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        int screenWidthSize = Screen.width;
        int screenHeightSize = Screen.height;
        switch (index2)
        {
            case 0:
                screenWidthSize = 640;
                screenHeightSize = 480;
                break;
            case 1:
                screenWidthSize = 800;
                screenHeightSize = 600;
                break;
            case 2:
                screenWidthSize = 1024;
                screenHeightSize = 768;
                break;
            case 3:
                screenWidthSize = 1920;
                screenHeightSize = 1080;
                break;
            case 4:
                screenWidthSize = 2560;
                screenHeightSize = 1920;
                break;

        }
        switch (index)
        {
            case 0:
                Screen.SetResolution(screenWidthSize, screenHeightSize, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(screenWidthSize, screenHeightSize, FullScreenMode.Windowed);
                break;
        }

        
    }
}