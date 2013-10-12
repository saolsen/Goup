namespace GoupLib.BaseComponent
open UnityEngine

type BaseComponent () =
    inherit MonoBehaviour()
    
    let event = new Event<_>()
    
    // Shared data that all components can access and edit.
    // Events that all components can subscribe to.
    // Code to handle adding events, removing events and triggering events.
    
    member this.Awake () =
        // Set up everything.
        Debug.Log("enabled")
