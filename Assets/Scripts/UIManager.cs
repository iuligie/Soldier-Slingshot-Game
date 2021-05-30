using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

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
    }

    public GameObject pausePanel;
 
    private bool paused;

    bool canPause;
    // Use this for initialization
    void Start()
    {

        paused = false;

        pausePanel.SetActive(false);



        canPause = true;
    }


    public void Pause()
    {

            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        
    }

    public void Resume()
    {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }


}
