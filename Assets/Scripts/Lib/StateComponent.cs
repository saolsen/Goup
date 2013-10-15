using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Base class that lets you implement components as explicit state machines.
// To use subclass StateComponent and set stateMachine to your state machine.
// T is the class used to represent your states. Typically an enum or other value type.

public class StateComponent<T> : MonoBehaviour {
	// This class is specific to goup. Because of that it does a few things specific to goup's setup
	// that would need to be modified for another game. Notably it uses the Event system set up on
	// Entity that goup uses for interacting with objects. State machines are set up to specifically
	// handle these events. Also all state changes get broadcast to all clients via goup's specific
	// networking setup and strategy.
	public Entity entity;
	public PhotonView pv;
	
	protected StateMachine<T> stateMachine;
	
	private T current_state;
	private StateProcess active_process;
	
	void OnEnable () {
		entity = gameObject.GetComponent<Entity>();
		if (entity == null) {
			Debug.LogError("Game Object is missing GoupEntity, can not attach GoupComponent");
		}
		
		pv = gameObject.GetComponent<PhotonView>();
		if (pv == null) {
			Debug.LogError("Game Object is missing PhotonView, can not attach GoupComponent");
		}
		
		current_state = stateMachine.initial_state;
		SetProcess(current_state);
	}
	
	void SetProcess(T state) {
		var new_process = stateMachine.state_process[state];
		if (new_process != null) {
			active_process = new_process;	
		} else {
			active_process = new StateProcess();
		}
	}
	
	protected void ChangeState(T newState) {
		if (!Comparer<T>.Equals(current_state, newState)) {
			active_process.exit();
			current_state = newState;
			SetProcess(newState);
			active_process.enter();
		}
	}
	
	// State Process Functions
	void Update () {
		active_process.update();	
	}
	
	void LateUpdate () {
		active_process.late_update();	
	}
	
	void FixedUpdate () {
		active_process.fixed_update();
	}
	
	void OnTriggerEnter (Collider c) {
		active_process.on_trigger_enter(c);
	}
	
	void OnTriggerStay (Collider c) {
		active_process.on_trigger_stay(c);
	}
	
	void OnTriggerExit (Collider c) {
		active_process.on_trigger_exit(c);
	}
	
	void OnCollisionEnter (Collision c) {
		active_process.on_collision_enter(c);
	}
	
	void OnCollisionStay (Collision c) {
		active_process.on_collision_stay(c);
	}
	
	void OnCollisionExit (Collision c) {
		active_process.on_collision_exit(c);
	}
	
	void OnMouseEnter () {
		active_process.on_mouse_enter();
	}

	void OnMouseUp () {
		active_process.on_mouse_up();
	}

	void OnMouseDown () {
		active_process.on_mouse_down();
	}

	void OnMouseOver () {
		active_process.on_mouse_over();
	}

	void OnMouseExit () {
		active_process.on_mouse_exit();
	}

	void OnMouseDrag () {
		active_process.on_mouse_drag();
	}

	void OnGUI () {
		active_process.on_gui();
	}
}
