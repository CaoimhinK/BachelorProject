using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureModule : MonoBehaviour
{
    public GameObject solution;
    public MatrixApplier app;

    private bool _isCorrect;
    private bool _correctShown;

    void Update()
    {
        if (_isCorrect && !_correctShown) {
            solution.GetComponent<MeshRenderer>().material.color = new Color(0,1,0,0.3f);
            _correctShown = true;
            app.Disable();
        }
    }

    public bool IsCorrect()
    {
        _isCorrect = app.IsCorrect();
        return _isCorrect;
    }
}
