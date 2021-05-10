using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

    public Transform target;
    public int speed;
    public float friction;
    public float lerpSpeed;
    float zDegrees;
    Quaternion fromRotation;
    Quaternion toRotation;
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.tag == "Cannon")
            {
                if (Input.GetMouseButton(0))
                {
                    zDegrees += Input.GetAxis("Mouse Y") * speed * friction;
                    fromRotation = target.transform.rotation;
                    toRotation = Quaternion.Euler(0,0, zDegrees);
                    target.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
                }
            }
        }

	}
}
