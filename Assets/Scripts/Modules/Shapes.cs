using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Shapes : MonoBehaviour
{
    public MatrixApplier[] apps;
    private bool _isCorrect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            foreach (var app in apps)
            {
                Debug.Log(app.IsCorrect());
            }
        }
    }

    public bool CheckSolved()
    {
        return apps.All((app) => app.IsCorrect());
    }
}
