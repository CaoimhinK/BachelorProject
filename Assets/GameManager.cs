using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputHandler input;
    public MessageManager mm;
    public GameObject[] rooms;
    public Door[] doors;
    public FieldGenerator fieldGenerator;
    public Noidel.Button[] buttons;

    private Chapter _chapter;
    private bool halt;

    void Start()
    {
        _chapter = Chapter.PreStart;
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
        if (halt)
        {
            if (Input.GetButtonDown("Grab"))
            {
                halt = false;
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
                halt = true;
            }
        }

        switch (_chapter)
        {
            case Chapter.PreStart:
                if (!mm) break;
                mm.ShowMessage("Start");
                _chapter = Chapter.Start;
                rooms[0].SetActive(true);
                doors[0].gameObject.SetActive(true);
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
                if (fieldGenerator.CheckSolved())
                {
                    rooms[1].SetActive(true);
                    doors[1].gameObject.SetActive(true);
                    doors[0].UnlockDoor();
                    _chapter = Chapter.Room2;
                }
                break;
            case Chapter.Room2:
                if (buttons[0].WasPushed())
                {
                    rooms[2].SetActive(true);
                    doors[2].gameObject.SetActive(true);
                    doors[1].UnlockDoor();
                    _chapter = Chapter.Room3;
                }
                break;
            case Chapter.Room3:
                if (buttons[1].WasPushed())
                {
                    rooms[3].SetActive(true);
                    doors[2].UnlockDoor();
                    _chapter = Chapter.Room4;
                }
                break;
            case Chapter.Room4:
                if (buttons[2].WasPushed())
                {
                    _chapter = Chapter.Ending;
                }
                break;
            case Chapter.Ending:
                mm.ShowMessage("Ending");
                input.lockInput = true;
                break;
        }
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
}