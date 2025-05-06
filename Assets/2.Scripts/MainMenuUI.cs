using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    
    [SerializeField] private GameObject SettingPanel;

    public void Start()
    {
        Instantiate(SettingPanel, SettingPanel.transform.parent);
        SettingPanel.SetActive(false);
    }
    public void OnStartGame()
    {
        SoundManager.Instance.PlaySE(SEType.Click);
        SceneManager.LoadScene("RopeTestScene");
        Cursor.lockState = CursorLockMode.Locked;
        //SceneManager.LoadScene("GameScene");
    }

    public void OnOptions()
    {
        Debug.Log("세팅 버튼 클릭됨");
        
        //Setting Panel 추가
        SettingPanel.SetActive(true);
    }

    public void OnQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();

        // 에디터에서 테스트용
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else // 빌드파일 종료 추가
        Application.Quit();
#endif
    }
}