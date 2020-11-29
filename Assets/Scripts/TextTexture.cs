using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTexture : MonoBehaviour
{
    public string text;
    public int fontSize;
    
    private MeshRenderer _ren;
    
    void Start()
    {
        _ren = GetComponent<MeshRenderer>();
        var mat = new Material(_ren.sharedMaterial) {mainTexture = TextRenderer.Instance.RenderText(text, fontSize)};
        _ren.material = mat;
    }

    public void Render()
    {
        if (!_ren) _ren = GetComponent<MeshRenderer>();
        var mat = new Material(_ren.sharedMaterial) {mainTexture = TextRenderer.Instance.RenderText(text, fontSize)};
        _ren.material = mat;
    }
}
