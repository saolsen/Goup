using UnityEngine;
using System;
using System.Collections.Generic;

// Type providers would be so great here, instead seems like I'll need to add a
// field every time I add an event to Events.cs
public class EventHandlers {
	
}

public class StateProcess {
	static Action DoNothing = (() => { });
	static Action<Collider> DoNothingCollider = ((c) => { });
	static Action<Collision> DoNothingCollision = ((c) => { });
	
	// Called when state is exited or entered
	public Action enter = DoNothing;
	public Action exit = DoNothing;
	
	// Unity component handlers for current state.
	public Action update = DoNothing;
	public Action late_update = DoNothing;
	public Action fixed_update = DoNothing;
	public Action<Collider> on_trigger_enter = DoNothingCollider;
	public Action<Collider> on_trigger_stay = DoNothingCollider;
	public Action<Collider> on_trigger_exit = DoNothingCollider;
	public Action<Collision> on_collision_enter = DoNothingCollision;
	public Action<Collision> on_collision_stay = DoNothingCollision;
	public Action<Collision> on_collision_exit = DoNothingCollision;
	public Action on_mouse_enter = DoNothing;
	public Action on_mouse_up = DoNothing;
	public Action on_mouse_down = DoNothing;
	public Action on_mouse_over = DoNothing;
	public Action on_mouse_exit = DoNothing;
	public Action on_mouse_drag = DoNothing;
	public Action on_gui = DoNothing;
	
	// Goup Event Handlers
	public EventHandlers event_handlers;
}

public class StateMachine<T> {
	public T initial_state;
	public IDictionary<T,StateProcess> state_process;
}