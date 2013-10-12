namespace GoupLib.Entity
open GoupLib.Events
open UnityEngine

type Entity () =
    inherit MonoBehaviour()
    
    let event = new Event<GoupEvent>()
    
    // Shared data that all components can access and edit.
    // Events that all components can subscribe to.
    // Code to handle adding events, removing events and triggering events.
    //addevent
    
    member this.Awake () =
        // Set up everything.
        event.Publish.Add(fun x -> Debug.Log("triggered"))
        event.Trigger(ThrowDown 12.0f (new Vector3 1 1 1))