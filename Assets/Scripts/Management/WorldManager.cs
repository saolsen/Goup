using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldManager : MonoBehaviour {

	int seed;
	IList<GameObject> terrain_chunks;

	public void GenerateTerrainChunks() {
		// Generate 4 terrain chunks (to start)
		//		3|0
		//		2|1

		//TODO: figure out how to duplicate the terrain asset at runtime and not use 4 prefabs.
		terrain_chunks = new List<GameObject> {
			PhotonNetwork.Instantiate ("BaseTerrain1", Vector3.zero, Quaternion.identity, 0),
			PhotonNetwork.Instantiate ("BaseTerrain2", new Vector3(0.0f, 0.0f, -2000.0f), Quaternion.identity, 0),
			PhotonNetwork.Instantiate ("BaseTerrain3", new Vector3(-2000.0f, 0.0f, -2000.0f), Quaternion.identity, 0),
			PhotonNetwork.Instantiate ("BaseTerrain4", new Vector3(-2000.0f, 0.0f, 0.0f), Quaternion.identity, 0)
		};

		foreach (var chunk in terrain_chunks) {
			var pv = chunk.GetComponent<PhotonView> ();
			var location = chunk.transform.position;
			pv.RPC("SetHeightMap", PhotonTargets.AllBuffered, seed, location);
		}

		// again, an algorithm for all of this will be much better once I figure out how to create terrain assets at runtime.
		var t1 = terrain_chunks [0].GetComponent<Terrain> ();
		var t2 = terrain_chunks [1].GetComponent<Terrain> ();
		var t3 = terrain_chunks [2].GetComponent<Terrain> ();
		var t4 = terrain_chunks [3].GetComponent<Terrain> ();

		t1.SetNeighbors (t4, null, null, t2);
		t2.SetNeighbors( t3, t1, null, null);
		t3.SetNeighbors(null, t4, t2, null);
		t4.SetNeighbors(null, null, t1, t3);
	}

	public void GenerateTestGoup() {
		var height = terrain_chunks [0].GetComponent<Terrain> ().SampleHeight (Vector3.zero);
		var above = new Vector3 (0, height + 300, 0);

		foreach (int i in Enumerable.Range(0, 15)) {
			var scale = Random.onUnitSphere * Random.Range(2, 10);
			var mass = scale.magnitude * 2;
			var newGoup = PhotonNetwork.Instantiate("TestGoup", (Random.onUnitSphere * 9) + above, Quaternion.identity, 0);
			var pv = newGoup.GetComponent<PhotonView>();
			pv.RPC("Setup", PhotonTargets.AllBuffered, scale, mass);
		}

		PhotonNetwork.Instantiate("Antisun", new Vector3(4, 700, 4), Quaternion.identity, 0);

	}

	public void SpawnWorld () {
		seed = Random.Range (0, 10000);
		GenerateTerrainChunks ();
		GenerateTestGoup ();

	}
}