using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public void changeScene()
    {
        //Use in Closing Slides to bring back Player to MainMenu
        SceneManager.LoadScene(0);
    }

    public void nextScene()
    {
        //Opens the next scene in the index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");    
    }
}
