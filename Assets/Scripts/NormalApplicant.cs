using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NormalApplicant : MonoBehaviour
{
    public Transform pivot;
    public Transform goalObj;
    public Transform target;
    public Transform origin;
    public Spawner[] vertices;
    
    private void Awake() {
        goalObj.SetParent(origin);
        pivot.SetParent(origin);
        UpdateTarget();
    }

    public void UpdateTarget() {
        goalObj.LookAt(target);
        vertices[0].spawnVecValue = goalObj.localPosition + goalObj.up - goalObj.right;
        vertices[1].spawnVecValue = goalObj.localPosition + goalObj.up + goalObj.right;
        vertices[2].spawnVecValue = goalObj.localPosition - goalObj.up + goalObj.right;
        vertices[3].spawnVecValue = goalObj.localPosition - goalObj.up - goalObj.right;
    }

    public void SetLocalRot(Quaternion quat) {
        pivot.localRotation = quat;
    }

    public Quaternion GetLocalRot() {
        return pivot.localRotation;
    }
}