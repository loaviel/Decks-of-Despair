using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Play()
    {
        // Loads the tutorial level scene

        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        // Quits the game

        Application.Quit();
    }

    public void Options()
    {
        // Add Options Menu Screen Code Here.
    }

    public void Menu()
    {
        // Loads the menu scene.
        SceneManager.LoadScene("MainMenu");
    }
}
