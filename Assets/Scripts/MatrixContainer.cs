using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

public class MatrixContainer : MonoBehaviour
{
    public Matrix4x4 Mat
    {
        get
        {
            _mat = CreateMatrix(cols);
            return _mat;
        }
    }
    public MatCol[] cols;
    public Axis axis;

    private Matrix4x4 _mat;
    private static int _matCount = 0;

    private void Start()
    {
        _mat = CreateMatrix(cols);
        var spaw = GetComponent<Spawner>();
        spaw.LoadTex();
        spaw.ChangeText("M", 200);
    }

    public void Update()
    {
        if (cols.Any(col => col.HasContentChanged()))
        {
            _mat = CreateMatrix(cols);
            foreach (var col in cols)
            {
                col.ListenToContent();
            }
        }
    }

    private Matrix4x4 CreateMatrix(MatCol[] columns)
    {
        Matrix4x4 newMat;
        if (columns.Length == 4)
        {
            newMat = new Matrix4x4(
                columns[0].colValue, columns[1].colValue, columns[2].colValue, columns[3].colValue
            );
        }
        else
        {
            var colA = columns[0].colValue;
            var colB = columns[1].colValue;
            var colC = columns[2].colValue;
            switch (axis)
            {
                case Axis.Z:
                    newMat = new Matrix4x4(
                            new Vector4(colA.x, colA.y, 0, colA.z),
                            new Vector4(colB.x, colB.y, 0, colB.z),
                            new Vector4(0,0,1,0),
                            new Vector4(colC.x, colC.y, 0, colC.z)
                        );
                    break;
                case Axis.X:
                    newMat = new Matrix4x4(
                            new Vector4(1,0,0,0),
                            new Vector4(0, colA.x, colA.y, colA.z),
                            new Vector4(0, colB.x, colB.y, colB.z),
                            new Vector4(0, colC.x, colC.y, colC.z)
                        );
                    break;
                default:
                    newMat = new Matrix4x4(
                            new Vector4(colA.x, 0, colA.y, colA.z),
                            new Vector4(0,1,0,0),
                            new Vector4(colB.x, 0, colB.y, colB.z),
                            new Vector4(colC.x, 0, colC.y, colC.z)
                        );
                    break;
            }
        }
        return newMat;
    }

    public static void IncMatrix()
    {
        _matCount++;
    }

    public static void DecMatrix()
    {
        _matCount--;
    }

    public static string GetMatrixString(out int fontSize)
    {
        int matCount = _matCount;
        string str = "";
        do
        {
            var rem = matCount % 10;
            str = Convert.ToChar(0x2080 + rem) + str;
            matCount /= 10;
        } while (matCount > 0);
        fontSize = 200 - (100 * str.Length / 4);
        return "M" + str;
    }
}