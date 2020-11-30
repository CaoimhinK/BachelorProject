using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTrigger : MonoBehaviour
{
    public NormalApplicant app;
    public int index;

    void OnTriggerEnter(Collider coll)
    {
        app.VertexTriggered(index);
    }

    void OnTriggerExit(Collider coll)
    {
        app.VertexLeft(index);
    }
}
