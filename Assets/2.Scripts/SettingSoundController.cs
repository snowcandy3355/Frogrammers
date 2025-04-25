using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSoundController : MonoBehaviour
{
    public Toggle bgmToggle;
    public Slider bgmSlider;
    public Toggle seToggle;
    public Slider seSlider;

    void Start()
    {
        // 초기값 설정
        bgmToggle.isOn = SoundManager.Instance.bgmOn;
        seToggle.isOn = SoundManager.Instance.seOn;
        bgmSlider.value = SoundManager.Instance.bgmAudioSource.volume;
        seSlider.value = SoundManager.Instance.seAudioSource.volume;

        // 이벤트 등록
        bgmToggle.onValueChanged.AddListener(ToggleBGM);
        seToggle.onValueChanged.AddListener(ToggleSE);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    public void ToggleBGM(bool isOn)
    {
        SoundManager.Instance.bgmOn = isOn;
        if (isOn)
            SoundManager.Instance.PlayBGM();
        else
            SoundManager.Instance.bgmAudioSource.Stop();
    }

    public void ToggleSE(bool isOn)
    {
        SoundManager.Instance.seOn = isOn;
    }

    public void SetBGMVolume(float value)
    {
        SoundManager.Instance.bgmAudioSource.volume = value;
    }

    public void SetSEVolume(float value)
    {
        SoundManager.Instance.seAudioSource.volume = value;
    }
}