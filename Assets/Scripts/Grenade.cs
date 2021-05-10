using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public GameObject GrenadeObj;

    public GameObject explosioneffect;

    bool hasExploded = false;

    public float delay = 3f;

    float countdown;

    public float radius = 5f;

    public float explosionforce = 50f;

    public int throwforce = 50;

	// Use this for initialization
	void Start () {
        countdown = delay;
        explosioneffect.SetActive(true);
        Debug.Log(countdown + " start");
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(countdown);
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Debug.Log("Going to explode");
            Explode();
            hasExploded = true;
            //explosioneffect.SetActive(false);
        }
	}

    void Explode()
    {
        Debug.Log("Is coming");
        Instantiate(explosioneffect, GrenadeObj.transform.position, transform.rotation);

        Debug.Log("BOOM");
        //explosioneffect.SetActive(false);
        //Debug.Log("BOOM");
        //
        Vector2 circlepos= new Vector2(GrenadeObj.transform.position.x, GrenadeObj.transform.position.y);
       

        Collider2D[] colliderToMove = Physics2D.OverlapCircleAll(circlepos, radius);

        foreach (Collider2D nearbyObj in colliderToMove)
        {
            Rigidbody2D rb = nearbyObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
                Vector2 direction = nearbyObj.GetComponent<Transform>().position - GrenadeObj.transform.position;
                rb.AddForce(direction * throwforce, ForceMode2D.Force);
            }

        }
        Debug.Log("Destroy Grenade");
        GrenadeObj.SetActive(false);
        //explosioneffect.
        //DestroyImmediate(GrenadeObj,true);
        //DestroyImmediate(explosioneffect,true);
        //if (GrenadeObj.)
        //{ }
    }
    
}
