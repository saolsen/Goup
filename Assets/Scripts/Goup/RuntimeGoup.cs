using UnityEngine;
using System.Collections;

public class RuntimeGoup : MonoBehaviour {

	void SetScale (Vector3 scale) {
		transform.localScale = 	scale;
	}
	
	void SetMass (float mass) {
		rigidbody.mass = mass;	
	}
	
	[RPC]
	void Setup (Vector3 scale, float mass) {
		SetMass(mass);
		SetScale(scale);
	}
	
	void Start () {
		this.transform.parent = GameObject.Find("DynamicObjects").transform;
	}
}