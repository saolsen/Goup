using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
	
	// Would handle saving and loading a game or whatever. For now just spawns some stuff into the world.
	// This stuff is only called by the host of the game so when they disconnect it all goes away. Uh oh!
	public void SpawnWorld () {
		// 1 test goup
		PhotonNetwork.Instantiate("TestGoup", new Vector3(3, 3, 3), Quaternion.identity, 0);
		PhotonNetwork.Instantiate("TestGoup1", new Vector3(-3, 3, 3), Quaternion.identity, 0);
		PhotonNetwork.Instantiate("TestGoup2", new Vector3(3, 3, -3), Quaternion.identity, 0);
		PhotonNetwork.Instantiate("TestGoup3", new Vector3(-3, 3, -3), Quaternion.identity, 0);
	}
}