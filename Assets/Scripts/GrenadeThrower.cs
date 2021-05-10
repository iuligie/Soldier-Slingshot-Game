using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrenadeThrower : MonoBehaviour {

    public float throwforce = 40f;
    public GameObject GrenadePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Throwing Grenade");
            ThrowGrenade();
        }
        if (Input.GetMouseButton(1))
        {
            SceneManager.LoadScene("ExplodeScene");
        }
        //else { Debug.Log("No Click"); }
    }

    void ThrowGrenade()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject grenade = Instantiate(GrenadePrefab,mousepos,Quaternion.identity);
       // Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        //rb.AddForce(transform.forward * throwforce, ForceMode2D.Force);
    }
}
