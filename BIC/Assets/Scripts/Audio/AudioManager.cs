using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    //VFX
    [SerializeField] private EventReference mainMusicLoop;
    [SerializeField] private GameObject AudioManagerObject;

    private void Start()
    {
        PlayMusic();
    }
    public void PlayMusic()
    {
        //SFX
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MUSIC/Main_Music_Loop", AudioManagerObject);
    }
     

}
