using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster camRaycaster;
    Vector3 currentClickTarget;

    bool isInDirectMode = false;
        
    private void Start()
    {
        camRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }
	private void ProcessDirectMovement ()
	{
			float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
			Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = v*camForward + h*Camera.main.transform.right;

			thirdPersonCharacter.Move (movement, false, false);
	}
	private void ProcressMouseMovement () // is called in FixedUpdate
	{
		if (Input.GetMouseButton (0)) {
			print ("Cursor raycast hit" + camRaycaster.currentLayerHit);
			switch (camRaycaster.currentLayerHit) {
			case Layer.Walkable:
				currentClickTarget = camRaycaster.hit.point;
				break;
			case Layer.Enemy:
				print ("Not moving to enemy");
				break;
			default:
				print ("Layer action not defined");
				return;
			}
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
			thirdPersonCharacter.Move (currentClickTarget - transform.position, false, false);
		}
		else {
			thirdPersonCharacter.Move (Vector3.zero, false, false);
		}
	}

    // Fixed update is called in sync with physics
    private void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.G)) {
			isInDirectMode = !isInDirectMode;
			currentClickTarget = transform.position;
		}

		if (isInDirectMode) {
			ProcessDirectMovement();
		} else 
		{
			ProcressMouseMovement ();
		}
    }
}

