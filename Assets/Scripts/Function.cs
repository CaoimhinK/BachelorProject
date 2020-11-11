using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function : MonoBehaviour
{
    public Vector3 vec;
    
    public Recepticle vec1;
    public Recepticle vec2;

    private Vector3 _last;

    public void Update()
    {
        _last = vec;
        vec = vec1.GetVector() + vec2.GetVector();
        if (!vec.Equals(_last))
        {
            var str = "<b>+</b>\n(" + (int) vec.x + "," + (int) vec.y + "," + (int) vec.z + ")";
            GetComponent<Spawner>().ChangeText(str);
        }
    }
}
