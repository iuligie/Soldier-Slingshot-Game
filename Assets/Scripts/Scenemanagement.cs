using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenemanagement : MonoBehaviour
{
    public AudioClip UiBeepFx;
    public GameObject playButton;
    public GameObject signinButton;
    public GameObject signinMessage;
    public GameObject achievementButton;
    public GameObject highScoreButton;
    public GameObject loadButton;
    public GameObject saveButton;
    public GameObject levelSelectionPanel;


    static bool sAutoAuthenticate = true;
    #region Authenticate
    // Use this for initialization
    void Start()
    {
        // if this is the first time we're running, bring up the sign in flow
        if (sAutoAuthenticate)
        {
            GoogleServicesManager.Instance.Authenticate();
            sAutoAuthenticate = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool standBy = GoogleServicesManager.Instance.Authenticating;
        bool authenticated = GoogleServicesManager.Instance.Authenticated;

        Text msg = signinMessage.GetComponent<Text>();
        Button signin = signinButton.GetComponent<Button>();

        if (standBy)
        {
            msg.text = Strings.SigningIn;
            signin.interactable = false;
        }
        else if (authenticated)
        {
            msg.text = Strings.SignedInBlurb;
            signin.interactable = !levelSelectionPanel.activeSelf;
            signinButton.GetComponentInChildren<Text>().text = Strings.SignOut;
        }
        else
        {
            msg.text = Strings.SignInBlurb;
            signin.interactable = !levelSelectionPanel.activeSelf;
            signinButton.GetComponentInChildren<Text>().text = Strings.SignIn;
        }

        //achievementButton.SetActive(authenticated);
        //highScoreButton.SetActive(authenticated);
        //loadButton.SetActive(authenticated);
        //saveButton.SetActive(authenticated);
    }

    public void OnSignIn()
    {
        if (GoogleServicesManager.Instance.Authenticating)
        {
            return;
        }

        Beep();

        if (GoogleServicesManager.Instance.Authenticated)
        {
            Beep();
            GoogleServicesManager.Instance.SignOut();
        }
        else
        {
            GoogleServicesManager.Instance.Authenticate();
        }
    }

    public void OnPlay()
    {
        Beep();

        if (GoogleServicesManager.Instance.Authenticated)
        {
            // If level 0 is the only possibility, don't bother to show the
            // level selection screen, just go straight into the level.
            //GameManager.Instance.GoToLevel(0);
            SceneManager.LoadScene("SoldierShootScene2");
        }
        else
        {
            // Show the level selection screen
            // gameObject.
            // this.enabled = false;
            //ShowLevelSelection();
        }
    }
    #endregion

    /*
    public void OnLoadProgress()
    {
        Beep();
        //GoogleServicesManager.Instance.LoadFromCloud();
    }

    public void OnSaveProgress()
    {
        Beep();
        //GoogleServicesManager.Instance.SaveProgress();
    }

    void ShowLevelSelection()
    {
        levelSelectionPanel.SetActive(true);

        playButton.GetComponent<Button>().interactable = false;
        achievementButton.GetComponent<Button>().interactable = false;
        highScoreButton.GetComponent<Button>().interactable = false;
        signinButton.GetComponent<Button>().interactable = false;
        loadButton.GetComponent<Button>().interactable = false;


        Button[] levels = levelSelectionPanel.GetComponentsInChildren<Button>();
        Text[] texts = levelSelectionPanel.GetComponentsInChildren<Text>();
        for (int i = 0; i < levels.Length; i++)
        {
            // create new local var for closure for click listener
            int level = i;
            texts[i].text = "Sector " + Convert.ToChar('A' + i) +
                            "\n" + GoogleServicesManager.Instance.Progress.GetLevelProgress(i).Score;
            levels[i].interactable =
                GoogleServicesManager.Instance.Progress.IsLevelUnlocked(i);
            levels[i].onClick.AddListener(() =>
            {
                Debug.Log("Playing level " + level);
                GoogleServicesManager.Instance.GoToLevel(level);
            });
        }
    }

    public void OnAchievements()
    {
        Beep();
        //GoogleServicesManager.Instance.ShowAchievementsUI();
    }

    public void OnHighScores()
    {
        Beep();
        //GoogleServicesManager.Instance.ShowLeaderboardUI();
    }
    */

    void Beep()
    {
        AudioSource.PlayClipAtPoint(UiBeepFx, Vector3.zero);
    }



    #region BasicButtons
    public void StartGame()
    {
        SceneManager.LoadScene("SoldierShootScene2");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
