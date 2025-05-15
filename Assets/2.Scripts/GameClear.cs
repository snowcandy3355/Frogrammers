using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private GameObject clearPanel;
    [SerializeField]
    private GameObject clearText;
    
    private bool canClear = true;
    void Start()
    {
        //clearPanel = GameObject.Find("ClearPanel").gameObject;
        clearPanel.SetActive(false);
        canClear = true;
        //clearText = GameObject.Find("ClearText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("ClearPoint")||!canClear) return;
        canClear = false;
        clearPanel.SetActive(true);
        canvasGroup.alpha = 0;
        SoundManager.Instance.PlaySE(SEType.GameEnd);
        canvasGroup.DOFade(1f, 3f).OnComplete(() =>
        {
            canClear = true;
            GameManager.Instance.gameState = GameState.None;
            SceneManager.LoadScene("MainMenu");
        });
        Debug.Log(other.gameObject.tag);
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
    }
}
