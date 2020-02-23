using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Camera cam;
		if (GameManager.instance.isOneTurn == true) {
			cam = GameObject.Find ("1 Camera").GetComponent<Camera> ();
		} else {
			cam = GameObject.Find ("2 Camera").GetComponent<Camera> ();;
		}


		Vector3 p = cam.transform.position;
		p.z = transform.position.z;
		p.x = transform.position.x;
		transform.LookAt (p);
	}
}
