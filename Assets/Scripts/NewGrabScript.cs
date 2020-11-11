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
                    if (currentGo) Destroy(currentGo);
                    StartCoroutine(nameof(Spawn), spawner);
                }
                else
                {
                    if (hitGo.TryGetComponent<Recepticle>(out var rec))
                    {
                        if (currentGo)
                        {
                            if (currentGo.GetComponent<MathObj>().type == rec.type)
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
                }
            }
        }
        else if (Input.GetButtonDown("Throw"))
        {
            if (currentGo) Destroy(currentGo);
            currentGo = null;
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
        currentGo = rec.TakeObject();
        StartAnim(currentGo.transform.position, grabPos.transform.position);
        yield return new WaitForSeconds(GrabDuration);
        currentGo.transform.SetParent(grabPos);
        currentGo.transform.localPosition = Vector3.zero;
        StopAnim();
    }

    IEnumerator GiveRec(Holder hold)
    {
        var hitPoint = hold.HitGo.transform.position;
        StartAnim(currentGo.transform.position, hitPoint + Vector3.up * 0.05f);
        currentGo.transform.SetParent(null);
        yield return new WaitForSeconds(GrabDuration);
        hold.Rec.GiveObject(currentGo);
        currentGo.transform.SetParent(hold.HitGo.transform);
        currentGo = null;
        StopAnim();
    }

    IEnumerator Spawn(Spawner spawner)
    {
        currentGo = spawner.SpawnObject();
        StartAnim(currentGo.transform.localPosition, grabPos.transform.position);
        yield return new WaitForSeconds(GrabDuration);
        currentGo.transform.SetParent(grabPos);
        currentGo.transform.localPosition = Vector3.zero;
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
