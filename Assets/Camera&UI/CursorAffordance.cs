using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

	CameraRaycaster camRaycaster;

	void Start () {
	camRaycaster = GetComponentInChildren<CameraRaycaster>();

	}

	void Update () {
//		print(camRaycaster.layerHit);
	}
}
