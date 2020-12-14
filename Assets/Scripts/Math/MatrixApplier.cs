using System.Collections;
using UnityEngine;

public class MatrixApplier : MonoBehaviour
{
    public Recepticle rec;
    public GameObject go;
    public bool chainMatrix;

    private bool _animating;
    private float _startTime;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Quaternion _startRot;
    private Quaternion _endRot;
    private Vector3 _startScale;
    private Vector3 _endScale;
    private Matrix4x4 _startMatrix;
    private Material _mat;

    public void Start()
    {
        var startPos = go.transform.localPosition;
        var startRot = go.transform.localRotation;
        var startScale = go.transform.localScale;
        _startMatrix = Matrix4x4.TRS(startPos, startRot, startScale);
        _mat = go.GetComponent<MeshRenderer>().material;
    }

    public void Update()
    {
        if (_animating) Animate();
    }

    void OnTriggerEnter()
    {
        go.transform.position += Vector3.up * 0.1f;
        _mat.color = Color.red;
    }

    void OnTriggerExit()
    {
        go.transform.position += Vector3.down * 0.1f;
        _mat.color = Color.white;
    }

    public void ApplyMatrix()
    {
        if (!_animating)
        {
            var mat = rec.GetMatrix();
            _startPos = go.transform.localPosition;
            _startRot = go.transform.localRotation;
            _startScale = go.transform.localScale;
            var startMat = Matrix4x4.TRS(_startPos, _startRot, _startScale);
            Matrix4x4 endMat;
            if (chainMatrix)
            {
                endMat = mat * startMat;
            }
            else
            {
                endMat = mat * _startMatrix;
            }
            _endPos = new Vector3(endMat.m03, endMat.m13, endMat.m23);
            _endRot = endMat.rotation;
            _endScale = new Vector3(endMat.lossyScale.x, endMat.lossyScale.y, endMat.lossyScale.z);
            StartCoroutine(nameof(MoveGo));
        }
    }

    void Animate()
    {
        var currentTime = Time.time - _startTime;
        go.transform.localPosition = Vector3.Lerp(_startPos, _endPos, currentTime / 0.5f);
        go.transform.localRotation = Quaternion.Lerp(_startRot, _endRot, currentTime / 0.5f);
        go.transform.localScale = Vector3.Lerp(_startScale, _endScale, currentTime / 0.5f);
    }
    
    IEnumerator MoveGo()
    {
        _animating = true;
        _startTime = Time.time;
        yield return new WaitForSeconds(0.5f);
        go.transform.localPosition = _endPos;
        _animating = false;
    }
}
