using System;
using Noidel;
using UnityEngine;

public class Coplanarity : MonoBehaviour
{
    public Transform[] points;
    public Spawner[] spawns;
    public Recepticle answer;
    public Button but;

    private float _distance;
    private bool _isCorrect;

    private void Update()
    {
        if (!_isCorrect && answer.HasObject() && but.WasPushed() && IsCorrect())
        {
            _isCorrect = true;
        }
    }

    void Start()
    {
        for (var i = 0; i < 4; i++)
        {
            spawns[i].spawnVecValue = points[i].localPosition;
        }

        var origin = points[0].localPosition;
        var first = points[1].localPosition - origin;
        var second = points[2].localPosition - origin;
        var third = points[3].localPosition - origin;
        var normal = Vector3.Cross(first, second).normalized;

        var mesh = new Mesh();
        var specialPoint = Vector3.ProjectOnPlane(points[3].localPosition - origin, normal) + origin;
        mesh.vertices = new Vector3[]
        {
            origin,
            points[1].localPosition,
            points[2].localPosition,
            specialPoint
        };
        mesh.triangles = new int[]
        {
            0,1,2,1,3,2
        };
        mesh.Optimize();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;

        var cyl = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cyl.GetComponent<MeshRenderer>().material.color = Color.red;
        cyl.transform.parent = points[0].parent;
        _distance = Vector3.Dot(third, normal);
        Debug.Log(_distance);
        cyl.transform.localPosition = specialPoint + ((points[3].localPosition - specialPoint) / 2f);
        cyl.transform.localScale = new Vector3(0.1f, _distance / 2f, 0.1f);
        cyl.transform.up = normal;
    }

    private bool IsCorrect()
    {
        var answerDistance = Mathf.Abs(_distance - answer.GetValue());
        return answerDistance < 0.01f;
    }

    public bool CheckSolved()
    {
        return _isCorrect;
    }
}
