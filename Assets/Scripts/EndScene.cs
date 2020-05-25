using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScene : MonoBehaviour {
	public Text ScoreTextHUD;
	public Text CoinTextHUD;
	public Text MeterTextHUD;

	private GameController gameController;
	public AudioSource gameOverMusicSource;

    private void Awake()
    {
		Time.timeScale = 1;
		gameController = FindObjectOfType<GameController> ();
	}
	void Start () {
		ScoreTextHUD.text =  gameController.score.ToString("D6");
		CoinTextHUD.text = "x" + gameController.coin.ToString("D2");
		MeterTextHUD.text = gameController.meter.ToString("D3");
		gameOverMusicSource.Play();
	}
	void Update() {
		if (Input.GetButtonUp ("Jump") || Input.GetButtonUp ("Fire1")) {
			gameController.LoadLevel();
		}
	}
}
