using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class MathObj : MonoBehaviour
{
    private ObjType _type;
    public ObjType Type
    {
        get
        {
            return _type;
        }
        set
        {
            if (value == ObjType.Matrix) MatrixContainer.IncMatrix();
            _type = value;
        }
    }
    public float value;
    public Vector3 vecValue;
    public Matrix4x4 matValue = Matrix4x4.identity;

    private void OnDestroy()
    {
        //if (_type == ObjType.Matrix) MatrixContainer.DecMatrix();
    }
}
