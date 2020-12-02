using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputHandler input;
    public MessageManager mm;
    public GameObject[] rooms;
    public Door door;

    private Chapter _chapter;

    void Start()
    {
        _chapter = Chapter.PreStart;
        input.lockInput = true;
        foreach (var room in rooms)
        {
            room.SetActive(false);
        }
    }

    void Update()
    {
        switch (_chapter)
        {
            case Chapter.PreStart:
                if (!mm) break;
                mm.ShowMessage("Start");
                _chapter = Chapter.Start;
                rooms[0].SetActive(true);
                break;
            case Chapter.Start:
                if (Input.GetButtonDown("Grab"))
                {
                    input.lockInput = false;
                    mm.HideMessage();
                    _chapter = Chapter.Level1;
                }
                break;
            case Chapter.Level1:
                if (!door.isClosed)
                {
                    rooms[1].SetActive(true);
                    _chapter = Chapter.Level2;
                }
                break;
            case Chapter.Level2:
                break;
        }
    }
}

enum Chapter
{
    PreStart,
    Start,
    Level1,
    Level2,
}