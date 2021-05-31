namespace GooglePlayServices
{
    using UnityEngine;
    using System.Collections.Generic;
    using GooglePlayGames;
    using GooglePlayGames.BasicApi.SavedGame;
    using System;
    using GooglePlayGames.BasicApi;
    using UnityEngine.SceneManagement;
    using SlingshotSoldier;

    public class GoogleServicesManager
    {
        private static GoogleServicesManager sInstance = new GoogleServicesManager();

        private bool mAuthenticating = false;
        private string mAuthProgressMessage = Strings.SigningIn;

        // achievement increments we are accumulating locally, waiting to send to the games API
        private Dictionary<string, int> mPendingIncrements = new Dictionary<string, int>();

        // list of achievements we know we have unlocked (to avoid making repeated calls to the API)
        private Dictionary<string, bool> mUnlockedAchievements = new Dictionary<string, bool>();

        // what is the highest score we have posted to the leaderboard?
        private int mHighestPostedScore = 0;

        public int score = 0;

        public static GoogleServicesManager Instance
        {
            get { return sInstance; }
        }

        void ReportAllProgress()
        {
            FlushAchievements();
            PostToLeaderboard();
        }

        public void FlushAchievements()
        {
            if (Authenticated)
            {
                foreach (string ach in mPendingIncrements.Keys)
                {
                    // incrementing achievements by a delta is a feature
                    // that's specific to the Play Games API and not part of the
                    // ISocialPlatform spec, so we have to break the abstraction and
                    // use the PlayGamesPlatform rather than ISocialPlatform
                    PlayGamesPlatform p = (PlayGamesPlatform)Social.Active;
                    p.IncrementAchievement(ach, mPendingIncrements[ach], (bool success) => { });
                }

                mPendingIncrements.Clear();
            }
        }

        internal void GoToScene(string scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }

        public void RestartLevel()
        {
            
            ReportAllProgress();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void FinishLevelAndGoToNext(int score, int stars)
        {
            //mProgress.SetLevelProgress(mLevel, score, stars);
            //AutoSave();
            ReportAllProgress();
            //TO DO - work on this
        }

        public void QuitGame()
        {
            ReportAllProgress();
            Application.Quit();
        }

        public void QuitToMenu()
        {
            //AutoSave();
            ReportAllProgress();
            SceneManager.LoadScene("MenuScene");
        }

        public void UnlockAchievement(string achId)
        {
            if (Authenticated && !mUnlockedAchievements.ContainsKey(achId))
            {
                Social.ReportProgress(achId, 100.0f, (bool success) => { });
                mUnlockedAchievements[achId] = true;
            }
        }


        public bool Authenticating
        {
            get { return mAuthenticating; }
        }

        public bool Authenticated
        {
            get { return Social.Active.localUser.authenticated; }
        }

        public void Authenticate()
        {
            if (Authenticated || mAuthenticating)
            {
                Debug.LogWarning("Ignoring repeated call to Authenticate().");
                return;
            }

            // Enable/disable logs on the PlayGamesPlatform
            PlayGamesPlatform.DebugLogEnabled = GameConsts.PlayGamesDebugLogsEnabled;

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .EnableSavedGames()
                .Build();
            PlayGamesPlatform.InitializeInstance(config);

            // Activate the Play Games platform. This will make it the default
            // implementation of Social.Active
            PlayGamesPlatform.Activate();

            // Set the default leaderboard for the leaderboards UI
            ((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(GameIds.LeaderboardId);

            // Sign in to Google Play Games
            mAuthenticating = true;
            Social.localUser.Authenticate((bool success) =>
            {
                mAuthenticating = false;
                if (success)
                {
                // if we signed in successfully, load data from cloud
                Debug.Log("Login successful!");
                }
                else
                {
                // no need to show error message (error messages are shown automatically
                // by plugin)
                Debug.LogWarning("Failed to sign in with Google Play Games.");
                }
            });
        }

        public void SignOut()
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
        }

        public string AuthProgressMessage
        {
            get { return mAuthProgressMessage; }
        }

        public void ShowLeaderboardUI()
        {
            if (Authenticated)
            {
                Social.ShowLeaderboardUI();
            }
        }

        public void ShowAchievementsUI()
        {
            if (Authenticated)
            {
                Social.ShowAchievementsUI();
            }
        }

        public void PostToLeaderboard()
        {
            
            if (Authenticated && score > mHighestPostedScore)
            {
                // post score to the leaderboard
                Social.ReportScore(score, GameIds.LeaderboardId, (bool success) => { });
                GooglePlayGamesServices.IncrementAchievement(SlingshotSoldier.GPGSIds.achievement_high_score, 1);
                mHighestPostedScore = score;
            }
            else
            {
                Debug.LogWarning("Not reporting score, auth = " + Authenticated + " " +
                                 score + " <= " + mHighestPostedScore);
            }
        }
    }
}