using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLayer : MonoBehaviour
{
    // Start is called before the first frame update
      	private GameController gameController;
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log (other.gameObject.tag + " Kill layer");
        if(other.gameObject.tag != "Player")
		 {   Destroy (other.gameObject); }
        else
        {
            Mario mario = other.gameObject.GetComponent<Mario> ();
            mario.FreezeAndDie();
        }

	}
}
