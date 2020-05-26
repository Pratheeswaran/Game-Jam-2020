using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
	private GameController gameController;
	public AudioSource gameStartMusic;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        gameController = FindObjectOfType<GameController> ();
        gameStartMusic.Play();
	}
	void Update() {
		if (Input.GetButtonUp ("Jump") || Input.GetButtonUp ("Fire1")) {
			gameStartMusic.Stop();
			gameController.LoadLevel();
		}
	}
}
