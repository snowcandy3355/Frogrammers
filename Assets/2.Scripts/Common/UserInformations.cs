using UnityEngine;

public static class UserInformations
{
    private const string DISPLAY_STATE = "Display State";
    private const string BGM_STATE = "BGM State";
    private const string BGM_VOLUME = "BGM Volume";
    private const string SE_STATE = "BGM Selected";
    private const string SE_VOLUME = "SE Volume";
    

    #region 디스플레이 세팅 로컬

    public static int DisplayState
    {
        get
        { return PlayerPrefs.GetInt(DISPLAY_STATE, 0);}
        set
        { PlayerPrefs.SetInt(DISPLAY_STATE, value); }
    }
    

    #endregion
    #region 사운드 세팅 로컬

    // 사운드 세팅 로컬에 저장
    public static int BgmState
    {
        get
        { return PlayerPrefs.GetInt(BGM_STATE, 1); }
        set
        { PlayerPrefs.SetInt(BGM_STATE, value); }
    }

    public static float BgmVolume
    {
        get
        { return PlayerPrefs.GetFloat(BGM_VOLUME, 1f); }
        set
        { PlayerPrefs.SetFloat(BGM_VOLUME, value); }
    }
    public static int SeState
    {
        get
        { return PlayerPrefs.GetInt(SE_STATE, 1); }
        set
        { PlayerPrefs.SetInt(SE_STATE, value); }
    }
    public static float SeVolume
    {
        get
        { return PlayerPrefs.GetFloat(SE_VOLUME, 1f); }
        set
        { PlayerPrefs.SetFloat(SE_VOLUME, value); }
    }

    #endregion
    
    
}