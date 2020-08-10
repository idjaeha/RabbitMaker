using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string soundName;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [Header("오디오 등록?")]
    [SerializeField] Sound[] Audios; //소리모음

    [Header("오디오 플레이어")]
    [SerializeField] AudioSource audioSorece;
    // Start is called before the first frame update


    void Start()
    {
        audioSorece.clip = Audios[0].clip;
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.S))){
            audioSorece.Play();
        }
        if ((Input.GetKeyDown(KeyCode.M)))
        {
            if (audioSorece.mute == false) {
                audioSorece.mute = true;
            }
            else
            {
                audioSorece.mute = false;
            }

            }

    }

  
    public void audioMute()
    {
        //음소거
        if (audioSorece.mute == false) { 
            audioSorece.mute = true;
        } else
        {
            audioSorece.mute = false;
        }

    }
}
