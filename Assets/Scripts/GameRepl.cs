using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameRepl : MonoBehaviour {
	
	// Networking
	PhotonView pv;
	
	// Environments for each player's scripts to execute in.
	Dictionary<PhotonPlayer, object> replEnvironments = new Dictionary<PhotonPlayer, object>();
	
	// Repl
	List<string> replHistory = new List<string>();
	string currentForm = "";
	string currentLine = "";
	
	// GUI
	bool replVisible = false;
	Vector2 scroll_position = Vector2.zero;
	
	[RPC]
	void SetupPlayer (PhotonMessageInfo info) {
		//todo sandboxing!
		//var env = IronScheme.RuntimeExtensions.Eval("(new-interaction-environment)");
		//replEnvironments.Add(info.sender, env);
		//Debug.Log(IronScheme.RuntimeExtensions.Eval("(import (ironscheme clr)) (clr-using UnityEngine)", env).ToString());
		//IronScheme.RuntimeExtensions.Eval(String.Format("(library-path (cons \"{0}\" (library-path)))", Application.streamingAssetsPath), env);
	}
	
	// Runs on the server
	[RPC]
	void Eval (string form, PhotonMessageInfo info) {
		var player = info.sender;
		//var env = replEnvironments[player];
		try {
			var result = IronScheme.RuntimeExtensions.Eval(form);
			pv.RPC("ReturnValue", player, result.ToString());
		} catch (Exception e) {
			pv.RPC ("ReturnValue", player, e.ToString());
		}	
	}
	
	// Callback to the client
	[RPC]
	void ReturnValue (string val) {
		replHistory.Add(">" + val);
	}

	// Use this for initialization
	void Start () {
		// This runs on an entity on each client. It's all controlled by the person who's in charge of it on the network though so while every person has their own
		// stuff going on everything actually executes on the server. (this is good...)
		pv = this.GetComponent<PhotonView>();
		
		// todo, do this per player in sandboxing!
		IronScheme.RuntimeExtensions.Eval("(import (ironscheme clr)) (clr-using UnityEngine)");
		IronScheme.RuntimeExtensions.Eval(String.Format("(library-path (cons \"{0}\" (library-path)))", Application.streamingAssetsPath));
		
		pv.RPC("SetupPlayer", PhotonTargets.MasterClient);
	}
	
	bool parensMatch(string form) {
		int lefts = form.Split('(').Length - 1;
		int rights = form.Split(')').Length - 1;
		return lefts == rights;
	}
	
	void OnGUI () {
		if (replVisible) {
			GUILayout.BeginArea(new Rect(Screen.width / 3 * 2, 0, Screen.width / 3, Screen.height));
			GUILayout.Label("Scheme Repl");
			scroll_position = GUILayout.BeginScrollView(scroll_position, false, true);
			
			GUILayout.TextArea(String.Join("\n", replHistory.ToArray()));
			
			GUILayout.EndScrollView();
			
			GUI.SetNextControlName("repl-input");
			currentLine = GUILayout.TextField(currentLine);
			GUILayout.EndArea();
			
			if (Event.current.isKey && Event.current.keyCode == KeyCode.Return) {
				replHistory.Add(currentLine);
				currentForm = currentForm + "\n" + currentLine;
				currentLine = "";
				if (parensMatch(currentForm) && currentForm != "") {
					pv.RPC ("Eval", PhotonTargets.MasterClient, currentForm);
					currentForm = "";
				}
				scroll_position.y = 100000;
			}
			
		}
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			var input = GameObject.FindWithTag("Player").GetComponent<vp_FPInput>();
			if (replVisible) {
				replVisible = false;
				input.ForceCursor = false;
			} else {
				replVisible = true;
				input.ForceCursor = true;
			}
		}
	}
}
