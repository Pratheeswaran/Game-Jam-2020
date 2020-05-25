using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLayer : MonoBehaviour
{
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
