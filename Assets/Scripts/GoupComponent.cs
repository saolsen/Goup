using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GoupEvent {
	PickUp,
	Drop
}

public delegate void PickUpEventHandler();

//public enum State {
//	Resting,
//	Falling
//}
//
//public delegate void VoidFn();
//
//public class EventHandlers {
//	public PickUpEventHandler pickup;
//}
//
//public class StateProcess<States> {
//	public States state;
//	public VoidFn enter;
//	public VoidFn exit;
//	public VoidFn update;
//	public VoidFn fixed_update;
//	public EventHandlers event_handlers;
//}
//
//public class StateMachine<States> {
//	public States init;
//	public IDictionary<string,object> properties;
//	public IList<StateProcess<States>> state_process;
//}
//
//public class GoupComponent : MonoBehaviour {
//	
//	private GoupEntity entity;
//
//	// Use this for initialization
//	void OnEnable () {
//		entity = gameObject.GetComponent<GoupEntity>();
//		if (entity == null) {
//			Debug.LogError("Game Object is missing GoupEntity, can not attach GoupComponent");
//		}
//	}
//	
//	// Update is called once per frame
//	void Start () {
//		//entity.goup_event += (e, args) => Debug.Log ("hit");
//		
//		var state_machine = new StateMachine<State> {
//			init = State.Resting,
//			properties = new Dictionary<string, object>(),
//			state_process = new StateProcess<State>[] {
//				new StateProcess<State> {
//					state = State.Resting,
//					enter = () => Debug.Log("entering state resting"),
//					update = () => Debug.Log("in state resting"),
//					event_handlers = new EventHandlers {
//						pickup = () => Debug.Log("picking up")
//					}
//				}
//			}
//		};
		
		
		
		
//		var obj = new {
//			init = new  { init = Resting },
//			properties = new  { speed = 0 },
//			state_machine = new object[] {
//				new  {
//					state = Resting,
//					enter = () => Debug.Log("entering state"),
//					update = () => Debug.Log("updating"),
//					exit = () => Debug.Log("exiting state"),
//					event_handlers = new object[] {
//						new object[] {
//							GoupEvent.PickUp,
//							(x) => Debug.Log("picking up, maybe this returns a new state")
//						},
//						new object[] {
//							GoupEvent.Drop,
//							(x) => Debug.Log("other shit")
//						}
//					}
//				},
//				new  {
//					state = Falling,
//					enter = () => Debug.Log("entering falling state"),
//					update = () => Debug.Log("fall"),
//					event_handlers = new object[] {}
//				}
//			}
//		};
//	}
//}
