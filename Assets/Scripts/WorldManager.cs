using UnityEngine;
using System.Collections;
using System.Linq;

public class WorldManager : MonoBehaviour {
	
	// Would handle saving and loading a game or whatever. For now just spawns some stuff into the world.
	// This stuff is only called by the host of the game so when they disconnect it all goes away. Uh oh!
	public void SpawnWorld () {
		// 1 test goup
		
		var upten = new Vector3(0, 10, 0);
		
		foreach (int i in Enumerable.Range(0, 15)) {
			var scale = Random.onUnitSphere * Random.Range(2, 10);
			var mass = scale.magnitude * 2;
			var newGoup = PhotonNetwork.Instantiate("TestGoup", (Random.onUnitSphere * 9) + upten, Quaternion.identity, 0);
			var pv = newGoup.GetComponent<PhotonView>();
			pv.RPC("Setup", PhotonTargets.AllBuffered, scale, mass);
		}
		
		PhotonNetwork.Instantiate("Antisun", new Vector3(4, 200, 4), Quaternion.identity, 0);
	}
}