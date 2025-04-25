using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartGame()
    {
        SoundManager.Instance.PlaySE(SEType.Click);
        SceneManager.LoadScene("GameScene");
    }

    public void OnOptions()
    {
        Debug.Log("옵션 버튼 클릭됨 (옵션 메뉴는 추후 구현)");
    }

    public void OnQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();

        // 에디터에서 테스트용
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}