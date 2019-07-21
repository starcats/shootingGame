using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float speed = 2f;
	float attackDelay = 0.15f;
	float attackDelayTime = 0f;
	float accelerator = 0.1f;

	float directionX = 0f;
	float directionY = 0f;

	Rigidbody2D myRigidbody;

	GameManager gameManager;

	GameObject bullet;

	GameObject canvas;

	public GameObject explosion;

	//public enum DIRECTION {RIGHT, LEFT, DOWN, UP};


	// Use this for initialization
	void Start () {

		myRigidbody = GetComponent<Rigidbody2D> ();

		bullet = Resources.Load<GameObject> ("Prefabs/PlayerBullet");
		canvas = GameObject.Find ("CanvasBack");

		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		//MovePlayer();

		Attack();

		attackDelayTime -= Time.deltaTime;

		if (Input.anyKey) {
			MovePlayer("");
		}

	}

	public void MovePlayer (string dir) {

		if (Input.GetKey(KeyCode.RightArrow) || dir == "Right") {
			directionX += accelerator;
			if (directionX > 2) {
				directionX = 2f;
			}
		} else if (Input.GetKey(KeyCode.LeftArrow) || dir == "Left") {
			directionX -= accelerator;
			if (directionX < -2) {
				directionX = -2f;
			}
		}

		if (Input.GetKey(KeyCode.UpArrow) || dir == "Up") {
			directionY += accelerator;
			if (directionY > 2) {
				directionY = 2f;
			}
		} else if (Input.GetKey(KeyCode.DownArrow) || dir == "Down") {
			directionY -= accelerator;
			if (directionY < -2) {
				directionY = -2f;
			}
		}


		// 壁判定
		MoveStop();

		var direction = new Vector2 (directionX, directionY);

		myRigidbody.velocity = direction * speed;

		
	}

	void Attack () {

		if (attackDelayTime <= 0 && gameManager.isPlay == true) {
			var z = 40;
			for (int i = 0; i < 3; i++) {
				var pf = Instantiate(bullet);
				pf.transform.SetParent (canvas.transform, false);
				pf.transform.localPosition = this.gameObject.transform.localPosition;
				pf.transform.rotation = Quaternion.Euler (0, 0, z + (i * -40) );
			}
			attackDelayTime = attackDelay;
		}
		
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Enemy") {
			gameManager.isPlayerAlive = false;

			Destroy (this.gameObject);
			GameObject effect = Instantiate (explosion, transform.position, transform.rotation);
			Destroy (effect, 1.0f);
		}
	}


	// 壁判定
	void MoveStop () {
		var x = transform.localPosition.x;
		var y = transform.localPosition.y;

		if (x < -350) {
			transform.localPosition = new Vector2(-350.0f, y);
			directionX = 0f;
		} else if (x > 350) {
			transform.localPosition = new Vector2(350.0f, y);
			directionX = 0f;
		}

		if (y < -620) {
			transform.localPosition = new Vector2(x, -620.0f);
			directionY = 0f;
		} else if (y > 620) {
			transform.localPosition = new Vector2(x, 620.0f);
			directionY = 0f;
		}
	}
}
