using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    [SerializeField]private GameObject swtichHandler;
    // Start is called before the first frame update
    private Color offColor = Color.grey;//new Color(106f, 106f, 106f, 1f);
    private Color onColor = new Color(0f, 0f, 0f, 1f);
    public int index;
    public Slider volumeSlider;
    public TMP_Text volumeText;

    void Start()
    {
        SoundManager.Instance.bgmOn = Convert.ToBoolean(UserInformations.BgmState);
        SoundManager.Instance.seOn = Convert.ToBoolean(UserInformations.SeState);
        SoundManager.Instance.bgmAudioSource.volume = Convert.ToSingle(UserInformations.BgmVolume);
        SoundManager.Instance.seAudioSource.volume = Convert.ToSingle(UserInformations.SeVolume);
        /*Debug.Log("스위치-유저정보 bgm "+bgmOn);
        Debug.Log("스위치-유저정보 se " +seOn);
        Debug.Log("스위치-매니저 bgm "+SoundManager.Instance.bgmOn);
        Debug.Log("스위치-매니저 se "+SoundManager.Instance.seOn);*/
        index = this.gameObject.transform.parent.parent.GetSiblingIndex(); // 0 : BGM 스위치 , 1 : SE 스위치
        volumeSlider = gameObject.transform.parent.parent.GetComponentInChildren<Slider>();
        volumeText = gameObject.transform.parent.parent.GetComponentsInChildren<TMP_Text>()[1];
        switch (index)
        {
            case 0:
                if (SoundManager.Instance.bgmOn)
                {
                    var currentX = swtichHandler.GetComponent<RectTransform>().anchoredPosition.x;
                    swtichHandler.GetComponent<RectTransform>().anchoredPosition = new Vector2(-currentX, swtichHandler.GetComponent<RectTransform>().anchoredPosition.y);
                    this.GetComponent<Image>().color = onColor;
                    volumeSlider.gameObject.SetActive(true);
                }
                else if (!SoundManager.Instance.bgmOn)
                {
                    this.GetComponent<Image>().color = offColor;
                    volumeSlider.gameObject.SetActive(false);
                }
                volumeSlider.value = SoundManager.Instance.bgmAudioSource.volume;
                break;
            case 1:

                if (SoundManager.Instance.seOn)
                {
                    var currentX = swtichHandler.GetComponent<RectTransform>().anchoredPosition.x;
                    swtichHandler.GetComponent<RectTransform>().anchoredPosition = new Vector2(-currentX, swtichHandler.GetComponent<RectTransform>().anchoredPosition.y);
                    this.GetComponent<Image>().color = onColor;
                    volumeSlider.gameObject.SetActive(true);
                }
                else if (!SoundManager.Instance.seOn)
                {
                    this.GetComponent<Image>().color = offColor;
                    volumeSlider.gameObject.SetActive(false);

                }
                volumeSlider.value = SoundManager.Instance.seAudioSource.volume;
                break;
            default:
                Debug.Log("스위치"+SoundManager.Instance.seOn.ToString());

                break;
        }


    }


    // Update is called once per frame
    void Update()
    {
        switch (index)
        {
            case 0:
                SoundManager.Instance.bgmAudioSource.volume = volumeSlider.value;
                volumeText.text = (volumeSlider.value*100).ToString("0");
                break;
            case 1:
                SoundManager.Instance.seAudioSource.volume = volumeSlider.value;
                volumeText.text = (volumeSlider.value*100).ToString("0");
                break;
        }
    }

    public void OnClickSwitch()
    {
        this.gameObject.GetComponent<Button>().enabled = false;
        var currentX = swtichHandler.GetComponent<RectTransform>().anchoredPosition.x;
        swtichHandler.GetComponent<RectTransform>().DOAnchorPosX(-currentX, 0.2f);
        /*var currentColor = this.GetComponent<Image>().color;
        currentColor = new Color(0,0,0, 1f);*/
        switch (index)
        {
            case 0:
                Debug.Log("0스위치클릭"+SoundManager.Instance.seOn.ToString());
                if (SoundManager.Instance.bgmOn)
                {
                    SoundManager.Instance.bgmOn = false;
                    this.GetComponent<Image>().DOColor(offColor, 0.2f);
                    volumeSlider.GetComponent<CanvasGroup>().alpha = 1;
                    volumeSlider.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
                    {
                        volumeSlider.gameObject.SetActive(false);
                        this.gameObject.GetComponent<Button>().enabled = true;
                    });
                }
                else
                {
                    SoundManager.Instance.bgmOn = true;
                    this.GetComponent<Image>().DOColor(onColor, 0.2f);
                    volumeSlider.gameObject.SetActive(true);
                    volumeSlider.GetComponent<CanvasGroup>().alpha = 0;
                    volumeSlider.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(() =>
                    {
                        this.gameObject.GetComponent<Button>().enabled = true;
                    });

                }
                SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmType);
                break;
            case 1:
                Debug.Log("1스위치클릭"+SoundManager.Instance.seOn.ToString());

                if (SoundManager.Instance.seOn)
                {
                    SoundManager.Instance.seOn = false;
                    this.GetComponent<Image>().DOColor(offColor, 0.2f);
                    volumeSlider.GetComponent<CanvasGroup>().alpha = 1;
                    volumeSlider.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
                    {
                        volumeSlider.gameObject.SetActive(false);
                        this.gameObject.GetComponent<Button>().enabled = true;
                    });
                }
                else
                {
                    SoundManager.Instance.seOn = true;
                    this.GetComponent<Image>().DOColor(onColor, 0.2f);
                    volumeSlider.gameObject.SetActive(true);
                    volumeSlider.GetComponent<CanvasGroup>().alpha = 0;
                    volumeSlider.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(() =>
                    {
                        this.gameObject.GetComponent<Button>().enabled = true;
                    });
                }
                SoundManager.Instance.PlaySE(SoundManager.Instance.seType);
                break;
            default:
                break;
        }
        
    }
}
