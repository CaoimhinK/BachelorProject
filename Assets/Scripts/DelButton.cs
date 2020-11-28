using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelButton : MonoBehaviour
{
    public Recepticle[] recs;
    public void PushButton() {
        foreach (var rec in recs) {
            rec.Clear();
        }
    }
}
