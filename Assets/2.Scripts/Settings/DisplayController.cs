using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    public TMP_Dropdown windowDropdown; //전체화면, 창화면
    public TMP_Dropdown windowSizeDropdown;// 화면 해상도

    private int screenWidth;
    private int screenHeight;

    public enum ScreenMode
    {
        FullScreenWindow,
        Window,
    }

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        WindowDropdownSet();
        WindowSizeDropdownSet();

    }

    #region 전체화면/창화면 Dropdown
    void WindowDropdownSet()
    {
        List<string> options = new List<string> {
            "전체화면",
            "창모드"
        };
        
        
        windowDropdown.ClearOptions();
        windowDropdown.AddOptions(options);
        windowDropdown.value = UserInformations.DisplayState;
        windowDropdown.onValueChanged.AddListener(index =>
        {
            UserInformations.DisplayState = index;
            Debug.Log(UserInformations.DisplayState);
            ChangeFullScreenMode((ScreenMode)index);
        });

        
        switch (windowDropdown.value)
        {
            case 0:
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
                break;
        }
        


    }
    /// <summary>
    /// 스크린의 전체 스크린 모드를 변경합니다.
    /// </summary>
    /// <param name="mode">변경할 스크린 모드</param>
    private void ChangeFullScreenMode(ScreenMode mode)
    {
        switch (mode)
        {
            case ScreenMode.FullScreenWindow:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case ScreenMode.Window:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
        
    }

    #endregion

    void WindowSizeDropdownSet()
    {
        List<(int,int)> displaySizes = new List<(int,int)>
        {
            (640,480),
            (800,600),
            (1024,768),
            (1920,1080),
            (2560,1920),
            
        };
        List<string> displaySizesString = new List<string>();
        foreach ((int, int) displaySize in displaySizes)
        {
            displaySizesString.Add(displaySize.ToString());
        }
        

        windowSizeDropdown.ClearOptions();
        windowSizeDropdown.AddOptions(displaySizesString);
        windowSizeDropdown.value = UserInformations.DisplaySizeState;
        windowSizeDropdown.onValueChanged.AddListener(index =>
        {
            UserInformations.DisplaySizeState = index;
            Debug.Log(UserInformations.DisplaySizeState);
            ChangeWindowSize(index,displaySizes);
        });
        
        switch (windowSizeDropdown.value)
        {
            case 0:
                Screen.SetResolution(displaySizes[windowSizeDropdown.value].Item1, displaySizes[windowSizeDropdown.value].Item2, UserInformations.DisplayState==0? FullScreenMode.FullScreenWindow:FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(displaySizes[windowSizeDropdown.value].Item1, displaySizes[windowSizeDropdown.value].Item2, UserInformations.DisplayState==0? FullScreenMode.FullScreenWindow:FullScreenMode.Windowed);
                break;
            case 2:
                Screen.SetResolution(displaySizes[windowSizeDropdown.value].Item1, displaySizes[windowSizeDropdown.value].Item2, UserInformations.DisplayState==0? FullScreenMode.FullScreenWindow:FullScreenMode.Windowed);
                break;
            case 3:
                Screen.SetResolution(displaySizes[windowSizeDropdown.value].Item1, displaySizes[windowSizeDropdown.value].Item2, UserInformations.DisplayState==0? FullScreenMode.FullScreenWindow:FullScreenMode.Windowed);
                break;
            case 4:
                Screen.SetResolution(displaySizes[windowSizeDropdown.value].Item1, displaySizes[windowSizeDropdown.value].Item2, UserInformations.DisplayState==0? FullScreenMode.FullScreenWindow:FullScreenMode.Windowed);
                break;
            
            
        }
    }
    private void ChangeWindowSize(int index,List<(int,int)> displaySizes)
    {
        Screen.SetResolution(displaySizes[index].Item1, displaySizes[index].Item2, UserInformations.DisplayState==0? FullScreenMode.FullScreenWindow:FullScreenMode.Windowed);
    }
    
}