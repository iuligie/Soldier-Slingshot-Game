using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlingshotSoldier;

public class EnemyManager : MonoBehaviour
{

    public int countDeadEnemies;
    public GameObject WinPanel;
    public int countBodyParts;
    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
        countBodyParts = this.gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (countBodyParts == countDeadEnemies)
        {
            TriggerWinPanel();
            GooglePlayGamesServices.UnlockAchievement(SlingshotSoldier.GPGSIds.achievement_full_body);
        }
    }

    void TriggerWinPanel()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
