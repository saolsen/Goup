using UnityEngine;
using System.Collections;
using IronScheme;

// Mostly optimizations and workarounds for things I can't figure out how to do in Ironscheme
public class SchemeHelpers {
	
	// Real nice for the repl.
	public static void CreateGoup (Vector3 position, Vector3 scale) {
		var mass = scale.magnitude * 2;
		var newGoup = PhotonNetwork.Instantiate("TestGoup", position, Quaternion.identity, 0);
		var pv = newGoup.GetComponent<PhotonView>();
		pv.RPC("Setup", PhotonTargets.AllBuffered, scale, mass);
	}

}
