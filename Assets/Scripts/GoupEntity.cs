using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoupEntity : MonoBehaviour {
	
	// Data
	public Dictionary<string,object> Data = new Dictionary<string, object>();
		
	// Events
	public event PickUpEventHandler goup_event;
	
	void Update () {
		//goup_event(GoupEvent.PickUp);
	}

	
}