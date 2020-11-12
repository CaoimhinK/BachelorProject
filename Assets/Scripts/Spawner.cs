using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TypeEnum type;
    public GameObject spawnGoPrefab;
    public int spawnValue;
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
        tex.texGo = _textTex.texGo;
        tex.fontSize = _textTex.fontSize;
        
        switch (type)
        {
            case (TypeEnum.Number):
                if (_func)
                {
                    mo.value = (int) _func.value;
                    var text = mo.value.ToString();
                    tex.fontSize = Mathf.RoundToInt(Mathf.Min(2f / text.Length * 200f, 200f));
                    tex.text = text;
                }
                else
                {
                    mo.value = spawnValue;
                    var text = spawnValue.ToString();
                    tex.fontSize = Mathf.RoundToInt(Mathf.Min(2f / text.Length * 200f, 200f));
                    tex.text = text;
                }
                break;
            case (TypeEnum.Vector):
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
                if (_func && _func.type == TypeEnum.Normalise)
                {
                    var v = mo.vecValue;
                    strBody = "(" + (int)(v.x * 100)/100f + "," + (int)(v.y * 100)/100f + "," + (int)(v.z * 100)/100f + ")";
                }
                var size = (7f / strBody.Length) * 80f;
                tex.fontSize = Mathf.RoundToInt(size);
                tex.text = strHead + strBody;
                break;
        }
        return go;
    }
}
