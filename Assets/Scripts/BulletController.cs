using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	float speed = 6.0f;

	// Use this for initialization
	void Start () {
		// transform.up = new Vector3 (0, 1, 0);
		GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 1, 0) * speed;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!GetComponent<Renderer> ().isVisible) {
			Destroy (this.gameObject);
		}
		
	}
}
