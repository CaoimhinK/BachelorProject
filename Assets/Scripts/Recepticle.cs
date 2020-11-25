using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recepticle : MonoBehaviour
{
    public ObjType type;
    public float defaultValue;
    public Vector3 defaultVector = Vector3.zero;
    public Matrix4x4 defaultMatrix = Matrix4x4.identity;

    private bool _contentChanged;
    
    private GameObject _heldGo;
    private MeshRenderer _ren;
    private Material _defaultMat;
    private Material _fullMat;
    private TextTexture _tex;

    public void Start()
    {
        _ren = GetComponent<MeshRenderer>();
        _defaultMat = new Material(_ren.material);
        _defaultMat.color = ColorPalette.Colors[type];
        _ren.material = _defaultMat;
        _fullMat = Instantiate(_defaultMat);
        _fullMat.color = new Color(0.6f,1,0.6f);
        _tex = GetComponent<TextTexture>();
        switch (type)
        {
            case ObjType.Matrix:
                _tex.text = "I";
                _tex.fontSize = 200;
                break;
            case ObjType.Number:
                _tex.text = defaultValue.ToString();
                _tex.fontSize = 200;
                break;
            case ObjType.Vector:
                _tex.text = $"Vec\n({defaultVector.x},{defaultVector.y},{defaultVector.z}";
                _tex.fontSize = 80;
                break;
        }
        _tex.Render();
    }

    public float GetValue()
    {
        var val = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.value : defaultValue;
        return val;
    }

    public Vector3 GetVector()
    {
        var val = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.vecValue : defaultVector;
        return val;
    }

    public Matrix4x4 GetMatrix()
    {
        var mat = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.matValue : defaultMatrix;
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
        _tex.Render();
        _heldGo = null;
        _contentChanged = true;
        return giveGo;
    }
}
