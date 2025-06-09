using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Zmień na faktyczną nazwę sceny z grą
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial"); // Zmień na faktyczną nazwę sceny z tutorialem
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
