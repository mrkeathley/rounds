using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RoomMove : MonoBehaviour {

	public Vector2 cameraChangeMin;
	public Vector2 cameraChangeMax;
	public Vector3 playerChange;
	private CameraMovement cam;

	void Start() {
		cam = Camera.main.GetComponent<CameraMovement>();
	}

	void Update() {

	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			cam.minPosition += cameraChangeMin;
			cam.maxPosition += cameraChangeMax;
			other.transform.position += playerChange;
		}
	}

    
}
