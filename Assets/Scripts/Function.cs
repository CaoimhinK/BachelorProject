using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
        
        var io = TypeFunctions.EvaluateFunction(type, vec1, vec2);
        io.Out(out vec, out value, out var strHead, out var strBody, out var fontSize);
        
        if (!vec.Equals(_last))
        {
            GetComponent<Spawner>().ChangeText(strHead, strBody, fontSize);
        }
        if (!value.Equals(_lastValue))
        {
            GetComponent<Spawner>().ChangeText(strHead, strBody, fontSize);
        }
    }
}
