using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalApplier : MonoBehaviour
{
    public NormalApplicant napp;
    public Recepticle rec;

    private bool _animating;
    private float _startTime;
    private Quaternion _startRot;
    private Quaternion _endRot;
    private bool _isCorrect;

    private void Update()
    {
        if (_animating)
        {
            Animate();
        }
    }

    public void ApplyNormal()
    {
        if (!_animating)
        {
            var vec = rec.GetVector();
            
            if (!vec.Equals(Vector3.zero)) {
                StartCoroutine(nameof(RotGo), vec);
            }
        }
    }

    void Animate()
    {
        var currentTime = Time.time - _startTime;
        napp.SetLocalRot(Quaternion.Lerp(_startRot, _endRot, currentTime / 0.5f));
    }
    
    IEnumerator RotGo(Vector3 endRot)
    {
        _animating = true;
        _startTime = Time.time;
        _startRot = napp.GetLocalRot();
        _endRot = Quaternion.LookRotation(endRot);
        yield return new WaitForSeconds(0.5f);

        Debug.Log(napp.CorrectDir().normalized);
        Debug.Log(rec.GetVector().normalized);

        if (napp.CorrectDir().normalized.Equals(rec.GetVector().normalized)) {
            napp.isCorrect = true;
        }
        _animating = false;
    }
}
