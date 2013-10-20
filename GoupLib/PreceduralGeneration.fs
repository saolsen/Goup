module GoupLib.PreceduralGeneration
open UnityEngine

open LibNoise

let perlin = new Perlin()

// 0 to 512
let size = 513
let scale = (float 10.0)

let noise = Array2D.init size size (fun x z -> 
    perlin.GetValue((float x) / (float size) * scale, (float 0.0), (float z) / (float size) * scale) / 10.0)

// Scale from [-1 1] to 1 255
let toGray (n : float) : int = floor ((n + (float 1.0)) / (float 2) * (float 255))

let grays = Array2D.map toGray noise