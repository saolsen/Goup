// Because Compiling and restarting unity is way too long a feedback loop

// The interactive console is getting wonky, I think it gets run from a different directory. Hardcoding paths now :(
// @Joel if you made it here good luck...
#I "/Applications/Unity/Unity.app/Contents/Frameworks/Managed"
#I "/Users/steveo/Dropbox/unity/GoupTwo/GoupLib"
#I "/Users/steveo/Dropbox/unity/GoupTwo/Assets/libnoise"
#r "UnityEngine.dll"
#r "LibNoise.dll"
#load "PreceduralGeneration.fs"

open GoupLib.PreceduralGeneration
open System
open System.Drawing
open System.Windows.Forms
open LibNoise

let perlin = new LibNoise.Perlin()

// Display or something...
let form = new Form()
let pb = new PictureBox()

//Image.FromHbitmap
form.Controls.Add(pb)
//Application.Run(form)

[<STAThread>]
do
    Application.Run(form)
