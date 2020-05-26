using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	public GameObject target;
	public float followAhead = 2.6f;
	public float smoothing = 5;
	public bool canMove;
	public bool canMoveBackward = false;

	private Transform leftEdge;
	private float cameraWidth;
	private Vector3 targetPosition;


	void Start () {
		Mario mario = FindObjectOfType<Mario> ();
		target = mario.gameObject;

		GameObject boundary = GameObject.Find ("Level Boundary");
		leftEdge = boundary.transform.Find ("Left Boundary").transform;
		float aspectRatio = 1.7778f;
		cameraWidth = Camera.main.orthographicSize * aspectRatio;

		// Initialize camera's position
		Vector3 spawnPosition = new Vector3(-5,1,0);
		targetPosition = new Vector3 (spawnPosition.x, transform.position.y, transform.position.z);

		bool passedLeftEdge = targetPosition.x < leftEdge.position.x + cameraWidth;


			transform.position = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
			canMove = true;
	}


	void Update () {
		if (canMove) {
			bool passedLeftEdge = transform.position.x < leftEdge.position.x + cameraWidth;


			targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

			// move target of camera ahead of Player, but do not let camera shoot pass
			// level boundaries
			if (target.transform.localScale.x > 0f  &&
			    targetPosition.x - leftEdge.position.x >= cameraWidth - followAhead) {
				if (canMoveBackward || target.transform.position.x + followAhead >= transform.position.x) {
					targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
					transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
				}

			} else if (target.transform.localScale.x < 0f && canMoveBackward && !passedLeftEdge 
				) {
				targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
				transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
			}
		}
			
//		void Update () { // can move camera both left and right
//			if (canMove) {
//				bool passedLeftEdge = transform.position.x < leftEdge.position.x + cameraWidth;
//				bool passedRightEdge = transform.position.x > rightEdge.position.x - cameraWidth;
//
//				targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
//
//				// move target of camera ahead of Player, but do not let camera shoot pass
//				// level boundaries
//				if (target.transform.localScale.x > 0f && !passedRightEdge && 
//					targetPosition.x - leftEdge.position.x >= cameraWidth - followAhead) {
//					targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
//					transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
//				} else if (target.transform.localScale.x < 0f && !passedLeftEdge && 
//					rightEdge.position.x - targetPosition.x >= cameraWidth - followAhead) {
//					targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
//					transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
//				}
//			}
//		}
	}
}