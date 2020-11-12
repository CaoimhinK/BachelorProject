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
        vec = new Vector3(vecs[0].GetValue(),vecs[1].GetValue(), vecs[2].GetValue());
        if (!vec.Equals(_last))
        {
            var strHead = "Vec\n(";
            var strBody = (int) vec.x + "," + (int) vec.y + "," + (int) vec.z + ")";
            var size = Mathf.RoundToInt(Mathf.Min((7f / strBody.Length) * 80f, 80f));
            GetComponent<Spawner>().ChangeText(strHead, strBody, size);
        }
    }
}
