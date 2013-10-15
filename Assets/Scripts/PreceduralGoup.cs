using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoupLib;

public class PreceduralGoup : MonoBehaviour {
	
	public Material mat;
	
	void Start () {
		Debug.Log("here we go");
		
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		
		var m = Precedural.Box(new Vector3(1, 0, 0), new Vector3(0, 4, 0), new Vector3(0, 0, 2));
		
		Mesh mesh = m.GetMesh();
		
		MeshFilter mesh_filter = gameObject.GetComponent<MeshFilter>();
		
		mesh_filter.mesh = mesh;
		
		MeshRenderer mesh_renderer = gameObject.GetComponent<MeshRenderer>();
		mesh_renderer.material = mat;
	}
	
}
