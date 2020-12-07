using UnityEngine;

public class Coplanarity : MonoBehaviour
{
    public Transform[] points;
    public Spawner[] spawns;

    private float _correct;
    
    void Start()
    {
        var positions = new[]
        {
            new Vector3(1, 1, -6),
            new Vector3(0, -2, 1),
            new Vector3(-2, 0, -1),
            new Vector3(-5, 1, 0)
        };

        for (var i = 0; i < 4; i++)
        {
            points[i].localPosition = positions[i];
            spawns[i].spawnVecValue = positions[i];
        }
    }
}
