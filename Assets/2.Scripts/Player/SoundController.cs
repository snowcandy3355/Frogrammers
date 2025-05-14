using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundController : MonoBehaviour
{
    [SerializeField]private PlayerMove _playerMove;
    [SerializeField]private AudioSource walkSound;
    [SerializeField]private AudioSource shootRopeSound;
    
    private float stepDelay = 0.5f;
    private float stepTimer = 0f;
    private GroundType _groundType;
    public AudioClip sandFootStep1;
    public AudioClip sandFootStep2;
    public AudioClip iceFootStep1;
    public AudioClip iceFootStep2;
    public AudioClip mudFootStep1;
    public AudioClip mudFootStep2;
    private void Update()
    {
        _groundType = _playerMove.groundType;
        SoundContoller();
    }

    private void SoundContoller()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shootRopeSound.Play();
        }

        if (_playerMove.IsWalking && _playerMove.IsGround)
        {
            AudioClip clip = null;
            stepTimer -= Time.deltaTime;
            switch (_groundType)
            {
                case GroundType.Ground:
                    clip = Random.Range(0,2) == 1 ? sandFootStep1 : sandFootStep2;
                    break;
                case GroundType.IceGround:
                    clip = Random.Range(0,2) == 1 ? iceFootStep1 : iceFootStep2;
                    break;
                case GroundType.Mud:
                    clip = Random.Range(0,2) == 1 ? mudFootStep1 : mudFootStep2;
                    break;
                    
            }
            if (stepTimer <= 0f)
            {
                walkSound.PlayOneShot(clip);
                stepTimer = stepDelay;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
