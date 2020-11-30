using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldGenerator : MonoBehaviour
{
    public NormalApplicant[] apps;

    public Transform masterTarget;

    void Update()
    {
        var isCorrect = true;
        foreach (var app in apps)
        {
            isCorrect = isCorrect && app.isCorrect;
        }
        if (isCorrect)
        {
            var filter = GetComponent<MeshFilter>();
            var renderer = GetComponent<MeshRenderer>();
            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            foreach (var app in apps)
            {
                vertices.Add(app.tip.transform.position - transform.position);
            }
            mesh.SetVertices(vertices);
            mesh.triangles = new int[] {2,1,0};
            filter.mesh = mesh;
            mesh.Optimize();
            mesh.RecalculateNormals();
        }
    }
}
