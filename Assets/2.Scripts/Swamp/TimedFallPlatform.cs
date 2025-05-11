using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedFallPlatform : MonoBehaviour
{
    [Header("설정")] 
    [SerializeField] private float fallDelay = 3f; 
    
    private bool isTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(FallAfterDelay());
        }
    }

    private IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        gameObject.SetActive(false);
    }
}
