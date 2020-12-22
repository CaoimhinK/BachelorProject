using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public InputHandler input;
    public MessageManager mm;
    public GameObject[] rooms;
    public Door[] doors;
    public FieldGenerator fieldGenerator;
    public Coplanarity coplanarity;
    public Shapes shapes;
    public MatrixApplier texture;

    public bool[] roomOverride;

    private Chapter _chapter;
    private bool _halt;
    private bool ende;

    void Start()
    {
        _chapter = Chapter.Tutorial;
        input.lockInput = true;
        foreach (var room in rooms)
        {
            room.SetActive(false);
        }
        foreach (var door in doors)
        {
            door.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_halt)
        {
            if (Input.GetButtonDown("Grab"))
            {
                _halt = false;
                mm.HideMessage();
                input.lockInput = false;
            }
            return;
        }
        foreach (var door in doors)
        {
            if (door.onDoor && door.locked && Input.GetButtonDown("Grab"))
            {
                mm.ShowMessage("DoorLocked");
                input.lockInput = true;
                _halt = true;
            }
        }

        switch (_chapter)
        {
            case Chapter.Tutorial:
                if (!mm) break;
                rooms[0].SetActive(true);
                doors[0].gameObject.SetActive(true);
                mm.ShowMessage("Tutorial");
                _chapter = Chapter.PreStart;
                break;
            case Chapter.PreStart:
                if (Input.GetButtonDown("Grab"))
                {
                    mm.HideMessage();
                    mm.ShowMessage("Start");
                    _chapter = Chapter.Start;
                }
                break;
            case Chapter.Start:
                if (Input.GetButtonDown("Grab"))
                {
                    input.lockInput = false;
                    mm.HideMessage();
                    _chapter = Chapter.Room1;
                }
                break;
            case Chapter.Room1:
                if (roomOverride[0] || fieldGenerator.CheckSolved())
                {
                    NextRoom(1);
                    _chapter = Chapter.Room2;
                }
                break;
            case Chapter.Room2:
                if (roomOverride[1] || coplanarity.CheckSolved())
                {
                    NextRoom(2);
                    _chapter = Chapter.Room3;
                }
                break;
            case Chapter.Room3:
                if (roomOverride[2] || shapes.CheckSolved())
                {
                    NextRoom(3);
                    _chapter = Chapter.Room4;
                }
                break;
            case Chapter.Room4:
                if (roomOverride[3] || texture.IsCorrect())
                {
                    _chapter = Chapter.Ending;
                }
                break;
            case Chapter.Ending:
                if (!ende)
                {
                    StartCoroutine(nameof(QueueEnding));
                }
                break;
        }
    }

    private void NextRoom(int index)
    {
        rooms[index].SetActive(true);
        rooms[index].GetComponent<RoomEnterTrigger>().deactivated = roomOverride[index - 1];
        if (index < 3) doors[index].gameObject.SetActive(true);
        doors[index - 1].UnlockDoor();
    }

    IEnumerator QueueEnding()
    {
        yield return new WaitForSeconds(3f);
        ende = true;
        mm.ShowMessage("Ending");
        input.lockInput = true;
    }
}

enum Chapter
{
    PreStart,
    Start,
    Room1,
    Room2,
    Room3,
    Room4,
    Ending,
    Tutorial,
}