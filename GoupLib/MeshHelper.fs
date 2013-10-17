// This is written in fsharp because I want this class to be immutable and c# doesn't have
// An immutable persistant list type.
module GoupLib.MeshHelper
open UnityEngine

type GoupMesh = { vertices  : Vector3 list;
                  triangles : int list;
                  normals   : Vector3 list;}

// Derived Properties and Objects
let private getMesh (goupMesh : GoupMesh) =
    let mesh = new Mesh()
    mesh.vertices <- List.toArray goupMesh.vertices
    mesh.triangles <- List.toArray goupMesh.triangles
    mesh.normals <- List.toArray goupMesh.normals
    //mesh.RecalculateBounds()
    mesh
    
// Transformers
let private translate (goupMesh : GoupMesh) (translation : Vector3) : GoupMesh =
    let newVerts = List.map (fun v -> v + translation) goupMesh.vertices
    { vertices = newVerts; triangles = goupMesh.triangles; normals = goupMesh.normals}
    
let private combine (m1 : GoupMesh) (m2 : GoupMesh) : GoupMesh =
    let trianglesInc = m1.vertices.Length
    let newTriangles = List.map ((+) trianglesInc) m2.triangles
    { vertices = List.append m1.vertices m2.vertices;
      triangles = List.append m1.triangles newTriangles;
      normals = List.append m1.normals m2.normals}
    
// Primitives
let Plane (width : Vector3) (height : Vector3) : GoupMesh =
    let normal = Vector3.Cross(height, width).normalized;
    let vertices = [Vector3.zero
                    width;
                    width + height;
                    height]
    let triangles = [2; 1; 0;
                     0; 3; 2]
    let normals = [normal; normal; normal; normal]
    {vertices = vertices; triangles = triangles; normals = normals}

let Box (width : Vector3) (height : Vector3) (depth : Vector3) : GoupMesh =
    // This was just in my head, not sure of an algorithm yet but I bet there is one.
    let planes = [Plane width height;
                  translate (translate (Plane -width height ) width) depth;
                  translate (Plane width depth) height;
                  translate (Plane -width depth) width;
                  Plane height depth;
                  translate (translate (Plane -height depth) width) height]
    List.reduce combine planes

let FromMesh (mesh : Mesh) =
    {vertices = Array.toList mesh.vertices;
     triangles = Array.toList mesh.triangles;
     normals = Array.toList mesh.normals;}
    
// Put methods on the type so they can be chained from c#
type GoupMesh with
    member m.GetMesh() =
        getMesh m
    member m.Combine(otherMesh : GoupMesh) =
        combine m otherMesh
    member m.Translate(translation : Vector3) =
        translate m translation