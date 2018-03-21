using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{	
	[SerializeField] float walkMoveStopRadius = 0.2f;
	[SerializeField] float rangedAttackRadius = 5f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster camRaycaster;
    Vector3 currentDestination, clickPoint;

	float stopRadius;
    bool isInDirectMode = false;
        
    private void Start()
    {
        camRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
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
			clickPoint = camRaycaster.hit.point;
			switch (camRaycaster.currentLayerHit) {
			case Layer.Walkable:
				stopRadius = walkMoveStopRadius;
				currentDestination = ShortDestination(clickPoint, stopRadius);
				break;
			case Layer.Enemy:
				stopRadius = rangedAttackRadius;
				currentDestination = ShortDestination(clickPoint, stopRadius);
				break;
			default:
				print ("Layer action not defined");
				return;
			}
		}
		WalkToDestination();
	}

	private void WalkToDestination()
	{
		var playerToClickPoint = currentDestination - transform.position;
		if (playerToClickPoint.magnitude >= stopRadius) {
			thirdPersonCharacter.Move (currentDestination - transform.position, false, false);
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
			currentDestination = transform.position;
		}

		if (isInDirectMode) {
			ProcessDirectMovement();
		} else 
		{
			ProcressMouseMovement ();
		}
    }
	Vector3 ShortDestination(Vector3 destination, float shortening)
	{
		Vector3 reductionVector = (destination - transform.position).normalized * shortening;
		return destination - reductionVector;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawLine(transform.position, currentDestination);
		Gizmos.DrawSphere(currentDestination,0.1f);
		Gizmos.DrawSphere(clickPoint,0.15f);

	}
}

