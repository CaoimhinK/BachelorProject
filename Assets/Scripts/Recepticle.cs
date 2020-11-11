using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recepticle : MonoBehaviour
{
    public TypeEnum type;
    
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
    }

    public int GetValue()
    {
        var val = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.value : 0;
        return val;
    }

    public Vector3 GetVector()
    {
        var val = ((_heldGo) && _heldGo.TryGetComponent<MathObj>(out var mo)) ? mo.vecValue : Vector3.zero;
        return val;
    }

    public bool HasObject()
    {
        return _heldGo;
    }

    public void GiveObject(GameObject go)
    {
        if (_heldGo) Destroy(_heldGo);
        _ren.material = _fullMat;
        _heldGo = go;
    }

    public GameObject TakeObject()
    {
        GameObject giveGo = _heldGo;
        _ren.material = _defaultMat;
        _heldGo = null;
        return giveGo;
    }
}
