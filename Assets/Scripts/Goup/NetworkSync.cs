using UnityEngine;
using System.Collections;

public class NetworkSync : MonoBehaviour {
	
	public float lerpAmount = 0.15f;
	
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if(stream.isWriting) {
			// Sending Data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(rigidbody.velocity);
			stream.SendNext(rigidbody.angularVelocity);
		} else {
			// Receiving Data
			var pos = (Vector3)stream.ReceiveNext();
			var rot = (Quaternion)stream.ReceiveNext();
			var vel = (Vector3)stream.ReceiveNext();
			var av = (Vector3)stream.ReceiveNext();
			
			transform.position = Vector3.Lerp(transform.position, pos, lerpAmount);
			transform.rotation = rot;
			rigidbody.velocity = vel; //Vector3.Lerp(rigidbody.velocity, vel, lerpAmount);
			rigidbody.angularVelocity = av; //Vector3.Lerp(rigidbody.angularVelocity, av, lerpAmount);
		}
	}
}
