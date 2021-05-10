using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanagement : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("SoldierShootScene1");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
