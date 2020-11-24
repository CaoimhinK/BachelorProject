using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            var pos2D = new Vector3(go.transform.localPosition.x, go.transform.localPosition.z, 1);
            var result = rec.GetMatrix().Multiply(pos2D);
            Debug.Log(pos2D + " " + result);
            StartCoroutine(nameof(MoveGo), new Vector3(result.x, go.transform.localPosition.y, result.y));
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
