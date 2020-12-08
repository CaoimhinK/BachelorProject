using System.Collections;
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
            //var result = rec.GetMatrix().MultiplyPoint(go.transform.localPosition);
            var mat = rec.GetMatrix();
            var mesh = go.GetComponent<MeshFilter>().mesh;
            TransformMesh(mesh, mat);
            // if (!float.IsNaN(result.x))
            // {
            //     StartCoroutine(nameof(MoveGo), result);
            // }
            // else
            // {
            //     Debug.Log("is NaN");
            // }
        }
    }

    private void TransformMesh(Mesh mesh, Matrix4x4 mat)
    {
        var oldVertices = mesh.vertices;
        var newVertices = new Vector3[oldVertices.Length];
        for (var i = 0; i < oldVertices.Length; i++)
        {
            newVertices[i] = mat.MultiplyPoint(oldVertices[i]);
        }

        mesh.vertices = newVertices;
        mesh.Optimize();
        mesh.RecalculateNormals();
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
