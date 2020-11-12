using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public enum TypeEnum 
{
    Number,
    Vector,
    Add,
    Sub,
    Dot,
    Cross,
    Invert,
}

public class FuncIO
{
    private readonly Vector3 _vec;
    private readonly float _value;
    private readonly string _strHead;
    private readonly string _strBody;
    private readonly int _fontSize;
    
    public FuncIO(Vector3 vec, float value, string strHead, string strBody, int fontSize)
    {
        _vec = vec;
        _value = value;
        _strHead = strHead;
        _strBody = strBody;
        _fontSize = fontSize;
    }

    public void Out(out Vector3 vec, out float value, out string strHead, out string strBody, out int fontSize)
    {
        vec = _vec;
        value = _value;
        strHead = _strHead;
        strBody = _strBody;
        fontSize = _fontSize;
    }
}

static class TypeFunctions
{
   public static FuncIO EvaluateFunction(TypeEnum type, Recepticle vec1, Recepticle vec2)
   {
       string strHead = "Math";
       string strBody = "";
       Vector3 vec = Vector3.negativeInfinity;
       float value = Mathf.NegativeInfinity;
       bool fsBool = true;
       int fontSize;
       switch (type)
       {
           case TypeEnum.Number:
               strHead = "";
               strBody = StrBody((int) value);
               fsBool = false;
               break;
           case TypeEnum.Vector:
               strHead = StrHead("Vec");
               strBody = StrBody(vec);
               fsBool = true;
               break;
           case TypeEnum.Add:
               vec = vec1.GetVector() + vec2.GetVector();
               strHead = StrHead("+");
               strBody = StrBody(vec);
               fsBool = true;
               break;
           case TypeEnum.Sub:
               vec = vec1.GetVector() - vec2.GetVector();
               strHead = StrHead("-");
               strBody = StrBody(vec);
               fsBool = true;
               break;
           case TypeEnum.Dot:
               value = Vector3.Dot(vec1.GetVector(), vec2.GetVector());
               strHead = StrHead(".");
               strBody = StrBody((int) value);
               fsBool = true;
               break;
           case TypeEnum.Cross:
               vec = Vector3.Cross(vec1.GetVector(), vec2.GetVector());
               strHead = StrHead("x");
               strBody = StrBody(vec);
               fsBool = true;
               break;
           case TypeEnum.Invert:
               vec = -vec1.GetVector();
               strHead = StrHead("Inv");
               strBody = StrBody(vec);
               fsBool = true;
               break;
       }

       if (fsBool)
       {
           fontSize = Mathf.RoundToInt(Mathf.Min((7f / strBody.Length) * 80f,80f));
       }
       else
       {
           fontSize = Mathf.RoundToInt(Mathf.Min((2f / strBody.Length) * 200f,200f));
       }
       
       return new FuncIO(vec, value, strHead, strBody, fontSize);
   }
   
   private static string StrHead(string str)
   {
       return "<b>" + str + "</b>\n";
   }
   
   private static string StrBody(Vector3 v)
   { 
       return "(" + (int) v.x + "," + (int) v.y + "," + (int) v.z + ")";
   }
   
    private static string StrBody(int n)
    {
        return n.ToString();
    }
}