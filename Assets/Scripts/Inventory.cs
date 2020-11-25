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
    public RawImage[] images;

    private Queue<GameObject> _gos;

    private void Start()
    {
        _gos = new Queue<GameObject>();
    }

    public void PushGo(GameObject go)
    {
        if (_gos.Count > 2)
        {
            var shmo = _gos.Dequeue();
            Destroy(shmo);
        }
        _gos.Enqueue(go);
        RedrawImages();
    }

    public GameObject PopGo()
    {
        if (_gos.Count > 0)
        {
            var popped = _gos.Dequeue();
            RedrawImages();
            return popped;
        }
        return null;
    }

    public GameObject PushPop(GameObject go)
    {
        var popped = _gos.Dequeue();
        _gos.Enqueue(go);
        
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
            images[index].texture = null;
            index++;
        }
        while (ator.MoveNext() && ator.Current)
        {
            var tex = ator.Current.GetComponent<TextTexture>();
            images[index].texture = TextRenderer.Instance.RenderText(tex.text, tex.fontSize);
            index++;
        }
        ator.Dispose();
    }
}
