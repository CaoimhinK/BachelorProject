using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class VectorObj : MonoBehaviour
{
    public Vector3 vec;
    private Recepticle[] _children;

    private void Start()
    {
        _children = transform.GetComponentsInChildren<Recepticle>();
    }

    private void Update()
    {
        var x = _children[0];
        var y = _children[1];
        var z = _children[2];
        vec = new Vector3(x.GetValue(), y.GetValue(), z.GetValue());        
    }
}
