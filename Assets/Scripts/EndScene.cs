using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScene : MonoBehaviour {
	public Text ScoreTextHUD;
	public Text CoinTextHUD;

	private GameController gameController;
	public AudioSource gameOverMusicSource;


	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
		gameController = FindObjectOfType<GameController> ();
		ScoreTextHUD.text =  gameController.score.ToString("D6");
		CoinTextHUD.text = "x" + gameController.coin.ToString("D2");
		
        gameOverMusicSource.Play();
	}
	void Update() {
		if (Input.GetButtonUp ("Jump")) {
			gameController.LoadLevel();
		}
	}
}
