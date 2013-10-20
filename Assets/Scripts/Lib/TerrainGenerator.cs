using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LibNoise.Unity;
using LibNoise.Unity.Generator;
using LibNoise.Unity.Operator;

public static class TerrainGenerator {

	public static bool modules_loaded = false;
	public static int seed = 0;
	public static RidgedMultifractal mountain;
	public static Billow baseFlatTerrain;
	public static ScaleBias flatTerrain;
	//public static Const flatTerrain;
	public static Perlin terrainType;
	public static Select finalTerrain;

	// This lets us load the modules once when the game starts and then treat them as static.
	private static void LoadModules (int game_seed) {
		seed = game_seed;
		// Create noise functions.
		mountain = new RidgedMultifractal ();
		mountain.Seed = seed;

		baseFlatTerrain = new Billow ();
		baseFlatTerrain.Seed = seed;
		baseFlatTerrain.Frequency = 0.5;
		//0.125
		flatTerrain = new ScaleBias (0.0625, -0.75, baseFlatTerrain);
		//flatTerrain = new LibNoise.Unity.Generator.Const (-0.75);

		terrainType = new Perlin ();
		terrainType.Seed = seed;
		terrainType.Frequency = 0.5;
		terrainType.Persistence = 0.125;

		finalTerrain = new Select (flatTerrain, mountain, terrainType);
		finalTerrain.SetBounds (0.25, 1000.0);
		finalTerrain.FallOff = 0.125;

		modules_loaded = true;
		
	}

	// Should be a tuple but nope, unity sux. Should be a type then...
	private static List<int> GetOffsets(Vector3 position, int size) {
		var xpos = Mathf.RoundToInt(position.x);
		var zpos = Mathf.RoundToInt(position.z);
		var xoffset = (xpos / 2000) * (size - 1);
		var zoffset = (zpos / 2000) * (size - 1);

		return new List<int> { xoffset, zoffset };
	}
	
	public static float[,] GetHeightsForQuadrent(int game_seed, Vector3 position, int size) {
		if (!modules_loaded) {
			LoadModules (game_seed);
		}

		// Generate array of heights.
		var array = new float[size,size];

		var offsets = GetOffsets (position, size);

		for (int x = 0; x < size; ++x) {
			for (int z = 0; z < size; ++z) {
				double val = finalTerrain.GetValue ((double)(x + offsets[0]) / 513, 0.0, (double)(z + offsets[1]) / 513);
				double scaled = (val + 1.0) / 2.0;
				double interpolated = System.Math.Round (scaled * 2, 2) / 2;

				array [z, x] = (float)interpolated;
			}
		}

		return array;
	}
}