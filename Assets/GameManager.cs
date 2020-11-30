using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputHandler input;
    public MessageManager mm;

    private Chapter chapter;

    void Start()
    {
        input.lockInput = true;
        mm.ShowMessage("Start");
        chapter = Chapter.Start;
    }

    void Update()
    {
        switch (chapter)
        {
            case Chapter.Start:
                if (Input.GetButtonDown("Grab"))
                {
                    input.lockInput = false;
                    mm.HideMessage();
                    chapter = Chapter.Level1;
                }
                break;
            case Chapter.Level1:
                break;
        }
    }
}

enum Chapter
{
    Start,
    Level1,
}