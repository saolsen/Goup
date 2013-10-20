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

let width = 513
let height = 513

// Display or something...
let form = new Form()
let pb = new PictureBox(BackColor = Color.White,
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.CenterImage)

let bm = new Bitmap(width, height)                 
let gfx = Graphics.FromImage(bm)

for x in 0..512 do
    for z in 0..512 do
        let v = Array2D.get grays x z
        bm.SetPixel(x, z, Color.FromArgb(v, v, v))

//Image.FromHbitmap
form.Controls.Add(pb)
//Application.Run(form)

[<STAThread>]
do
    Application.Run(form)
