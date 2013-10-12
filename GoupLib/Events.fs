// Goup Events! Things that can be sent to an entity.
module GoupLib.Events
open System
open UnityEngine

type GoupEvent =
    | PickUp
    | PutDown
    | ThrowDown of float * Vector3