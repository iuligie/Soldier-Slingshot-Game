using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;

public class Enemy : MonoBehaviour {

	public GameObject deathEffect;

	public static float health = 4f;

	public static int EnemiesAlive = 0;

	bool firstSpark = false;

	void Start ()
	{
		EnemiesAlive++;
	}

	void OnCollisionEnter2D (Collision2D colInfo)
	{
        if (colInfo.gameObject.CompareTag("Player"))
        {
		    if (colInfo.relativeVelocity.magnitude > health)
		    {
			    Die();
		    }
        }
    }

	void Die ()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Handheld.Vibrate();
		if (!firstSpark)
		{
			GooglePlayGamesServices.UnlockAchievement(SlingshotSoldier.GPGSIds.achievement_first_spark);
			firstSpark = true;
		}
		EnemiesAlive--;
		if (EnemiesAlive <= 0)
			Debug.Log("LEVEL WON!");

		Destroy(gameObject);
	}

}
