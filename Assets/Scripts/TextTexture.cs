using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTexture : MonoBehaviour
{
    public GameObject texGo;
    [TextArea]
    public string text = "test";
    public int fontSize = 200;

    private static TextRenderer _tex;
    private MeshRenderer _ren;
    
    void Start()
    {
        if (!_tex) _tex = Instantiate(texGo).GetComponent<TextRenderer>();
        _ren = GetComponent<MeshRenderer>();
        var mat = new Material(_ren.sharedMaterial) {mainTexture = _tex.RenderText(text, fontSize)};
        _ren.material = mat;
    }

    public void Render()
    {
        if (!_tex) _tex = Instantiate(texGo).GetComponent<TextRenderer>();
        if (!_ren) _ren = GetComponent<MeshRenderer>();
        var mat = new Material(_ren.sharedMaterial) {mainTexture = _tex.RenderText(text, fontSize)};
        _ren.material = mat;
    }
}
