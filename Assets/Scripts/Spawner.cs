using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjType type;
    public GameObject spawnGoPrefab;
    public float spawnValue;
    public Vector3 spawnVecValue;
    public Matrix4x4 spawnMatValue;

    private TextTexture _textTex;
    private VectorContainer _cont;
    private MatrixContainer _matCont;
    private Function _func;

    private void Start()
    {
        _cont = GetComponent<VectorContainer>();
        _matCont = GetComponent<MatrixContainer>();
        _func = GetComponent<Function>();
        GetComponent<MeshRenderer>().material.color = ColorPalette.Colors[type];
        _textTex = GetComponent<TextTexture>();
        _textTex.Render();
    }

    public void LoadTex()
    {
        if (!_textTex)
        {
            _textTex = GetComponent<TextTexture>();
            _textTex.Render();
        }
    }

    public void ChangeText(string strHead, string strBody, int fontSize)
    {
        _textTex.fontSize = fontSize;
        _textTex.text = strHead + strBody;
        _textTex.Render();
    }

    public void ChangeText(string text, int fontSize)
    {
        _textTex.fontSize = fontSize;
        _textTex.text = text;
        _textTex.Render();
    }

    public GameObject SpawnObject()
    {
        Transform trans = transform;
        GameObject go = Instantiate(spawnGoPrefab, trans.position, trans.rotation);
        
        var mat = go.GetComponent<MeshRenderer>().material;
        
        var mo = go.GetComponent<MathObj>();
        mo.Type = type;
        mat.color = ColorPalette.Colors[type];
        
        var tex = go.GetComponent<TextTexture>();
        
        switch (type)
        {
            case (ObjType.Number):
                string text;
                if (_func)
                {
                    mo.value = _func.value;
                    text = ((int)(_func.value * 100)/100f).ToString();
                }
                else
                {
                    mo.value = spawnValue;
                    text = ((int)(spawnValue * 100)/100f).ToString();
                }
                tex.fontSize = Mathf.RoundToInt(Mathf.Min(2f / text.Length * 200f, 200f));
                tex.text = text;
                break;
            case (ObjType.Vector):
                if (_cont)
                {
                    mo.vecValue = _cont.vec;
                }
                else if (_func)
                {
                    mo.vecValue = _func.vec;
                }
                else
                {
                    mo.vecValue = spawnVecValue;
                }

                var strHead = "Vec\n";
                var strBody = "(" + (int) mo.vecValue.x + "," + (int) mo.vecValue.y + "," + (int) mo.vecValue.z + ")";
                var size = (7f / strBody.Length) * 80f;
                tex.fontSize = Mathf.RoundToInt(size);
                tex.text = strHead + strBody;
                break;
            case (ObjType.Matrix):
                if (_matCont)
                {
                    mo.matValue = _matCont.Mat;
                }
                else
                {
                    mo.matValue = spawnMatValue;
                }
                tex.text = MatrixContainer.GetMatrixString(out var fontSize);
                tex.fontSize = fontSize;
                break;
        }
        return go;
    }
}
