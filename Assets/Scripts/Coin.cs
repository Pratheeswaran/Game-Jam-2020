using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	private GameController gameController;
	public AudioSource coinHit;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			coinHit.Play();
			gameObject.GetComponent<Renderer>().enabled = false;
			gameController.AddCoin(1,gameObject.transform.position);
			Destroy(gameObject, 1f);
		}
	}
}
