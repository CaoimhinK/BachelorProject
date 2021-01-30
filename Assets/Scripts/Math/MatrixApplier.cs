using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class MatrixApplier : MonoBehaviour
{
    public Container rec;
    public GameObject go;
    public GameObject target;
    public bool chainMatrix;
    public Matrix4x4 correctMatrix = Matrix4x4.identity;

    private bool _animating;
    private float _startTime;
    private Vector3[] _startVertices;
    private Vector3[] _endVertices;
    private Material _mat;
    private Material _targetMat;
    private Vector3[] _correctVertices;

    private Color _col;
    private Mesh _startMesh;
    private Mesh _mesh;

    private bool _solved;

    public void Start()
    {
        _mat = go.GetComponent<MeshRenderer>().material;
        _targetMat = target.GetComponent<MeshRenderer>().material;
        _col = _targetMat.color;
        _mesh = go.GetComponent<MeshFilter>().mesh;
        _startMesh = Instantiate(_mesh);
        var verts = go.GetComponent<MeshFilter>().mesh.vertices;
        _correctVertices = new Vector3[verts.Length];
        for (var i = 0; i < verts.Length; i++)
        {
            _correctVertices[i] = correctMatrix.MultiplyPoint(verts[i]);
        }
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
        if (_animating) return;
        
        var mat = rec.GetMatrix();
        _startVertices = _mesh.vertices;
        _endVertices = new Vector3[_startVertices.Length];

        if (chainMatrix)
        {
            for (var i = 0; i < _endVertices.Length; i++)
            {
                _endVertices[i] = mat.MultiplyPoint(_startVertices[i]);
            }
        }
        else
        {
            for (var i = 0; i < _endVertices.Length; i++)
            {
                _endVertices[i] = mat.MultiplyPoint(_startMesh.vertices[i]);
            }
        }
        StartCoroutine(nameof(MoveGo));
    }

    public bool IsCorrect()
    {
        return _mesh.vertices.All((meshVertex) => _correctVertices.Any((correctVertex) => Vector3.Distance(correctVertex,meshVertex) < 0.05f));
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
    }
    
    IEnumerator MoveGo()
    {
        _animating = true;
        _startTime = Time.time;
        yield return new WaitForSeconds(0.5f);
        _animating = false;
    }
}
