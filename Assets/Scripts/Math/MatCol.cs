using UnityEngine;

public class MatCol : MonoBehaviour
{
    public Container[] recs;

    public Vector4 ColValue => new Vector4(recs[0].GetValue(), recs[1].GetValue(), recs[2].GetValue(), (recs.Length == 4) ? recs[3].GetValue() : 0);
}
