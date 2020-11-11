using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class TextRenderer : MonoBehaviour
{
    public Camera cam;
    public RenderTexture renTex;
    public Text renderText;
    
    public Texture2D RenderText(string text, int fontSize)
    {
        renderText.text = text;
        renderText.fontSize = fontSize;

        var dest = new Texture2D(renTex.width, renTex.height, renTex.graphicsFormat, TextureCreationFlags.None);
        cam.Render();
        Graphics.CopyTexture(renTex, dest);
        return dest;
    }
}
