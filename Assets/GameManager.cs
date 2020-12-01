using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputHandler input;
    public MessageManager mm;

    private Chapter _chapter;

    void Start()
    {
        _chapter = Chapter.PreStart;
        input.lockInput = true;
    }

    void Update()
    {
        switch (_chapter)
        {
            case Chapter.PreStart:
                if (!mm) break;
                mm.ShowMessage("Start");
                _chapter = Chapter.Start;
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
                break;
        }
    }
}

enum Chapter
{
    PreStart,
    Start,
    Level1,
}