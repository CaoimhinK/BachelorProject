using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private readonly float GrabDuration = 0.2f;
    private readonly float WarnDuration = 0.1f;
    public Transform grabPos;

    public void Del(GameObject currentGo, GameObject hitGo)
    {
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            hitGo.transform,
            (hold) => {
                Destroy(currentGo);
            }
        ));
    }
    
    public GameObject TakeRec(Container rec)
    {
        var currentGo = rec.TakeObject();
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            grabPos.transform,
            (hold) =>
            {
                currentGo.transform.position = grabPos.position;
            }
        ));
        return currentGo;
    }
    
    public void TakeRecToInventory(Container rec, Inventory inv, int index)
    {
        var currentGo = rec.TakeObject();
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            grabPos.transform,
            (hold) =>
            {
                currentGo.transform.position = grabPos.position;
                inv.SetIndex(index, currentGo);
                currentGo.SetActive(false);
            }
        ));
    }

    private bool _isGiving;
    public bool GiveRec(GameObject current, Transform hitGo, Container rec)
    {
        if (_isGiving)
        {
            current.SetActive(false);
            Warn(hitGo.gameObject);
            return false;
        }
        else
        {
            _isGiving = true;
            StartCoroutine(nameof(AnimFromTo), new Holder(
                    current.transform,
                    hitGo,
                    (hold) => {
                        rec.GiveObject(current);
                        current.transform.localPosition = Vector3.up * 0.05f;
                        _isGiving = false;
                    }
                ));
            return true;
        }
    }

    public GameObject Spawn(Spawner spawner)
    {
        var currentGo = spawner.SpawnObject();
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            grabPos.transform,
            (hold) =>
            {
                currentGo.transform.position = grabPos.position;
            }
        ));
        return currentGo;
    }
    
    public void SpawnInInventory(Spawner spawner, Inventory inv, int index)
    {
        var currentGo = spawner.SpawnObject();
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            grabPos.transform,
            (hold) =>
            {
                currentGo.transform.position = grabPos.position;
                inv.SetIndex(index, currentGo);
                currentGo.SetActive(false);
            }
        ));
    }

    public void Warn(GameObject hitGo)
    {
        StartCoroutine(nameof(WarnCoroutine), hitGo);
    }

    private class FadeMaterial
    {
        private readonly Material _mat;
        private readonly float _end;
        public FadeMaterial(Material mat, float end)
        {
            _mat = mat;
            _end = end;
        }

        public void Out(out Material mat, out float end)
        {
            mat = _mat;
            end = _end;
        }
    }
    
    private Dictionary<Material, Coroutine> dic = new Dictionary<Material, Coroutine>();

    public void Fade(Material mat, float end)
    {
        var fadeMat = new FadeMaterial(mat, end);
        if (dic.ContainsKey(mat))
        {
            StopCoroutine(dic[mat]);
            dic[mat] = StartCoroutine(nameof(AnimFromToOpacity), fadeMat);
        }
        else
        {
            dic.Add(mat, StartCoroutine(nameof(AnimFromToOpacity), fadeMat));
        }
    }

    private IEnumerator WarnCoroutine(GameObject hitGo)
    {
        var mat = hitGo.GetComponent<MeshRenderer>().material;
        var col = mat.color;
        if (col == Color.red) yield break;
        mat.color = Color.red;
        yield return new WaitForSeconds(WarnDuration);
        mat.color = col;
    }

    private IEnumerator AnimFromTo(Holder hold)
    {
        var startTime = Time.time;
        var elapsedTime = 0f;
        var startPos = hold.go.position;

        hold.go.SetParent(hold.target);

        while (elapsedTime < GrabDuration)
        {
            hold.go.position = Vector3.Lerp(startPos, hold.target.position, elapsedTime / GrabDuration);
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        hold.after(hold);
    }

    IEnumerator AnimFromToOpacity(FadeMaterial fadeMat)
    {
        var startTime = Time.time;
        var elapsedTime = 0f;

        fadeMat.Out(out var mat, out var end);

        var startCol = mat.color;
        var endCol = mat.color;
        endCol.a = end;

        while (elapsedTime < 0.2f)
        {
            elapsedTime = Time.time - startTime;
            mat.color = Color.Lerp(startCol, endCol, elapsedTime / 0.2f);
            yield return null;
        }

        mat.color = endCol;
        dic.Remove(mat);
    }

    private class Holder
    {
        public readonly Transform go;
        public readonly Transform target;
        public readonly Action<Holder> after;

        public Holder(Transform go, Transform target, Action<Holder> after)
        {
            this.go = go;
            this.target = target;
            this.after = after;
        }
        public Holder(Transform go, Transform target)
        {
            this.go = go;
            this.target = target;
            after = (hold) => {};
        }
    }
}
