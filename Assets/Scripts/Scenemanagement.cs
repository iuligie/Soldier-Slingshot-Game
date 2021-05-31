using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayServices;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public AudioClip UiBeepFx;
    public GameObject playButton;
    public GameObject signinButton;
    public GameObject signinMessage;
    public GameObject achievementButton;
    public GameObject leaderboardButton;
    public GameObject levelSelectionPanel;


    static bool sAutoAuthenticate = true;
    #region Authenticate
    // Use this for initialization
    void Start()
    {
        // if this is the first time we're running, bring up the sign in flow
        if (sAutoAuthenticate)
        {
            if (!GoogleServicesManager.Instance.Authenticated)
            {
                GoogleServicesManager.Instance.Authenticate();
                sAutoAuthenticate = false;
            }
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

            achievementButton.GetComponent<Button>().interactable = authenticated;
            leaderboardButton.GetComponent<Button>().interactable = authenticated;
        }
        else
        {
            msg.text = Strings.SignInBlurb;
            signin.interactable = !levelSelectionPanel.activeSelf;
            signinButton.GetComponentInChildren<Text>().text = Strings.SignIn;
        }

       
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

    #endregion

    /*

    void ShowLevelSelection()
    {
        levelSelectionPanel.SetActive(true);

        playButton.GetComponent<Button>().interactable = false;
        achievementButton.GetComponent<Button>().interactable = false;
        leaderboardButton.GetComponent<Button>().interactable = false;
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
      */

    public void ShowAchievements()
    {
        Beep();
        GooglePlayGamesServices.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        Beep();
        GooglePlayGamesServices.ShowLeaderboardsUI();
    }

    void Beep()
    {
        AudioSource.PlayClipAtPoint(UiBeepFx, Vector3.zero);
    }



    #region BasicButtons
    public void StartGame()
    {
        Beep();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuScene()
    {
        Beep();
        //SceneManager.LoadScene("MenuScene 1");
    }

    public void ExitGame()
    {
        Beep();
        Application.Quit();
    }
    #endregion
}
