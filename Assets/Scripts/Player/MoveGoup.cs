using UnityEngine;
using System.Collections;

public class MoveGoup : MonoBehaviour {
	
	public float playerStrength = 5;
	public float maxGrabDistance = 10;
	public float minGrabDistance = 1;
	
	Transform playerCam;
	bool isHolding = false;
	float distanceToHolding;
	PhotonView holding;
	float holdingMass;
	
	void HandleThrowObject() {
		if (isHolding && Input.GetKeyDown(KeyCode.Mouse0)) {
			holding.RPC("ThrowDown", PhotonTargets.All, playerStrength, playerCam.forward);
			isHolding = false;
		}
	}
	
	void HandleGrabOrDropObject() {
		if (Input.GetKeyDown(KeyCode.E)) {
			if (!isHolding) {
				// See if anything is in front of cursor to pick up.
        	    RaycastHit hit;
            	if (Physics.Raycast(playerCam.position, playerCam.forward, out hit)) {
					distanceToHolding = (hit.transform.position - playerCam.position).magnitude;
            		var moveAble = hit.transform.gameObject.GetComponent<MoveAble>();
	                if (moveAble != null) {
						if (!moveAble.isMoving && distanceToHolding < maxGrabDistance) {
							holding = hit.transform.gameObject.GetComponent<PhotonView>();
							holdingMass = hit.rigidbody.mass;
							holding.RPC("PickUp", PhotonTargets.All);
							isHolding = true;
						}
					}
				}
			} else {
				holding.RPC("PutDown", PhotonTargets.All);
				isHolding = false;
			}
		}
	}
	
	void MoveHeldObject() {
		if (isHolding) {
			var change = Input.GetAxis("Mouse ScrollWheel");
			var newDist = distanceToHolding + change;
			
			if (newDist < minGrabDistance) {
				newDist = minGrabDistance;
			} else if (newDist > maxGrabDistance) {
				newDist = maxGrabDistance;
			}
			
			distanceToHolding = newDist;
			
			var centerScreen = playerCam.position + playerCam.forward * distanceToHolding;
			holding.RPC ("SetMoveToPosition", PhotonTargets.All, centerScreen, playerStrength);
		}
	}
	
	void RotateObject() {
		if (isHolding) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				var forward = new Vector3(playerCam.forward.x, 0, playerCam.forward.z);
				var sideways = new Vector3(playerCam.right.x, 0, playerCam.right.z);
				
				if (Input.GetKey(KeyCode.W)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, sideways * 3 * holdingMass);
				}
				else if (Input.GetKey(KeyCode.A)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, forward * 3 * holdingMass);
				}
				else if (Input.GetKey(KeyCode.S)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, sideways * -3 * holdingMass);
				}
				else if (Input.GetKey(KeyCode.D)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, forward * -3 * holdingMass);
				}
				else {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, true);
				}
			}
		}
	}
	
	void DisableControllerOnShift () {
		if (Input.GetKey(KeyCode.LeftShift)) {
			this.GetComponent<CharacterController>().enabled = false;
		} else {
			this.GetComponent<CharacterController>().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		HandleThrowObject();
		HandleGrabOrDropObject();
		DisableControllerOnShift();
	}
	
	void FixedUpdate () {
		MoveHeldObject();
		RotateObject();
	}
	
	void Start () {
		playerCam = this.transform.FindChild("Camera");	
	}
}