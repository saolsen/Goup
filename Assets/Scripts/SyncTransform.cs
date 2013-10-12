using UnityEngine;
using System.Collections;

public class SyncTransform : MonoBehaviour {

	public Transform child = null;

	// Update is called once per frame
	void Update () {
		if (child != null) {
			child.transform.position = transform.position;
			child.transform.rotation = transform.rotation;
		}
	}
}