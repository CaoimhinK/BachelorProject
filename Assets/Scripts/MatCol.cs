using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatCol : MonoBehaviour
{
    public Recepticle[] recs;
    public Vector4 colValue;

    private void Start()
    {
        colValue = new Vector4(recs[0].GetValue(), recs[1].GetValue(), recs[2].GetValue(), (recs.Length == 4) ? recs[3].GetValue() : 0);
    }

    public bool HasContentChanged()
    {
        if (recs.Any(num => num.HasContentChanged()))
        {
            colValue = new Vector4(recs[0].GetValue(), recs[1].GetValue(), recs[2].GetValue(), (recs.Length == 4) ? recs[3].GetValue() : 0);
            return true;
        }

        return false;
    }

    public void ListenToContent()
    {
        foreach (var rec in recs)
        {
            rec.ListenToContent();
        }
    }
}
