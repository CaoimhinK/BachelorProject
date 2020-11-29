using System;
using System.Collections;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private readonly float GrabDuration = 0.2f;
    public Transform grabPos;

    public void Del(GameObject currentGo, GameObject hitGo)
    {
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            hitGo.transform,
            (hold) => {
                UnityEngine.GameObject.Destroy(currentGo);
            }
        ));
    }

    public GameObject TakeRec(Recepticle rec)
    {
        var currentGo = rec.TakeObject();
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            grabPos.transform
        ));
        return currentGo;
    }

    public void GiveRec(GameObject current, Transform hitGo, Recepticle rec)
    {
        StartCoroutine(nameof(AnimFromTo), new Holder(
                current.transform,
                hitGo,
                (hold) => {
                    rec.GiveObject(current);
                    current.transform.localPosition += Vector3.up * 0.05f;
                }
            ));
    }

    public GameObject Spawn(Spawner spawner)
    {
        var currentGo = spawner.SpawnObject();
        StartCoroutine(nameof(AnimFromTo), new Holder(
            currentGo.transform,
            grabPos.transform
        ));
        return currentGo;
    }

    public void Warn(GameObject hitGo)
    {
        StartCoroutine(nameof(WarnCoroutine), hitGo);
    }

    private IEnumerator WarnCoroutine(GameObject hitGo)
    {
        var mat = hitGo.GetComponent<MeshRenderer>().material;
        var col = mat.color;
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
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
            this.after = (hold) => {};
        }
    }
}
