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
    // Start is called before the first frame update
    void Start()
    {
        //clearPanel = GameObject.Find("ClearPanel").gameObject;
        clearPanel.SetActive(false);
        //clearText = GameObject.Find("ClearText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("ClearPoint")) return;
        clearPanel.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, 3f).OnComplete(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
        Debug.Log(other.gameObject.tag);
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
    }
}
