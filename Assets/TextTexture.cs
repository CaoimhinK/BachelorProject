using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTexture : MonoBehaviour
{
    public TextRenderer tex;
    public string text = "test";
    public int fontSize = 200;
    
    private MeshRenderer _ren;
    void Start()
    {
        _ren = GetComponent<MeshRenderer>();
        _ren.material.mainTexture = tex.RenderText(text, fontSize);
    }
}
