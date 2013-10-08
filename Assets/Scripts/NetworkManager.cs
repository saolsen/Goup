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
		PhotonNetwork.ConnectUsingSettings("alpha-0.8");
	}
	
	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom(null);
		serverOwner = true;
	}
	
	void OnJoinedRoom() {
		GameObject playerController = (GameObject)Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
		GameObject playerModel = PhotonNetwork.Instantiate("PlayerModel", playerController.transform.position, Quaternion.identity, 0);
		
		SyncTransform sync = playerController.GetComponent<SyncTransform>();
		sync.child = playerModel.transform;
		
		playerModel.GetComponent<PhotonView>().RPC("SetNameTag", PhotonTargets.AllBuffered, playerName);
		foreach (MeshRenderer r in playerModel.GetComponentsInChildren<MeshRenderer>()) {
			r.enabled = false;	
		}
		
		if (serverOwner) {
			// Instantiate all the stuff for the world. Or load the level or whatever.
			this.GetComponent<WorldManager>().SpawnWorld();
		}
	}
}