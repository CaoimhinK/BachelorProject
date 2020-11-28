using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Inventory : MonoBehaviour
{
    public RawImage[] textures;
    public RawImage[] bgs;

    private List<GameObject> _gos;

    private void Start()
    {
        _gos = new List<GameObject>();
    }

    public bool PushGo(GameObject go)
    {
        if (_gos.Count > 2)
        {
            return false;
        }
        _gos.Add(go);
        RedrawImages();
        return true;
    }

    public GameObject PopGo()
    {
        if (_gos.Count > 0)
        {
            var popped = _gos.Last<GameObject>();
            _gos.RemoveAt(_gos.Count - 1);
            RedrawImages();
            return popped;
        }
        return null;
    }

    public GameObject PushPop(GameObject go)
    {
        var popped = _gos.Last<GameObject>();
        _gos.RemoveAt(_gos.Count - 1);
        _gos.Add(go);
        
        RedrawImages();
        return popped;
    }

    public bool HasItem()
    {
        return _gos.Count > 0;
    }

    private void RedrawImages()
    {
        var ator = _gos.GetEnumerator();
        var index = 0;

        while (index < 3 - _gos.Count)
        {
            textures[index].texture = null;
            bgs[index].color = Color.white;
            index++;
        }
        while (ator.MoveNext() && ator.Current)
        {
            var tex = ator.Current.GetComponent<TextTexture>();
            textures[index].texture = TextRenderer.Instance.RenderText(tex.text, tex.fontSize);
            bgs[index].color = ColorPalette.Colors[ator.Current.GetComponent<MathObj>().Type];
            index++;
        }
        ator.Dispose();
    }

    public GameObject TakeIndex(int index, GameObject go)
    {
        // TO-FUCKING-DO: do things
        return null;
    }
}
