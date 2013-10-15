using UnityEngine;
using System.Collections;
using System.Linq;

public class TestTerrainGen : MonoBehaviour {
	
	public float scale = 10.0f;
	
	void Start () {
		Debug.Log ("Generating Terrain");

		var terrainData = Terrain.activeTerrain.terrainData;
		var width = terrainData.heightmapWidth;
		var height = terrainData.heightmapHeight;

		// My research tells me there's no functional way to do this, embrace the demons.
		var array = new float[width,height];

		for (int x = 0; x < width; ++x) {
			for (int z = 0; z < height; ++z) {
				var val = Mathf.PerlinNoise ((float)x / (float)width * scale, (float)z / (float)height * scale)/10.0f;

				array [x, z] = val;
			}
		}

		terrainData.SetHeights (0, 0, array);
		
	}
}
