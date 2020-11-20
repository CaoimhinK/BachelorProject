using System.Linq;
using UnityEngine;

public class VectorContainer : MonoBehaviour
{
    public Vector3 vec;
    public Recepticle[] recs;

    public void Update()
    {
        if (recs.Any(num => num.HasContentChanged()))
        {
            vec = new Vector3(recs[0].GetValue(),recs[1].GetValue(), recs[2].GetValue());
            var strHead = "Vec\n(";
            var strBody = (int) vec.x + "," + (int) vec.y + "," + (int) vec.z + ")";
            var size = Mathf.RoundToInt(Mathf.Min((7f / strBody.Length) * 80f, 80f));
            GetComponent<Spawner>().ChangeText(strHead, strBody, size);
            foreach (var rec in recs)
            {
                rec.ListenToContent();
            }
        }
    }
}
