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
    private readonly int _capacity = 3;

    private List<GameObject> _gos;

    private void Start()
    {
        _gos = Enumerable.Repeat<GameObject>(null, _capacity).ToList();
    }

    public GameObject Pop()
    {
        for (var i = 0; i < _capacity; i++)
        {
            if (_gos[i])
            {
                var temp = _gos[i];
                _gos[i] = null;
                RedrawImages();
                return temp;
            }
        }
        return null;
    }

    private void RedrawImages()
    {
        for (var index = 0; index < _capacity; index++)
        {
            var go = _gos[index];
            if (go)
            {
                var tex = _gos.ElementAt(index).GetComponent<TextTexture>();
                textures[index].texture = TextRenderer.Instance.RenderText(tex.text, tex.fontSize);
                bgs[index].color = ColorPalette.Colors[go.GetComponent<MathObj>().Type];
            }
            else
            {
                textures[index].texture = null;
                bgs[index].color = Color.white;
            }
        }
    }

    public bool Push(GameObject go)
    {
        for (var i = 0; i < _capacity; i++)
        {
            if (!_gos[i]) {
                _gos[i] = go;
                RedrawImages();
                return true;
            }
        }
        return false;
    }

    public bool IndexFull(int index)
    {
        return !!_gos[index];
    }

    public GameObject SwapIndex(int index, GameObject go)
    {
        var temp = _gos[index];
        _gos[index] = go;
        RedrawImages();
        return temp;
    }

    public GameObject GetIndex(int index)
    {
        return SwapIndex(index, null);
    }

    public void SetIndex(int index, GameObject go)
    {
        SwapIndex(index, go);
    }
}
