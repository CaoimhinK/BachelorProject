﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalApplier : MonoBehaviour
{
    public GameObject pivot;
    public Recepticle rec;

    private bool _animating;
    private float _startTime;
    private Quaternion _startRot;
    private Quaternion _endRot;

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
        pivot.transform.localRotation = Quaternion.Lerp(_startRot, _endRot, currentTime / 0.5f);
    }
    
    IEnumerator RotGo(Vector3 endRot)
    {
        _animating = true;
        _startTime = Time.time;
        _startRot = pivot.transform.localRotation;
        _endRot = Quaternion.LookRotation(endRot);
        yield return new WaitForSeconds(0.5f);
        pivot.transform.localRotation = _endRot;
        _animating = false;
    }
}
