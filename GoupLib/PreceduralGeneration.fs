module GoupLib.PreceduralGeneration
open UnityEngine

open LibNoise

let perlin = new Perlin()

// 0 to 512
let size = 513
let scale = (float 10.0)

let array = Array2D.init size size (fun x z -> 
    perlin.GetValue((float x) / (float size) * scale, (float 0.0), (float z) / (float size) * scale) / 10.0)
