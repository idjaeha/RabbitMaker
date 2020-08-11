using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [Header("오디오 등록?")]
    [SerializeField] Sound[] Audios; //소리모음

    [Header("오디오 플레이어")]
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update


    void Start()
    {
        audioSource.clip = Audios[0].clip;
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.S)))
        {
            audioSource.Play();
        }
        if ((Input.GetKeyDown(KeyCode.M)))
        {
            audioMute();
        }

    }

    //오디오 플레이
    public void audioPlay()
    {
        audioSource.Play();
    }


    //오디오 뮤트
    public void audioMute()
    {

        if (audioSource.mute == false)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }

    }

    //오디오 멈춤
    public void audioStop()
    {
        audioSource.Stop();
    }
}
