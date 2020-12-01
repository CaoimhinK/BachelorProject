using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftWing;
    public GameObject rightWing;
    public bool isClosed = true;
    
    private readonly Vector3 openLeftPos = new Vector3(-3.25f, 2.5f, 0f);
    private readonly Vector3 closedLeftPos = new Vector3(-1.25f, 2.5f, 0f);
    private readonly Vector3 openRightPos = new Vector3(3.25f, 2.5f, 0f);
    private readonly Vector3 closedRightPos = new Vector3(1.25f, 2.5f, 0f);

    private const float DoorTime = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isClosed) OpenDoor();
            else CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (isClosed) StartCoroutine(nameof(OpenAnim));
    }

    void CloseDoor()
    {
        if (!isClosed) StartCoroutine(nameof(CloseAnim));
    }

    IEnumerator OpenAnim()
    {
        var startTime = Time.time;
        var elapsedTime = 0f;

        while (elapsedTime < DoorTime)
        {
            elapsedTime = Time.time - startTime;
            leftWing.transform.localPosition = Vector3.Lerp(closedLeftPos, openLeftPos, elapsedTime / DoorTime);
            rightWing.transform.localPosition = Vector3.Lerp(closedRightPos, openRightPos, elapsedTime / DoorTime);
            yield return null;
        }

        
        isClosed = false;
    }

    IEnumerator CloseAnim()
    {
        var startTime = Time.time;
        var elapsedTime = 0f;

        while (elapsedTime < DoorTime)
        {
            elapsedTime = Time.time - startTime;
            leftWing.transform.localPosition = Vector3.Lerp(openLeftPos, closedLeftPos, elapsedTime / DoorTime);
            rightWing.transform.localPosition = Vector3.Lerp(openRightPos, closedRightPos, elapsedTime / DoorTime);
            yield return null;
        }

        isClosed = true;
    }
}
