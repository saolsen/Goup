using UnityEngine;
using System.Collections;

public class PreceduralTerrainChunk : MonoBehaviour {

	[RPC]
	void SetHeightMap(int seed, Vector3 location) {
		var heights = TerrainGenerator.GetHeightsForQuadrent (seed, location, 513);
		gameObject.GetComponent<Terrain> ().terrainData.SetHeights (0, 0, heights);
	}

}