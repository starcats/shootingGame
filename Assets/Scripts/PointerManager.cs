using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour {

	PlayerController playerManager;
	// Use this for initialization
	void Start () {
		playerManager = GameObject.Find ("Player").GetComponent<PlayerController> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetMouseButton(0)) {
			this.gameObject.SetActive (true);
	
			var clickPosition = Input.mousePosition;
			var worldClickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
			worldClickPosition.z = 0f;

			transform.position = worldClickPosition;

		} else {
			//this.gameObject.SetActive (false);
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.gameObject.tag == "MoveButtonRight") {
			playerManager.MovePlayer ("Right");
		}
		if (col.gameObject.tag == "MoveButtonLeft") {
			playerManager.MovePlayer ("Left");
		}
		if (col.gameObject.tag == "MoveButtonUp") {
			playerManager.MovePlayer ("Up");
		}
		if (col.gameObject.tag == "MoveButtonDown") {
			playerManager.MovePlayer ("Down");
		}
	}
}
