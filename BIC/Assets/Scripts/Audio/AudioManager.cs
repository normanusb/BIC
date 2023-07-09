using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    //Reference to Audio Manager Object
    [SerializeField] private GameObject AudioManagerObject;

    //Main Music
    [SerializeField] private EventReference mainMusicLoop;
    
    //Main Menu Music
    [SerializeField] private EventReference mainMenuMusicLoop;

    //Ambience SFX
    [SerializeField] private EventReference ambienceSFX;

    private void Start()
    {
        PlayMusic();
    }
    public void PlayMusic()
    {
        //Music
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MUSIC/Main_Music_Loop", AudioManagerObject);
    }

    public void PlayAmbienceSFX()
    {
        //SFX
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/AMBIENCE/AMB_Farm_Forest", AudioManagerObject);
    }


}
