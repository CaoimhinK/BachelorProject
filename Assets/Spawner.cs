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

    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = ColorPalette.Colors[type];
        _textTex = GetComponent<TextTexture>();
        _cont = GetComponent<VectorContainer>();
        spawnGoPrefab.GetComponent<TextTexture>().text = _textTex.text;
        _textTex.Render();
    }

    public void ChangeText()
    {
        _textTex.text = "Vec\n(" + (int) _cont.vec.x + "," + (int) _cont.vec.y + "," + (int) _cont.vec.z + ")";
        _textTex.Render();
    }

    public GameObject SpawnObject()
    {
        GameObject go = Instantiate(spawnGoPrefab, transform.position, transform.rotation);
        var tex = go.GetComponent<TextTexture>();
        var mat = go.GetComponent<MeshRenderer>().material;
        var mo = go.GetComponent<MathObj>();
        tex.texGo = _textTex.texGo;
        tex.text = _textTex.text;
        tex.fontSize = _textTex.fontSize;
        switch (type)
        {
            case (TypeEnum.Number):
                mo.value = spawnValue;
                break;
            case (TypeEnum.Vector):
                mo.vecValue = (_cont) ? _cont.vec : spawnVecValue;
                break;
        }
        mat.color = ColorPalette.Colors[type];
        go.GetComponent<MathObj>().type = type;
        return go;
    }
}
