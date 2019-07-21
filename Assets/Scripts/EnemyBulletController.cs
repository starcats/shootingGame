using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour {

	float speed = 1.5f;

	public GameObject explosion;
	static GameManager gameManager;
	static AudioManager audioManager;
	static GameObject backGround;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = transform.up * speed;
		GetManager();

	}
	
	// Update is called once per frame
	void Update () {

		if (!GetComponent<Renderer> ().isVisible) {
			Destroy (this.gameObject);
		}

		if (gameManager.isPlay == false) {
			Destroy (this.gameObject);
		}
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerBullet") {
			GetManager();

			// player以外との衝突でscoreが上がる仕様なのでバグ起こりそう
			//  ⇨prefabからplayerBullet引っ張ってくればいいのか？
			//   ⇨ダメだった。cloneと別のものとして扱われる
			// clone全てforeachで取得してそれが球ならという風にすればバグは起こらなさそう
			//  ⇨負担がすごそう
			// tagで管理できそう
			if (col.gameObject.tag == "PlayerBullet") {
				gameManager.score ++;
				// 違うスクリプトから音を鳴らす。
				audioManager.BulletExplosionSE();
			} 

			var effect = Instantiate (explosion, this.gameObject.transform.localPosition, this.gameObject.transform.rotation);
			effect.transform.SetParent (backGround.transform, false);
			Destroy (effect, 0.1f);
			gameManager.scoreText.text = "score : " + gameManager.score;

			Destroy (this.gameObject);

		}		
	}

	void GetManager () {
		// instant毎にGameObject.Findさせると絶対重くなる。
		if (gameManager == null || backGround == null) {
			backGround = GameObject.Find ("BackGround");
			GameObject manager = GameObject.Find ("GameManager");
			gameManager = manager.GetComponent<GameManager> ();
			audioManager = manager.GetComponent<AudioManager> ();
		}
	}

}
