using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster camRaycaster;
    Vector3 currentClickTarget;
        
    private void Start()
    {
        camRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate ()
	{
		if (Input.GetMouseButton (0)) {
			print ("Cursor raycast hit" + camRaycaster.layerHit);
			switch (camRaycaster.layerHit) {
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
			m_Character.Move (currentClickTarget - transform.position, false, false);
		} else 
		{
			m_Character.Move (Vector3.zero,false,false);
		}

    }
}
