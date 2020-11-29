using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public enum FuncType 
{
    Sin,
    Cos,
    Add,
    Sub,
    Dot,
    Cross,
    Invert,
    Combine,
    Negative,
}

public enum ObjType
{
    Number,
    Vector,
    Matrix,
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
   public static FuncIO EvaluateFunction(FuncType type, Recepticle vec1, Recepticle vec2)
   {
       string strHead = "Math";
       string strBody = "";
       Vector3 vec = Vector3.negativeInfinity;
       float value = Mathf.NegativeInfinity;
       bool numTypeb = true;
       
       switch (type)
       {
           case FuncType.Add:
               vec = vec1.GetVector() + vec2.GetVector();
               strHead = StrHead("+");
               strBody = StrBody(vec);
               break;
           case FuncType.Sub:
               vec = vec1.GetVector() - vec2.GetVector();
               strHead = StrHead("-");
               strBody = StrBody(vec);
               break;
           case FuncType.Dot:
               value = Vector3.Dot(vec1.GetVector(), vec2.GetVector());
               strHead = StrHead(".");
               strBody = StrBody(value);
               numTypeb = false;
               break;
           case FuncType.Cross:
               vec = Vector3.Cross(vec1.GetVector(), vec2.GetVector());
               strHead = StrHead("x");
               strBody = StrBody(vec);
               break;
           case FuncType.Invert:
               vec = -vec1.GetVector();
               strHead = StrHead("Inv");
               strBody = StrBody(vec);
               break;
           case FuncType.Sin:
               value = Mathf.Sin(Mathf.Deg2Rad * vec1.GetValue());
               strHead = StrHead("Sin");
               strBody = StrBody(value);
               numTypeb = false;
               break;
           case FuncType.Cos:
               value = Mathf.Cos(Mathf.Deg2Rad * vec1.GetValue());
               strHead = StrHead("Cos");
               strBody = StrBody(value);
               numTypeb = false;
               break;
           case FuncType.Combine:
               value = vec1.GetValue() * 10 + vec2.GetValue();
               strHead = StrHead("Num");
               strBody = StrBody(value);
               numTypeb = false;
               break;
           case FuncType.Negative:
               value = -vec1.GetValue();
               strHead = StrHead("Neg");
               strBody = StrBody(value);
               numTypeb = false;
               break;
       } 
       
       var fontSize = (numTypeb) ? 55 : 100;

       return new FuncIO(vec, value, strHead, strBody, fontSize);
   }
   
   private static string StrHead(string str)
   {
       return "<b>" + str + "</b>\n";
   }
   
   private static string StrBody(Vector3 v)
   { 
       return "(" + Round2(v.x) + ",\n" + Round2(v.y) + ",\n" + Round2(v.z) + ")";
   }

   public static float Round2(float value) {
       return Mathf.Round(value * 100) / 100f;
   }
   
    private static string StrBody(int n)
    {
        return n.ToString();
    }

    private static string StrBody(float q)
    {
        return ((int)(q * 100)/100f).ToString();
    }
}