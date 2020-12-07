using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coplanarity : MonoBehaviour
{
    public Transform[] points;
    public Spawner[] spawns;
    
    void Start()
    {
        for (var i = 0; i < 4; i++)
        {
            var point = points[i];
            var spawn = spawns[i];
            spawn.spawnVecValue = point.localPosition;
        }
        
    }
}
