using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Transform grabPos;
    public Inventory inventory;
    public float speed = 10f;
    public bool lockInput;
    public Transform cam;
    public MessageManager mm;

    public string currentMessageName = "Start";
    
    private const int NumInventorySlots = 3;
    private CharacterController _controller;
    private GameObject _currentGo = null;
    private Anim _anim;
    private Camera _cam;
    private GameObject _currentWall;
    private Material _currentMat;
    private bool _messageShowing;
    
    void Start()
    {
        _cam = Camera.main;
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Anim>();
        HandleMove();
    }

    void Update()
    {
        if (lockInput) return;
        HandleMove();
        CheckWalls();
        if (Input.GetButtonDown("Grab"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo))
            {
                var hitGo = hitInfo.collider.gameObject;
                if (HitGoTypeIs<Spawner>(hitGo, out var spawner))
                {
                    if (!_currentGo) {
                        _currentGo = _anim.Spawn(spawner);
                    }
                    else if (inventory.Push(_currentGo)) {
                        _currentGo.SetActive(false);
                        _currentGo = _anim.Spawn(spawner);
                    }
                }
                else if (HitGoTypeIs<Recepticle>(hitGo, out var rec))
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
                                    _currentGo = _anim.TakeRec(rec);
                                }
                                else
                                {
                                    _anim.Warn(hitGo);
                                }
                            }
                            else
                            {
                                _anim.GiveRec(_currentGo, hitGo.transform, rec);
                                _currentGo = null;
                            }
                        }
                        else
                        {
                            _anim.Warn(hitGo);
                        }
                    }
                    else if (rec.HasObject())
                    {
                        _currentGo = _anim.TakeRec(rec);
                    }
                }
                else if (HitGoTypeIs<MatrixApplier>(hitGo, out var mapp))
                {
                    mapp.ApplyMatrix();
                }
                else if (HitGoTypeIs<NormalApplier>(hitGo, out var napp))
                {
                    napp.ApplyNormal();
                }
                else if (HitGoTypeIs<DelButton>(hitGo, out var delBut))
                {
                    if (delBut.bin)
                    {
                        if(_currentGo) _anim.Del(_currentGo, hitGo);
                    }
                    else
                    {
                        delBut.PushButton();
                    }
                }
                else if (HitGoTypeIs<Noidel.Button>(hitGo, out var but))
                {
                    but.PushButton();
                }
                else if (_messageShowing)
                {
                    mm.HideMessage();
                    _messageShowing = false;
                }
            }
        }
        else if (Input.GetButtonDown("ShowMessage"))
        {
            if (_messageShowing)
            {
                mm.HideMessage();
                _messageShowing = false;
            }
            else
            {
                mm.ShowMessage(currentMessageName);
                _messageShowing = true;
            }
        }
        else
        {
            for (var i = 0; i < NumInventorySlots; i++)
            {
                if (Input.GetButtonDown($"Inventory {i}"))
                {
                    HandleInventorySlot(i);
                }
            }
        }
    }

    private void HandleInventorySlot(int index)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo)) {
            var hitGo = hitInfo.collider.gameObject;
            if (HitGoTypeIs<DelButton>(hitGo, out var but) && but.bin)
            {
                var temp = inventory.GetIndex(index);
                if (temp)
                {
                    temp.SetActive(true);
                    _anim.Del(temp, hitGo);
                }
            }
            else if (HitGoTypeIs<Recepticle>(hitGo, out var rec))
            {
                if (inventory.IndexFull(index) && !rec.HasObject())
                {
                    var temp = inventory.GetIndex(index);
                    if (temp.GetComponent<MathObj>().Type == rec.type)
                    {
                        temp.SetActive(true);
                        _anim.GiveRec(temp, hitGo.transform, rec);
                    }
                    else
                    {
                        inventory.SetIndex(index, temp);
                        _anim.Warn(hitGo);
                    }
                }
                else if (!inventory.IndexFull(index) && rec.HasObject())
                {
                    _anim.TakeRecToInventory(rec, inventory, index);
                }
                else
                {
                    _anim.Warn(hitGo);
                }
            }
            else if (HitGoTypeIs<Spawner>(hitGo, out var spawn))
            {
                if (inventory.IndexFull(index))
                {
                    _anim.Warn(hitGo);
                    return;
                }
                _anim.SpawnInInventory(spawn, inventory, index);
            }
            else
            {
                if (_currentGo) _currentGo.SetActive(false);
                _currentGo = inventory.SwapIndex(index, _currentGo);
                if (_currentGo) _currentGo.SetActive(true);
            }
        }
    }

    private bool HitGoTypeIs<T>(GameObject hitGo, out T obj)
    {
        return hitGo.TryGetComponent(out obj);
    }

    private void HandleMove()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var trans = transform;

        var direction = new Vector3(h, 0, v);

        _controller.Move(speed * Time.deltaTime * (direction.normalized));
        var pos = transform.position;
        var newPos = new Vector3(pos.x, 1.4f, pos.z);
        
        trans.position = newPos;
        var transPos = trans.position;
        
        cam.transform.position = transPos;
    }

    private void CheckWalls()
    {
        Physics.Raycast(_cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out var hitInfo);
        var potentialWall = (hitInfo.collider) ? hitInfo.collider.gameObject : null;
        bool isWall = potentialWall && potentialWall.CompareTag("Wall");
        if (!_currentWall && isWall)
        {
            _currentWall = potentialWall;
            _currentMat = potentialWall.GetComponent<MeshRenderer>().material;
            _anim.Fade(_currentMat, 0.3f);
        }
        else if (_currentWall && !isWall)
        {
            _anim.Fade(_currentMat, 1f);
            _currentWall = null;
        }
        else if (isWall && _currentWall != potentialWall)
        {
            var oldMat = _currentMat;
            _anim.Fade(oldMat, 1f);
            _currentWall = potentialWall;
            _currentMat = potentialWall.GetComponent<MeshRenderer>().material;
            _anim.Fade(_currentMat, 0.3f);
        }
    }

    public void TriggerEnterMessage()
    {
        mm.ShowMessage(currentMessageName);
        _messageShowing = true;
    }
}
