using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalApplier : MonoBehaviour
{
    public NormalApplicant napp;
    public Recepticle rec;
    public bool isCorrect;

    private bool _animating;
    private float _startTime;
    private Quaternion _startRot;
    private Quaternion _endRot;

    private Material mat;
    

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

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
            if (isCorrect)
            {
                StartCoroutine(nameof(AlreadyCorrect));
            }
            else
            {
                var vec = rec.GetVector();
                
                if (!vec.Equals(Vector3.zero)) {
                    StartCoroutine(nameof(RotGo), vec);
                }
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

        if (Vector3.Distance(napp.CorrectDir(), rec.GetVector().normalized) < 0.01f) {
            napp.isCorrect = true;
            isCorrect = true;
        }
        _animating = false;
    }

    IEnumerator AlreadyCorrect()
    {
        var col = mat.color;
        if (col == Color.green) yield break;
        mat.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        mat.color = col;
    }
}
