using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public EventReference mainThemeReference;
    public GameObject mainThemeObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayGame()
    {
        //Opens the first scene of the game
        SceneManager.LoadScene("AudioTest");

        //FMOD - Play one-shot
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MUSIC/MUS_Farm_Test", mainThemeObject);
    }

    public void QuitGame()
    {
        //Exits App when game is built. Doesn´t work in Unity editor
        Application.Quit();
    }

}

