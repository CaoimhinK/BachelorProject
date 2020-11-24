using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatrixContainer : MonoBehaviour
{
    public Matrix3x3 mat;
    public MatCol[] cols;

    private static int _matCount = 0;

    public void Update()
    {
        if (cols.Any(col => col.HasContentChanged()))
        {
            mat = new Matrix3x3(
                    cols[0].colValue,
                    cols[1].colValue,
                    cols[2].colValue
                );
            GetComponent<Spawner>().ChangeText("M" + Convert.ToChar(2080 + MatrixContainer.GetMatrixCount()), 200);
            foreach (var col in cols)
            {
                col.ListenToContent();
            }
        }
    }

    public static void IncMatrix()
    {
        _matCount++;
    }

    public static void DecMatrix()
    {
        _matCount--;
    }

    public static int GetMatrixCount()
    {
        return _matCount;
    }
}

public class Matrix3x3
{
    public static readonly Matrix3x3 identity = new Matrix3x3(
            new Vector3(1,0,0),
            new Vector3(0,1,0),
            new Vector3(0,0,1)
        );
    
    private Vector3[] cols;
    
    public Matrix3x3(Vector3 col1, Vector3 col2, Vector3 col3)
    {
        cols = new []
        {
            col1, col2, col3
        };
    }
    
    public Vector3 Multiply(Vector3 vec)
    {
        return new Vector3(
                    cols[0].x * vec.x + cols[1].x * vec.y + cols[2].x * vec.z,
                    cols[0].y * vec.x + cols[1].y * vec.y + cols[2].y * vec.z,
                    cols[0].z * vec.x + cols[1].z * vec.y + cols[2].z * vec.z
                );
    }
}