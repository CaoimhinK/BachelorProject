using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GrabController : MonoBehaviour
{
    public Transform grabPos;
    public Inventory inventory;

    private GameObject _currentGo = null;
    private Anim anim;

    void Start()
    {
        anim = GetComponent<Anim>();
    }

    void Update()
    {
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
                            }
                            else
                            {
                                anim.GiveRec(_currentGo, hitGo.transform, rec);
                                _currentGo = inventory.Pop();
                                if (_currentGo) _currentGo.SetActive(true);
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
                        _currentGo = inventory.Pop();
                        if (_currentGo) _currentGo.SetActive(true);
                    }
                    else
                    {
                        but.PushButton();
                    }
                }
            }
        }
        else if (Input.GetButtonDown("Inventory 1"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo)) {
                var hitGo = hitInfo.collider.gameObject;
                if (hitInfo.collider.gameObject.TryGetComponent<DelButton>(out var but) && but.bin)
                {
                    var temp = inventory.StoreIndex(0, null);
                    if (temp)
                    {
                        temp.SetActive(true);
                        anim.Del(temp, hitGo);
                    }
                }
                else
                {
                    if (_currentGo) _currentGo.SetActive(false);
                    _currentGo = inventory.StoreIndex(0, _currentGo);
                    if (_currentGo) _currentGo.SetActive(true);
                }
            }
        }
        else if (Input.GetButtonDown("Inventory 2"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo)) {
                var hitGo = hitInfo.collider.gameObject;
                if (hitGo.TryGetComponent<DelButton>(out var but) && but.bin)
                {
                    var temp = inventory.StoreIndex(1, null);
                    if (temp)
                    {
                        temp.SetActive(true);
                        anim.Del(temp, hitGo);
                    }
                }
                else
                {
                    if (_currentGo) _currentGo.SetActive(false);
                    _currentGo = inventory.StoreIndex(1, _currentGo);
                    if (_currentGo) _currentGo.SetActive(true);
                }
            }
        }
        else if (Input.GetButtonDown("Inventory 3"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo)) {
                var hitGo = hitInfo.collider.gameObject;
                if (hitGo.TryGetComponent<DelButton>(out var but) && but.bin)
                {
                    var temp = inventory.StoreIndex(2, null);
                    if (temp)
                    {
                        temp.SetActive(true);
                        anim.Del(temp, hitGo);
                    }
                }
                else
                {
                    if (_currentGo) _currentGo.SetActive(false);
                    _currentGo = inventory.StoreIndex(2, _currentGo);
                    if (_currentGo) _currentGo.SetActive(true);
                }
            }
        }
    }
}
