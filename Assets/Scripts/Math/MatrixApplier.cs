using System;
using System.Collections;
using UnityEngine;

public class MatrixApplier : MonoBehaviour
{
    public Recepticle rec;
    public GameObject go;
    public GameObject target;
    public bool chainMatrix;

    private bool _animating;
    private float _startTime;
    private Vector3[] _startVertices;
    private Vector3[] _endVertices;
    private Material _mat;
    private Material _targetMat;

    private Color _col;
    private Mesh _startMesh;
    private Mesh _mesh;

    public void Start()
    {
        _mat = go.GetComponent<MeshRenderer>().material;
        _targetMat = target.GetComponent<MeshRenderer>().material;
        _col = _targetMat.color;
        _mesh = go.GetComponent<MeshFilter>().mesh;
        _startMesh = Instantiate(_mesh);
    }

    public void Update()
    {
        if (_animating) Animate();
    }

    void OnTriggerEnter(Collider other)
    {
        go.transform.position += Vector3.up * 0.1f;
        target.transform.position += Vector3.up * 0.1f;
        _mat.color = Color.red;
        _targetMat.color = Color.cyan;
    }

    void OnTriggerExit(Collider other)
    {
        go.transform.position += Vector3.down * 0.1f;
        target.transform.position += Vector3.down * 0.1f;
        _mat.color = Color.white;
        _targetMat.color = _col;
    }

    public void ApplyMatrix()
    {
        if (!_animating)
        {
            var mat = rec.GetMatrix();

            if (chainMatrix)
            {
                _startVertices = _mesh.vertices;
            }
            else
            {
                _startVertices = _startMesh.vertices;
            }
            
            _endVertices = new Vector3[_startVertices.Length];
            
            for (var i = 0; i < _endVertices.Length; i++)
            {
                _endVertices[i] = mat.MultiplyPoint(_startVertices[i]);
            }

            StartCoroutine(nameof(MoveGo));
        }
    }

    public bool IsCorrect()
    {
        return false;
    }

    void Animate()
    {
        var currentTime = Time.time - _startTime;
        var vertices = new Vector3[_mesh.vertices.Length];
        for (var i = 0; i < _mesh.vertices.Length; i++)
        {
            vertices[i] = Vector3.Lerp(_startVertices[i], _endVertices[i], currentTime / 0.5f);
        }

        _mesh.vertices = vertices;
        _mesh.RecalculateNormals();
    }
    
    IEnumerator MoveGo()
    {
        _animating = true;
        _startTime = Time.time;
        yield return new WaitForSeconds(0.5f);
        _animating = false;
    }
}
