using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {

	float backGroundSpeed = -0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = transform.position + new Vector3 (0, backGroundSpeed, 0);

		if (transform.localPosition.y < -1300) {
			transform.localPosition = new Vector3 (0, 1300, 0);
		}
		
	}
}
