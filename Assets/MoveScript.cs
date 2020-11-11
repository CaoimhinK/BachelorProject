using System;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MoveScript : MonoBehaviour
{
    public float speed = 10f;
    public Transform cam;

    private CharacterController _controller;
    private Vector3 _lastDirection;
    private Camera _cam;
    private RaycastHit _cursor;

    void Start()
    {
        _cam = Camera.main;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var mousePos = Input.mousePosition;

        var trans = transform;

        var direction = new Vector3(h, 0, v);

        _controller.Move(speed * Time.deltaTime * direction.normalized);
        
        var transPos = trans.position;
        
        cam.transform.position = transPos;
    }
}
