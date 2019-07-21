using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	float speed = 0.2f;
	float sideSpeed = 0.5f;
	int sideMoveTime = 3;

	int hp = 5;

	Rigidbody2D myRigidbody;

	float shotDelay = 1.0f;
	float time;

	GameObject bullet;
	GameObject canvas;
	GameManager gameManager;

	public GameObject explosion;

	// Use this for initialization
	void Start () {

		transform.localPosition = new Vector2 (UnityEngine.Random.Range (300.0f, -300.0f),
												450);

		myRigidbody = GetComponent<Rigidbody2D> ();
		//myRigidbody.velocity = -transform.up * speed;

		bullet = Resources.Load<GameObject> ("Prefabs/EnemyBullet");

		canvas = GameObject.Find ("CanvasBack");

		StartCoroutine (Shot());
		//StartCoroutine (SideMove());

		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		// ゲームが終わったら全削除
		if (gameManager.isPlay == false) {
			Destroy (this.gameObject);
		}


		// コルーチン使わないバージョン
		/*
		time += Time.deltaTime;

		if (time > shotDelay) {
			for (int i = 0; i < 3; i++) {
				Attack();
			}
			time = 0;
		}
		*/

		if (transform.localPosition.y < -640) {
			Destroy (this.gameObject);
		}

	}

	IEnumerator Shot() {
		while (true) {
			for (int i = 0; i < 10; i++) {
				var z = 155 + (i * 5);
				Attack(z);
				//yield return new WaitForSeconds (0.03f);
			}
			yield return new WaitForSeconds (shotDelay);
		}
	}

	IEnumerator SideMove () {

		var directionX = 1;

		while (true) {
			var direction = new Vector2 (sideSpeed * directionX, -1 * speed);
			myRigidbody.velocity = direction;

			yield return new WaitForSeconds (sideMoveTime);
			directionX *= -1;
		}
	}

	void Attack (int directionZ) {
		var pf = Instantiate (bullet);
		pf.transform.SetParent (canvas.transform, false);
		pf.transform.position = this.gameObject.transform.position;
		pf.transform.rotation = Quaternion.Euler (0, 0, directionZ);
													//UnityEngine.Random.Range (140, 220));
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Enemy") {
			return;
		} else {

			hp --;
			if (hp <= 0) {
				Destroy (this.gameObject);
				var effect = Instantiate (explosion, transform.position, transform.rotation);
				Destroy (effect, 1.0f);
			}
		}

	}
}
