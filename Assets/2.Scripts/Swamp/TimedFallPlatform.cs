using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedFallPlatform : MonoBehaviour
{
    [Header("설정")] 
    [SerializeField] private float fallDelay = 3f; 
    [SerializeField] private float respawnDelay = 5f;
    
    private bool isTriggered = false;
    private float stayTimer = 0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            stayTimer += Time.deltaTime;

            if (stayTimer >= fallDelay)
            {
                StartCoroutine(DisableAndRespawn());
                stayTimer = 0f;
                isTriggered = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = false;
            stayTimer = 0f;
        }
    }

    private IEnumerator DisableAndRespawn()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        gameObject.SetActive(true);
    }
}
