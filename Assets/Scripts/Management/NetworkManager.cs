using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {
	
	bool Connected = false;
	string playerName = "";
	
	public GameObject playerControllerPrefab = null;
	
	bool serverOwner = false;
	
	void OnGUI () {
		if (!Connected) {
			GUILayout.Label("Name:");
			playerName = GUILayout.TextField(playerName);
			if (GUILayout.Button("PLAY")) {
				Connected = true;
				StartConnect ();
			}
		} else {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		}
	}
	
	void StartConnect () {
		PhotonNetwork.ConnectUsingSettings("alpha-0.9");
	}
	
	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom(null);
		serverOwner = true;
	}
	
	void OnJoinedRoom() {
		if (serverOwner) {
			this.GetComponent<WorldManager> ().SpawnWorld();
		}

		// Add player
		GameObject playerController = (GameObject)Instantiate(playerControllerPrefab, Vector3.up * 601, Quaternion.identity);
		GameObject playerModel = PhotonNetwork.Instantiate("PlayerModel", playerController.transform.position, Quaternion.identity, 0);

		// Connect model to controller so others can see position.
		SyncTransform sync = playerController.GetComponent<SyncTransform>();
		sync.child = playerModel.transform;

		// Set your name on the hat for other players to see.
		playerModel.GetComponent<PhotonView>().RPC("SetNameTag", PhotonTargets.AllBuffered, playerName);

		// Disable hat rendering locally.
		foreach (MeshRenderer r in playerModel.GetComponentsInChildren<MeshRenderer>()) {
			r.enabled = false;	
		}
	}
}