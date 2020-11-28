using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public NormalApplicant[] apps;

    public Transform masterTarget;

    void Awake()
    {
        /*
        foreach (var app in apps) {
            app.target.position = masterTarget.position;
            app.origin.position = transform.position;
            app.UpdateTarget();
        }
        */
    }
}
