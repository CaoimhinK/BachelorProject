using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatCol : MonoBehaviour
{
    public Recepticle[] recs;
    public Vector3 colValue;
    
    public bool HasContentChanged()
    {
        if (recs.Any(num => num.HasContentChanged()))
        {
            colValue = new Vector3(recs[0].GetValue(), recs[1].GetValue(), recs[2].GetValue());
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
