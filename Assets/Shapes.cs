using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shapes : MonoBehaviour
{
    public MatrixApplier[] apps;
    public bool isCorrect;

    private void Update()
    {
        isCorrect = isCorrect || apps.All((app) => app.IsCorrect());
    }
}
