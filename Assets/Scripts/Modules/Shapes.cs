using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Shapes : MonoBehaviour
{
    public MatrixApplier[] apps;
    public GameObject[] solution;

    private bool _isCorrect;
    private bool _correctShown;

    private void Update()
    {
        if (_isCorrect && !_correctShown) {
            foreach (var sol in solution) {
                sol.GetComponent<MeshRenderer>().material.color = new Color(0,1,0,0.3f);
            }
            foreach (var app in apps) {
                app.Disable();
            }
            _correctShown = true;
        }
    }

    public bool CheckSolved()
    {
        _isCorrect = apps.All((app) => app.IsCorrect());
        return apps.All((app) => app.IsCorrect());
    }
}
