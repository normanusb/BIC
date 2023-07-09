using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManageer : MonoBehaviour
{
    public EventReference mainMenuMusicReference;
    public GameObject AudioManagerObject;

    public void PlayMainMenuMusic()
    {
        //Music
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MUSIC/MUS_Menu_Loop", AudioManagerObject);
    }
}
