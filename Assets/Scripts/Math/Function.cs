using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Spawner))]
public class Function : MonoBehaviour
{
    public Vector3 vec;
    public float value;
    public Matrix4x4 mat;
    public FuncType type;
    
    public Container vec1;
    public Container vec2;

    private Vector3 _last;
    private float _lastValue;

    public void Start()
    {
        var io = TypeFunctions.EvaluateFunction(type, vec1, vec2);
        io.Out(out vec, out value, out var mat, out var strHead, out var strBody, out var fontSize);
        
        var spaw = GetComponent<Spawner>();
        spaw.LoadTex();
        spaw.ChangeText(strHead, strBody, fontSize);
    }

    public void Update()
    {
        _last = vec;
        _lastValue = value;

        if (vec1.HasContentChanged() || vec2 && vec2.HasContentChanged())
        {
            var io = TypeFunctions.EvaluateFunction(type, vec1, vec2);
            io.Out(out vec, out value, out mat, out var strHead, out var strBody, out var fontSize);
            
            GetComponent<Spawner>().ChangeText(strHead, strBody, fontSize);
            vec1.ListenToContent();
            if (vec2) vec2.ListenToContent();
        }
    }
}
