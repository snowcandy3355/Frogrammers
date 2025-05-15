using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BGMType
{
    None,
    Main,
    Game,
}
public enum SEType
{
    None,
    Click,
    GameStart,
    GameEnd,
}
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgmAudioSource;
    public AudioSource subBgmAudioSource;
    public AudioSource seAudioSource;
    public AudioClip[] bgmSounds;
    public AudioClip[] seSounds;
    public BGMType bgmType;
    public SEType seType;
    public bool bgmOn;
    public bool seOn;
    private void Start()
    {
        /*bgmOn = true;
        seOn = true;*/
        // 사운드 상태 로컬저장
        bgmOn = Convert.ToBoolean(UserInformations.BgmState);
        seOn = Convert.ToBoolean(UserInformations.SeState);
        bgmAudioSource.volume = Convert.ToSingle(UserInformations.BgmVolume);
        subBgmAudioSource.volume = Convert.ToSingle(UserInformations.BgmVolume);
        seAudioSource.volume = Convert.ToSingle(UserInformations.SeVolume);
        
        /*bgmAudioSource = GetComponent<AudioSource>();
        if (bgmAudioSource == null)
            Debug.LogWarning("SoundManager: BGM AudioSource가 없습니다.");
        
        if (transform.childCount > 0)
            seAudioSource = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        else
            Debug.LogWarning("SoundManager: 자식 오브젝트 없음 (SE AudioSource 연결 실패)");*/
        
        
        
        /*bgmType = BGMType.Main;
        PlayBGM();*/

    }

    #region  각 씬에 맞는 BGM만 나오도록 설정

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 현재 씬의 이름에 따라 bgmType을 업데이트
        if (scene.name.Equals("MainMenu"))
        {
            bgmType = BGMType.Main;
        }

        else if (scene.name.Equals("Map"))
        {
            bgmType = BGMType.Game;
        }
        // 필요한 경우 다른 씬에 대한 분기도 추가
    
        // 씬에 맞게 BGM 재생
        PlayBGM();
    }
    #endregion
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& GameManager.Instance.gameState!=GameState.Gameplay)
        {
            PlaySE(SEType.Click);
        }
    }

    public void PlayBGM() // 타입 지정하지 않을 시 현재 타입으로 재생 오버로딩
    {
        PlayBGM(bgmType);
    }
    

    public void PlayBGM(BGMType type) // 타입 지정 재생 오버로딩
    {
        //Debug.Log(bgmOn.ToString());
        if (!bgmOn)
        {
            bgmAudioSource.Stop();
            subBgmAudioSource.Stop();
            return;
        }
        switch (type)
        {
            case BGMType.Main:
                bgmAudioSource.clip= bgmSounds[0];
                bgmAudioSource.loop = true;
                bgmAudioSource.Play();
                subBgmAudioSource.clip= bgmSounds[1];
                subBgmAudioSource.loop = true;
                subBgmAudioSource.Play();
                break;

            case BGMType.Game:
                bgmAudioSource.clip = bgmSounds[2];
                bgmAudioSource.loop = true;
                bgmAudioSource.Play();
                subBgmAudioSource.clip= bgmSounds[4];
                subBgmAudioSource.loop = true;
                subBgmAudioSource.Play();
                break;
            default:
                break;
            
        }
    }

    public void PlaySE(SEType type)
    {
        if (!seOn)
        {
            seAudioSource.Stop();
            return;
        }
        switch (type)
        {
            case SEType.Click:
                seAudioSource.clip= seSounds[0];
                seAudioSource.Play();
                break;
            case SEType.GameStart:
                seAudioSource.clip = seSounds[1];
                seAudioSource.Play();
                break;
            case SEType.GameEnd:
                seAudioSource.clip = seSounds[2];
                seAudioSource.Play();
                break;
            default:
                break;
            
        }
    }
    
    /*protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }*/
    
}
