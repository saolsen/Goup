using UnityEngine;
using System.Collections;

public class MoveAble : MonoBehaviour {
	
	public bool isMoving = false;
	public Vector3 towardsPosition;
	
	public float force = 10;
	
	public PhotonPlayer holder;
	
	bool forceToApply = false;
	Vector3 throwForce;
	
	[RPC]
	public void PickUp (PhotonMessageInfo info) {
		// Pick up the object.
		// Turn off physics, flag it as picked up, turn off network view.
		// Network view is turned off because we want the player holding the object to have full control over it.
		if (!isMoving) {
			holder = info.sender;
			rigidbody.freezeRotation = true;
			rigidbody.useGravity = false;
			isMoving = true;
			this.GetComponent<PhotonView>().synchronization = ViewSynchronization.Off;
		}
	}
	
	[RPC]
	public void PutDown () {
		// Put down. Turn back on physics and the network view.
		if (isMoving) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.freezeRotation = false;
			rigidbody.useGravity = true;
			isMoving = false;
			this.GetComponent<PhotonView>().synchronization = ViewSynchronization.ReliableDeltaCompressed;
		}
	}
	
	[RPC]
	public void ThrowDown (float playerStrength, Vector3 forward) {
		if (isMoving) {
			rigidbody.freezeRotation = false;
			rigidbody.useGravity = true;
			isMoving = false;
			this.GetComponent<PhotonView>().synchronization = ViewSynchronization.ReliableDeltaCompressed;
			throwForce = forward * playerStrength;
			forceToApply = true;
		}
	}
	
	// Called by the script holding the object.
	[RPC]
	public void SetFreezeRotation(bool freeze) {
		rigidbody.freezeRotation = freeze;	
	}
	
	[RPC]
	public void ApplyTorque(Vector3 force) {
		rigidbody.AddTorque(force);	
	}
	
	[RPC]
	public void SetMoveToPosition(Vector3 position, float playerStrength) {
		towardsPosition = position;
	}
	
	// Called from update.
	[RPC]
	public void UpdatePositionAndRotation (Vector3 position, Quaternion rotation) {
		transform.position = position;
		transform.rotation = rotation;
	}
	
	void FixedUpdate() {
		// Move towards the towardsPosition.
		if (isMoving) {
			var direction = towardsPosition - transform.position;
			rigidbody.velocity = direction * force;
		}

		if (forceToApply) {
			rigidbody.AddForce(throwForce * 1000);
			forceToApply = false;
		}
	}
	
	void Update() {
		// Since network view is no longer broadcasting position broadcast the position as seen by the person holding the object.
		if (isMoving && holder == PhotonNetwork.player) {
			this.GetComponent<PhotonView>().RPC("UpdatePositionAndRotation", PhotonTargets.Others, transform.position, transform.rotation);
		}
	}
}
