using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class VectorContainer : MonoBehaviour
{
    public Vector3 vec;
    public Recepticle[] vecs;

    private Vector3 _last;

    public void Update()
    {
        _last = vec;
        var x = vecs[0];
        var y = vecs[1];
        var z = vecs[2];
        vec = new Vector3(x.GetValue(), y.GetValue(), z.GetValue());
        if (!vec.Equals(_last))
        {
            GetComponent<Spawner>().ChangeText();
        }
    }
}
