using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldGenerator : MonoBehaviour
{
    public NormalApplicant[] apps;

    public Transform masterTarget;

    public Material textureMat;

    public bool isSolved;

    void Update()
    {
        if (isSolved) return;
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
            mesh.uv = new Vector2[] {
                new Vector2(0,0),
                new Vector2(0.5f,0.5f),
                new Vector2(1,0)
            };
            mesh.triangles = new int[] {2,1,0};
            filter.mesh = mesh;
            renderer.material = textureMat;
            mesh.Optimize();
            mesh.RecalculateNormals();
            StartCoroutine(nameof(Animate));
            isSolved = true;
        }
    }

    IEnumerator Animate()
    {
        var startTime = Time.time;
        var elapsedTime = 0f;
        textureMat.color = new Color(1,1,1,0);

        while (elapsedTime < 2)
        {
            elapsedTime = Time.time - startTime;
            textureMat.color = Color.Lerp(new Color(1,1,1,0), Color.white, elapsedTime / 2f);
            yield return null;
        }
    }

    public bool CheckSolved()
    {
        return isSolved;
    }
}
