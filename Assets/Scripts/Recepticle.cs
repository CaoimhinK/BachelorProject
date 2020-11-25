using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recepticle : MonoBehaviour
{
    public ObjType type;
    public float defaultValue;

    private bool _contentChanged;
    
    private GameObject _heldGo;
    private MeshRenderer _ren;
    private Material _defaultMat;
    private Material _fullMat;

    public void Start()
    {
        _ren = GetComponent<MeshRenderer>();
        _defaultMat = new Material(_ren.material);
        _defaultMat.color = ColorPalette.Colors[type];
        _ren.material = _defaultMat;
        _fullMat = Instantiate(_defaultMat);
        _fullMat.color = new Color(0.6f,1,0.6f);
        var tex = GetComponent<TextTexture>();
        tex.text = defaultValue.ToString();
        tex.fontSize= 200;
        tex.Render();
    }

    public float GetValue()
    {
        var val = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.value : defaultValue;
        return val;
    }

    public Vector3 GetVector()
    {
        var val = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.vecValue : Vector3.zero;
        return val;
    }

    public Matrix4x4 GetMatrix()
    {
        var mat = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.matValue : Matrix4x4.identity;
        return mat;
    }

    public bool HasObject()
    {
        return _heldGo;
    }

    public bool HasContentChanged()
    {
        return _contentChanged;
    }

    public void ListenToContent()
    {
        _contentChanged = false;
    }

    public void GiveObject(GameObject go)
    {
        if (_heldGo) Destroy(_heldGo);
        _ren.material = _fullMat;
        _heldGo = go;
        _contentChanged = true;
    }

    public GameObject TakeObject()
    {
        GameObject giveGo = _heldGo;
        _ren.material = _defaultMat;
        _heldGo = null;
        _contentChanged = true;
        return giveGo;
    }
}
