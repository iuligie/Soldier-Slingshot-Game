using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayServices;

public class Enemy : MonoBehaviour {

	public GameObject deathEffect;

	public static float health = 4f;

	public bool isAlive = true;

	bool firstSpark = false;

	void Start ()
	{
		//EnemiesAlive++;
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
		transform.parent.gameObject.GetComponent<EnemyManager>().countDeadEnemies++;
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Handheld.Vibrate();
		if (!firstSpark)
		{
			GooglePlayGamesServices.UnlockAchievement(SlingshotSoldier.GPGSIds.achievement_first_spark);
			firstSpark = true;
		}
		//EnemiesAlive--;
		if (!isAlive)
		{
			Debug.Log("Body Part Destroyed!");
		}
		Destroy(gameObject);
	}

}
