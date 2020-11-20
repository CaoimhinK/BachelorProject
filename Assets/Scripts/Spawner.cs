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

    private TextTexture _textTex;
    private VectorContainer _cont;
    private Function _func;

    private void Start()
    {
        _cont = GetComponent<VectorContainer>();
        _func = GetComponent<Function>();
        GetComponent<MeshRenderer>().material.color = ColorPalette.Colors[type];
        _textTex = GetComponent<TextTexture>();
        spawnGoPrefab.GetComponent<TextTexture>().text = _textTex.text;
        _textTex.Render();
    }

    public void LoadTex()
    {
        if (!_textTex)
        {
            _textTex = GetComponent<TextTexture>();
            spawnGoPrefab.GetComponent<TextTexture>().text = _textTex.text;
            _textTex.Render();
        }
    }

    public void ChangeText(string strHead, string strBody, int fontSize)
    {
        _textTex.fontSize = fontSize;
        _textTex.text = strHead + strBody;
        _textTex.Render();
        
    }

    public GameObject SpawnObject()
    {
        GameObject go = Instantiate(spawnGoPrefab, transform.position, transform.rotation);
        
        var mat = go.GetComponent<MeshRenderer>().material;
        
        var mo = go.GetComponent<MathObj>();
        mo.type = type;
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
        }
        return go;
    }
}
