using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextEffect : MonoBehaviour {

	void Start () {
		GetComponent<MeshRenderer> ().sortingLayerName = "Player";
	}
}
