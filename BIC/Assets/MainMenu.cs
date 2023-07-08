using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayGame()
    {
        //Opens the first scene of the game
        SceneManager.LoadScene("Audio");
    }

    public void QuitGame()
    {
        //Exits App when game is built. Doesn´t work in Unity editor
        Application.Quit();
    }

}

