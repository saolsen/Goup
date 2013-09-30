using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {
	
	public GameObject playerControllerPrefab = null;
	
	bool serverOwner = false;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("alpha-0.1");
	}
	
	void OnGUI () {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
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
		
		if (serverOwner) {
			// Instantiate all the stuff for the world. Or load the level or whatever.
			this.GetComponent<WorldManager>().SpawnWorld();
		}
	}
}