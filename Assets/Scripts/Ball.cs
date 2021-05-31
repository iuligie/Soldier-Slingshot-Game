using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayServices;

public class Ball : MonoBehaviour {

	public Rigidbody2D rb;
	public Rigidbody2D hook;

	public float releaseTime = .15f;
	public float maxDragDistance = 2f;

	public GameObject nextBall;

	private bool isPressed = false;

	public GameObject enemyGO;

	private EnemyManager enemy;

	void Start()
	{
		enemy = enemyGO.GetComponent<EnemyManager>();
	}

	void Update ()
	{
		if (isPressed)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
				rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
			else
				rb.position = mousePos;
		}
	}

	void OnMouseDown ()
	{
		isPressed = true;
		rb.isKinematic = true;
	}

	void OnMouseUp ()
	{
		isPressed = false;
		rb.isKinematic = false;

		StartCoroutine(Release());
	}

	IEnumerator Release ()
	{
		yield return new WaitForSeconds(releaseTime);

		GetComponent<SpringJoint2D>().enabled = false;
		this.enabled = false;

		yield return new WaitForSeconds(2f);

		if (nextBall != null)
		{
			nextBall.SetActive(true);
		} else
		{
			//Enemy.EnemiesAlive = 0;
            //if (GetComponent<Rigidbody2D>().velocity == new Vector2(2,2))
            //{
			if(enemy.countDeadEnemies != enemy.countBodyParts)
              Debug.Log("Enter last if before reset " + GetComponent<Rigidbody2D>().velocity);
                yield return new WaitForSeconds(3f);
			GoogleServicesManager.Instance.score = enemy.countDeadEnemies;
			GoogleServicesManager.Instance.RestartLevel();
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //}
        }
	
	}

}
