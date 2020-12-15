using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatCol : MonoBehaviour
{
    public Recepticle[] recs;

    public Vector4 ColValue => new Vector4(recs[0].GetValue(), recs[1].GetValue(), recs[2].GetValue(), (recs.Length == 4) ? recs[3].GetValue() : 0);
}
