using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	float createEnemyTime = 3f;
	float createEnemyDelay = 0f;
	int enemyCount = 15;
	public int score = 0;
	public Text scoreText;
	Text timeText;
	GameObject resultScoreText;

	float gameTime = 60.0f;

	GameObject enemy;
	GameObject canvas;

	GameObject gameOverImage;
	GameObject gameClearImage;
	public bool isPlay;
	public bool isPlayerAlive;

	GameObject startButton;

	// Use this for initialization
	void Start () {

		enemy = Resources.Load<GameObject> ("Prefabs/Enemy");

		canvas = GameObject.Find ("CanvasBack");
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		timeText = GameObject.Find ("TimeText").GetComponent<Text> ();
		resultScoreText = GameObject.Find ("ResultScoreText");
		resultScoreText.SetActive (false);


		gameOverImage = GameObject.Find ("GameOverImage");
		gameOverImage.SetActive (false);

		gameClearImage = GameObject.Find ("GameClearImage");
		gameClearImage.SetActive (false);

		startButton = GameObject.Find ("StartButton");
		Button btn = startButton.GetComponent<Button> ();
		btn.onClick.AddListener (GameStart);

		isPlayerAlive = true;
	}

	
	// Update is called once per frame
	void Update () {
		var t = (int)gameTime;
		timeText.text = "残り時間 : " + t;

		// クリア判定、制限時間
		if (gameTime < 0f) {
			gameClearImage.SetActive (true);
			isPlay = false;
			resultScoreText.SetActive (true);
			var text = resultScoreText.GetComponent<Text> ();
			text.text = "Score : " + score;
		} else if (isPlay == true) {
			gameTime -= Time.deltaTime;
		}

		// player死亡判定
		if (isPlayerAlive == false) {
			gameOverImage.SetActive (true);
			isPlay = false;
			resultScoreText.SetActive (true);
			var text = resultScoreText.GetComponent<Text> ();
			text.text = "Score : " + score;
		}

		createEnemyDelay += Time.deltaTime;

		if (createEnemyDelay > createEnemyTime && enemyCount > 0 && isPlay == true) {
			CreateEnemy();
			createEnemyDelay = 0f;
			enemyCount--;
		}
		
	}

	void CreateEnemy () {
		GameObject pf = Instantiate<GameObject> (enemy);
		pf.transform.SetParent (canvas.transform, false);
	}

	void GameStart () {
		isPlay = true;
		startButton.SetActive (false);
	}

}
