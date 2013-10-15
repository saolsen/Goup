using UnityEngine;
using System.Collections;

public class EditableTerrain : MonoBehaviour {
	[RPC]
	void ModifyTerrain() {
		Debug.Log("terrain getting edited");
	}
}
