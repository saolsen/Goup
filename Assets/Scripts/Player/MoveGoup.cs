using UnityEngine;
using System.Collections;

public class MoveGoup : MonoBehaviour {
	
	public float rotationSpeed = 3;
	
	Transform playerCam;
	bool isHolding = false;
	float distanceToHolding;
	PhotonView holding;
	
	void HandleGrabOrDropObject() {
		if (Input.GetKeyDown(KeyCode.E)) {
			if (!isHolding) {
				// See if anything is in front of cursor to pick up.
        	    RaycastHit hit;
            	if (Physics.Raycast(playerCam.position, playerCam.forward, out hit)) {
					distanceToHolding = (hit.transform.position - playerCam.position).magnitude;
            		var moveAble = hit.transform.gameObject.GetComponent<MoveAble>();
	                if (moveAble != null) {
						if (!moveAble.isMoving) {
							holding = hit.transform.gameObject.GetComponent<PhotonView>();
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
			distanceToHolding += change;
			
			var centerScreen = playerCam.position + playerCam.forward * distanceToHolding;
			holding.RPC ("SetMoveToPosition", PhotonTargets.All, centerScreen);
		}
	}
	
	void RotateObject() {
		if (isHolding) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				var forward = new Vector3(playerCam.forward.x, 0, playerCam.forward.z);
				var sideways = new Vector3(playerCam.right.x, 0, playerCam.right.z);
				
				if (Input.GetKey(KeyCode.W)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, sideways * rotationSpeed);
				}
				else if (Input.GetKey(KeyCode.A)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, forward * rotationSpeed);
				}
				else if (Input.GetKey(KeyCode.S)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, sideways * -rotationSpeed);
				}
				else if (Input.GetKey(KeyCode.D)) {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, false);
					holding.RPC("ApplyTorque", PhotonTargets.All, forward * -rotationSpeed);
				}
				else {
					holding.RPC("SetFreezeRotation", PhotonTargets.All, true);
					holding.RPC("ApplyTorque", PhotonTargets.All, sideways * rotationSpeed);
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