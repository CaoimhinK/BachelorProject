using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class NewGrabScript : MonoBehaviour
{
    public Transform grabPos;
    
    private const float GrabDuration = 0.2f;
    
    private Camera _cam;
    
    public GameObject currentGo = null;

    private bool _animating = false;
    private float _startTime;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Quaternion _startRot;
    private Quaternion _endRot;
    
    void Start()
    {
        _cam = Camera.main;
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
                    StartCoroutine(nameof(Spawn), spawner);
                }
                else
                {
                    if (hitGo.TryGetComponent<Recepticle>(out var rec))
                    {
                        if (currentGo)
                        {
                            StartCoroutine(nameof(GiveRec), new Holder(hitGo, rec));
                        }
                        else if (rec.HasObject())
                        {
                            StartCoroutine(nameof(TakeRec), rec);
                        }
                    }
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
            this.HitGo = hitGo;
            this.Rec = rec;
        }
    }

    void Animate()
    {
        var elapsedTime = Time.time - _startTime;
        currentGo.transform.position = Vector3.Lerp(_startPos, _endPos, elapsedTime / GrabDuration);
    }

    IEnumerator TakeRec(Recepticle rec)
    {
        _animating = true;
        _startTime = Time.time;
        if (currentGo)
        {
            Destroy(currentGo);
            currentGo = null;
        }
        currentGo = rec.TakeObject();
        _startPos = currentGo.transform.position;
        _endPos = grabPos.transform.position;
        yield return new WaitForSeconds(GrabDuration);
        currentGo.transform.SetParent(grabPos);
        currentGo.transform.localPosition = Vector3.zero;
        _animating = false;
    }

    IEnumerator GiveRec(Holder hold)
    {
        _animating = true;
        var hitPoint = hold.HitGo.transform.position;
        _startTime = Time.time;
        currentGo.transform.SetParent(null);
        _startPos = currentGo.transform.position;
        _endPos = hitPoint + Vector3.up * 0.05f;
        yield return new WaitForSeconds(GrabDuration);
        hold.Rec.GiveObject(currentGo);
        currentGo = null;
        _animating = false;
    }

    IEnumerator Spawn(Spawner spawner)
    {
        _animating = true;
        _startTime = Time.time;
        if (currentGo)
        {
            Destroy(currentGo);
            currentGo = null;
        }
        currentGo = spawner.SpawnObject();
        currentGo.GetComponent<BoxCollider>().enabled = false;
        _startPos = currentGo.transform.localPosition;
        _endPos = grabPos.transform.position;
        yield return new WaitForSeconds(GrabDuration);
        currentGo.transform.SetParent(grabPos);
        currentGo.transform.localPosition = Vector3.zero;
        _animating = false;
    }
}
