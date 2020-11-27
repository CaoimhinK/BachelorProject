using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MoveScript : MonoBehaviour
{
    public float speed = 10f;
    public Transform cam;

    private CharacterController _controller;
    private Vector3 _lastDirection;
    private RaycastHit _cursor;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var trans = transform;

        var direction = new Vector3(h, 0, v);

        _controller.Move(speed * Time.deltaTime * (direction.normalized));
        transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
        
        var transPos = trans.position;
        
        cam.transform.position = transPos;
    }
}
