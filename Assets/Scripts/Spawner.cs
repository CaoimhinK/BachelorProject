using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ChangeText(string str)
    {
        _textTex.text = str;
        _textTex.Render();
    }

    public GameObject SpawnObject()
    {
        GameObject go = Instantiate(spawnGoPrefab, transform.position, transform.rotation);
        var tex = go.GetComponent<TextTexture>();
        var mat = go.GetComponent<MeshRenderer>().material;
        var mo = go.GetComponent<MathObj>();
        tex.texGo = _textTex.texGo;
        tex.fontSize = _textTex.fontSize;
        switch (type)
        {
            case (TypeEnum.Number):
                mo.value = spawnValue;
                tex.text = spawnValue.ToString();
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
                var size = (7f / strBody.Length) * 80f;
                tex.fontSize = Mathf.RoundToInt(size);
                tex.text = strHead + strBody;
                break;
        }
        mat.color = ColorPalette.Colors[type];
        go.GetComponent<MathObj>().type = type;
        return go;
    }
}
