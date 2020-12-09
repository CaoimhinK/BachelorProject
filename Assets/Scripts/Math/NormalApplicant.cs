using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class NormalApplicant : MonoBehaviour
{
    public Transform pivot;
    public Transform goalObj;
    public Transform target;
    public Transform origin;
    public Transform tip;
    public Spawner[] vertices;
    public GameObject vertexIndicator;
    public bool isCorrect;

    private int _activeVertexCount;
    
    private void Awake() {
        goalObj.SetParent(origin);
        pivot.SetParent(origin);
        vertexIndicator.transform.SetParent(origin);
        vertexIndicator.SetActive(false);
        UpdateTarget();
    }

    private void UpdateTarget() {
        goalObj.LookAt(target);
        var pos = goalObj.localPosition;
        var up = goalObj.up;
        var right = goalObj.right;
        vertices[0].spawnVecValue = pos + up - right;
        vertices[1].spawnVecValue = pos + up + right;
        vertices[2].spawnVecValue = pos - up + right;
        vertices[3].spawnVecValue = pos - up - right;
    }

    public void SetLocalRot(Quaternion quat) {
        pivot.localRotation = quat;
    }

    public void VertexTriggered(int index)
    {
        _activeVertexCount++;
        vertexIndicator.SetActive(true);
        vertexIndicator.transform.localPosition = vertices[index].spawnVecValue;
    }
    public void VertexLeft(int index)
    {
        _activeVertexCount--;
        if (_activeVertexCount == 0)
        {
            vertexIndicator.SetActive(false);
        }
    }

    public Vector3 CorrectDir()
    {
        return Vector3.Normalize(target.position - tip.position);
    }

    public Quaternion GetLocalRot() {
        return pivot.localRotation;
    }
}