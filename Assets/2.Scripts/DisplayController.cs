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
        WindowFull,
        Window,
    }

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        WindowDropdownSet();
        windowSizeDropdownSet();

    }

    #region 전체화면/창화면 Dropdown
    void WindowDropdownSet()
    {
        List<string> options = new List<string> {
            "전체화면",
            "테두리 없는 창모드",
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
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.MaximizedWindow);
                break;
            case 2:
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
            case ScreenMode.WindowFull:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case ScreenMode.Window:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
        
    }

    #endregion

    void windowSizeDropdownSet()
    {
        List<(int,int)> displaySizes = new List<(int,int)>
        {
            (640,480),
            (800,600),
            (1024,1280),
            (1920,1080),
            (1280,1280),
            (2560,1920),
            
        };
        windowSizeDropdown.ClearOptions();
        //windowSizeDropdown.AddOptions();
        //windowSizeDropdown.onValueChanged.AddListener(index => ChangeFullScreenMode((ScreenMode)index));
        
        
    }
    
}