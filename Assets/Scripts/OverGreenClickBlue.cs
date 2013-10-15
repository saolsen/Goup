using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ColorState {
	Red,
	Green,
	Blue
}

public class OverGreenClickBlue : StateComponent<ColorState> {
	
	void Awake () {
		stateMachine = new StateMachine<ColorState> {
			initial_state = ColorState.Red,
			state_process = new Dictionary<ColorState,StateProcess> {
				{ColorState.Red, new StateProcess {
						enter = () => {
							renderer.material.color = Color.red;
						},
						on_mouse_enter = () => {
							ChangeState(ColorState.Blue);
						}
					}},
				{ColorState.Blue, new StateProcess {
						enter = () => {
							renderer.material.color = Color.blue;	
						},
						on_mouse_exit = () => {
							ChangeState(ColorState.Red);	
						}
					}}
			}
		};
	}
}
