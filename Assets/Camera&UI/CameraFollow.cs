using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	GameObject player;
	float playerXPos, playerZPos;

	// Use this for initialization
	void Start () {
	player = GameObject.FindGameObjectWithTag("Player");
		
	}

	void LateUpdate () {

	playerXPos = player.transform.position.x;
	playerZPos= player.transform.position.z;
	Vector3 ArmPos = new Vector3(playerXPos, 0.0f, playerZPos);
	gameObject.transform.position = ArmPos;
		
	}
}
