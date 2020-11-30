using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class InputHandler : MonoBehaviour
{
    public Transform grabPos;
    public Inventory inventory;
    public float speed = 10f;
    public bool lockInput;
    public Transform cam;

    private CharacterController _controller;
    private GameObject _currentGo = null;
    private Anim anim;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Anim>();
        HandleMove();
    }

    void Update()
    {
        if (lockInput) return;
        HandleMove();
        if (Input.GetButtonDown("Grab"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo))
            {
                var hitGo = hitInfo.collider.gameObject;
                if (hitGo.TryGetComponent<Spawner>(out var spawner))
                {
                    if (!_currentGo) {
                        _currentGo = anim.Spawn(spawner);
                    }
                    else if (inventory.Push(_currentGo)) {
                        _currentGo.SetActive(false);
                        _currentGo = anim.Spawn(spawner);
                    }
                }
                else if (hitGo.TryGetComponent<Recepticle>(out var rec))
                {
                    if (_currentGo)
                    {
                        if (_currentGo.GetComponent<MathObj>().Type == rec.type)
                        {
                            if (rec.HasObject())
                            {
                                if (inventory.Push(_currentGo))
                                {
                                    _currentGo.SetActive(false);
                                    _currentGo = anim.TakeRec(rec);
                                }
                                else
                                {
                                    anim.Warn(hitGo);
                                }
                            }
                            else
                            {
                                anim.GiveRec(_currentGo, hitGo.transform, rec);
                                _currentGo = null;
                            }
                        }
                        else
                        {
                            anim.Warn(hitGo);
                        }
                    }
                    else if (rec.HasObject())
                    {
                        _currentGo = anim.TakeRec(rec);
                    }
                }
                else if (hitGo.TryGetComponent<MatrixApplier>(out var mapp))
                {
                    mapp.ApplyMatrix();
                }
                else if (hitGo.TryGetComponent<NormalApplier>(out var napp))
                {
                    napp.ApplyNormal();
                }
                else if (hitGo.TryGetComponent<DelButton>(out var but))
                {
                    if (but.bin)
                    {
                        if(_currentGo) anim.Del(_currentGo, hitGo);
                    }
                    else
                    {
                        but.PushButton();
                    }
                }
            }
        }
        else
        {
            for (var i = 0; i < 3; i++)
            {
                if (Input.GetButtonDown("Inventory " + i))
                {
                    HandleInventorySlot(i);
                    break;
                }
            }
        }
    }

    private void HandleInventorySlot(int index)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo)) {
            var hitGo = hitInfo.collider.gameObject;
            if (hitGo.TryGetComponent<DelButton>(out var but) && but.bin)
            {
                var temp = inventory.StoreIndex(index, null);
                if (temp)
                {
                    temp.SetActive(true);
                    anim.Del(temp, hitGo);
                }
            }
            else if (hitGo.TryGetComponent<Recepticle>(out var rec))
            {
                var temp = inventory.StoreIndex(index, null);
                if (temp && !rec.HasObject())
                {
                    temp.SetActive(true);
                    anim.GiveRec(temp, hitGo.transform, rec);
                }
                else if (temp)
                {
                    inventory.StoreIndex(index, temp);
                    anim.Warn(hitGo);
                }
            }
            else
            {
                if (_currentGo) _currentGo.SetActive(false);
                _currentGo = inventory.StoreIndex(index, _currentGo);
                if (_currentGo) _currentGo.SetActive(true);
            }
        }
    }

    void HandleMove()
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
