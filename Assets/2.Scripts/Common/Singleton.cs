using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    
    // Inspector에서 설정할 수 있도록 persistent 플래그 추가 (기본값 true)
    // 해당 싱글톤을 상속받는 스크립트의 true, flase를 inspector 창에서 조절가능
    // true = DontDestroyOnLoad(씬이 변경되어도 남아있음), false = 씬이 변경되면 파괴
    [SerializeField]
    private bool persistent = true;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        //Debug.Log("타입:"+this.GetType());

        if (_instance == null)
        {
            _instance = this as T;
            if (persistent)
            {
                DontDestroyOnLoad(gameObject);
            }
            
            // 씬 전환 시 호출되는 액션 메서드 할당
            /*SceneManager.sceneLoaded += OnSceneLoaded;*/
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    /*protected abstract void OnSceneLoaded(Scene scene, LoadSceneMode mode);*/
}