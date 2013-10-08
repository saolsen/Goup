using UnityEngine;
using System.Collections;

public class SetName : MonoBehaviour {
	
	public TextMesh nametag;
	
	[RPC]
	void SetNameTag (string name) {
		nametag.text = name;	
	}
}
