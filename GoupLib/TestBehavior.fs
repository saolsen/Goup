namespace GoupLib.TestBehavior
open UnityEngine

type TestBehavior () =
    inherit MonoBehaviour()
    
    member this.Start () =
        Debug.Log("hello")
        Debug.Log("hello again")