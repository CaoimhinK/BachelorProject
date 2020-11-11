using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnGoPrefab;
    public int spawnValue;

    private TextTexture _textTex;

    private void Start()
    {
        _textTex = GetComponent<TextTexture>();
        spawnGoPrefab.GetComponent<TextTexture>().text = _textTex.text;
    }

    public GameObject SpawnObject()
    {
        GameObject go = Instantiate(spawnGoPrefab, transform.position, transform.rotation);
        var tex = go.GetComponent<TextTexture>();
        tex.tex = _textTex.tex;
        tex.text = _textTex.text;
        go.GetComponent<MathObj>().value = spawnValue;
        return go;
    }
}
