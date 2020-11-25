using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GrabController : MonoBehaviour
{
    public Transform grabPos;
    public Canvas ui;
    
    private const float GrabDuration = 0.2f;
    
    private Camera _cam;

    private Inventory _inventory;
    private GameObject _currentGo = null;
    private bool _animating = false;
    private float _startTime;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Quaternion _startRot;
    private Quaternion _endRot;
    
    void Start()
    {
        _cam = Camera.main;
        _inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (_animating)
        {
            Animate();
        }
        else if (Input.GetButtonDown("Grab"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo))
            {
                var hitGo = hitInfo.collider.gameObject;
                if (hitGo.TryGetComponent<Spawner>(out var spawner))
                {
                    if (_currentGo)
                    {
                        _inventory.PushGo(_currentGo);
                        _currentGo.SetActive(false);
                    }
                    StartCoroutine(nameof(Spawn), spawner);
                }
                else if (hitGo.TryGetComponent<Recepticle>(out var rec))
                {
                    if (_currentGo)
                    {
                        if (_currentGo.GetComponent<MathObj>().Type == rec.type)
                        {
                            StartCoroutine(nameof(GiveRec), new Holder(hitGo, rec));
                        }
                        else
                        {
                            StartCoroutine(nameof(Warn), hitGo);
                        }
                    }
                    else if (rec.HasObject())
                    {
                        StartCoroutine(nameof(TakeRec), rec);
                    }
                }
                else if (hitGo.TryGetComponent<MatrixApplier>(out var app))
                {
                    app.ApplyMatrix();
                }
            }
        }
        else if (Input.GetButtonDown("Throw"))
        {
            if (_currentGo) Destroy(_currentGo);
            _currentGo = null;
        }
        else if (Input.GetButtonDown("Restore"))
        {
            if (_inventory.HasItem())
            {
                if (_currentGo)
                {
                    _currentGo.SetActive(false);
                    _currentGo = _inventory.PushPop(_currentGo);
                    _currentGo.SetActive(true);
                }
                else
                {
                    _currentGo = _inventory.PopGo();
                    _currentGo.SetActive(true);
                }
            }
            else
            {
                if (_currentGo)
                {
                    _currentGo.SetActive(false);
                    _inventory.PushGo(_currentGo);
                    _currentGo = null;
                }
            }
        }
    }

    class Holder
    {
        public readonly GameObject HitGo;
        public readonly Recepticle Rec;

        public Holder(GameObject hitGo, Recepticle rec)
        {
            HitGo = hitGo;
            Rec = rec;
        }
    }

    void Animate()
    {
        var elapsedTime = Time.time - _startTime;
        _currentGo.transform.position = Vector3.Lerp(_startPos, _endPos, elapsedTime / GrabDuration);
    }

    IEnumerator TakeRec(Recepticle rec)
    {
        _currentGo = rec.TakeObject();
        StartAnim(_currentGo.transform.position, grabPos.transform.position);
        yield return new WaitForSeconds(GrabDuration);
        _currentGo.transform.SetParent(grabPos);
        _currentGo.transform.localPosition = Vector3.zero;
        StopAnim();
    }

    IEnumerator GiveRec(Holder hold)
    {
        var hitPoint = hold.HitGo.transform.position;
        StartAnim(_currentGo.transform.position, hitPoint + Vector3.up * 0.05f);
        yield return new WaitForSeconds(GrabDuration);
        hold.Rec.GiveObject(_currentGo);
        _currentGo.transform.SetParent(hold.HitGo.transform);
        _currentGo = null;
        StopAnim();
    }

    IEnumerator Spawn(Spawner spawner)
    {
        _currentGo = spawner.SpawnObject();
        StartAnim(_currentGo.transform.localPosition, grabPos.transform.position);
        _currentGo.transform.SetParent(grabPos);
        yield return new WaitForSeconds(GrabDuration);
        _currentGo.transform.localPosition = Vector3.zero;
        StopAnim();
    }

    IEnumerator Warn(GameObject hitGo)
    {
        var mat = hitGo.GetComponent<MeshRenderer>().material;
        var col = mat.color;
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = col;
    }

    void StartAnim(Vector3 startPos, Vector3 endPos)
    {
        _animating = true;
        _startTime = Time.time;
        _startPos = startPos;
        _endPos = endPos;
    }

    void StopAnim()
    {
        _animating = false;
    }
}
