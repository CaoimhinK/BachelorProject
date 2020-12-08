using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixableMesh : MonoBehaviour
{
    public Vector3[] points;
    public int[] triangles;
    
    // Start is called before the first frame update
    void Start()
    {
        var mesh = new Mesh();
        mesh.vertices = points;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
