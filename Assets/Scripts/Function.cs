using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Function : MonoBehaviour
{
    public Vector3 vec;
    public float value;
    public TypeEnum type;
    
    public Recepticle vec1;
    public Recepticle vec2;

    private Vector3 _last;
    private float _lastValue;

    public void Update()
    {
        _last = vec;
        _lastValue = value;
        var strHead = "Vec";
        var strBody = "";
        switch (type)
        {
            case TypeEnum.Add:
                vec = vec1.GetVector() + vec2.GetVector();
                strHead = StrHead("+");
                strBody = StrBody(vec);
                break;
            case TypeEnum.Sub:
                vec = vec1.GetVector() - vec2.GetVector();
                strHead = StrHead("-");
                strBody = StrBody(vec);
                break;
            case TypeEnum.Dot:
                value = Vector3.Dot(vec1.GetVector(), vec2.GetVector());
                strHead = StrHead(".");
                strBody = StrBody((int) value);
                break;
            case TypeEnum.Cross:
                vec = Vector3.Cross(vec1.GetVector(), vec2.GetVector());
                strHead = StrHead("x");
                strBody = StrBody(vec);
                break;
        }
        if (!vec.Equals(_last))
        {
            GetComponent<Spawner>().ChangeText(strHead + strBody);
        }
        if (!value.Equals(_lastValue))
        {
            GetComponent<Spawner>().ChangeText(strHead + strBody);
        }
    }

    private string StrHead(string str)
    {
        return "<b>" + str + "</b>\n";
    }

    private string StrBody(Vector3 v)
    { 
        return "(" + (int) v.x + "," + (int) v.y + "," + (int) v.z + ")";
    }

    private string StrBody(int n)
    {
        return n.ToString();
    }
}
