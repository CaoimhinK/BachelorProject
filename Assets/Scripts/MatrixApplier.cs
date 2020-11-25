using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class MatrixApplier : MonoBehaviour
{
    public Recepticle rec;
    public GameObject go;

    private bool _animating;
    private float _startTime;
    private Vector3 _startPos;
    private Vector3 _endPos;

    public void Update()
    {
        if (_animating) Animate();
    }

    public void ApplyMatrix()
    {
        if (!_animating)
        {
            var result = rec.GetMatrix().MultiplyPoint(go.transform.localPosition);
            if (!float.IsNaN(result.x))
            {
                StartCoroutine(nameof(MoveGo), result);
            }
            else
            {
                Debug.Log("is NaN");
            }
        }
    }

    void Animate()
    {
        var currentTime = Time.time - _startTime;
        go.transform.localPosition = Vector3.Lerp(_startPos, _endPos, currentTime / 0.5f);
    }
    
    IEnumerator MoveGo(Vector3 newPos)
    {
        _animating = true;
        _startTime = Time.time;
        _startPos = go.transform.localPosition;
        _endPos = newPos;
        yield return new WaitForSeconds(0.5f);
        go.transform.localPosition = _endPos;
        _animating = false;
    }
}
