using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayServices;

public class UIManager : MonoBehaviour
{

    /*public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    public GameObject pausePanel;
 
    private bool paused;

    public GameObject txtScore;

    public GameObject enemyManager;
   
    // Use this for initialization
    void Start()
    {

        paused = false;

        pausePanel.SetActive(false);

        if (txtScore != null)
            txtScore.GetComponent<Text>().text+= "0";

    }
    void Update()
    {
        txtScore.GetComponent<Text>().text = "Score: " + enemyManager.GetComponent<EnemyManager>().countDeadEnemies.ToString();
    }

    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Game is Paused");
        }
    }

    public void Resume()
    {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        GoogleServicesManager.Instance.QuitToMenu();
    }

    public void QuitGame()
    {
        GoogleServicesManager.Instance.QuitGame();
    }


}
