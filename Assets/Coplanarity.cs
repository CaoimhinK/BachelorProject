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
            Debug.Log(_distance);
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
        _distance = Math.Abs(Vector3.Dot(normal, third));
    }

    private bool IsCorrect()
    {
        var answerDistance = Mathf.Abs(_distance - Mathf.Abs(answer.GetValue()));
        return answerDistance < 0.01f;
    }

    public bool CheckSolved()
    {
        return _isCorrect;
    }
}
