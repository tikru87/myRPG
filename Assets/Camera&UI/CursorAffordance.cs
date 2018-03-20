using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D targetCursor = null;
	[SerializeField] Texture2D unknownCursor = null;
	[SerializeField] Vector2 cursoHotspot = new Vector2(0,0);

	CameraRaycaster camRaycaster;

	void Start () {
	camRaycaster = GetComponentInChildren<CameraRaycaster>();

	}

	void LateUpdate () {
		switch (camRaycaster.currentLayerHit) {
			case Layer.Walkable:
				Cursor.SetCursor(walkCursor, cursoHotspot, CursorMode.Auto);
				break;
			case Layer.Enemy:
				Cursor.SetCursor(targetCursor, cursoHotspot, CursorMode.Auto);
				break;
			case Layer.RaycastEndStop:
				Cursor.SetCursor(unknownCursor, cursoHotspot, CursorMode.Auto);
				break;
			default:
				Debug.LogError("What cursor to use here?");
				return;
			}
	}
}
