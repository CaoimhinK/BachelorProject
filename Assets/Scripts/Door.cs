using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftWing;
    public GameObject rightWing;
    public bool isClosed = true;
    public bool locked = true;
    public bool onDoor = false;

    private readonly Vector3 openLeftPos = new Vector3(-3.25f, 2.5f, 0f);
    private readonly Vector3 closedLeftPos = new Vector3(-1.25f, 2.5f, 0f);
    private readonly Vector3 openRightPos = new Vector3(3.25f, 2.5f, 0f);
    private readonly Vector3 closedRightPos = new Vector3(1.25f, 2.5f, 0f);

    private const float DoorTime = 0.5f;

    private bool opening = false;
    private bool closing = false;

    void OnTriggerEnter()
    {
        onDoor = true;
        if (locked) return;
        if (closing) StopCoroutine(nameof(CloseAnim));
        StartCoroutine(nameof(OpenAnim));
    }

    void OnTriggerExit()
    {
        onDoor = false;
        if (locked) return;
        if (opening) StopCoroutine(nameof(OpenAnim));
        StartCoroutine(nameof(CloseAnim));
    }

    public void UnlockDoor()
    {
        locked = false;
    }

    IEnumerator OpenAnim()
    {
        opening = true;
        var startTime = Time.time;
        var elapsedTime = 0f;

        while (elapsedTime < DoorTime)
        {
            elapsedTime = Time.time - startTime;
            leftWing.transform.localPosition = Vector3.Lerp(leftWing.transform.localPosition, openLeftPos, elapsedTime / DoorTime);
            rightWing.transform.localPosition = Vector3.Lerp(rightWing.transform.localPosition, openRightPos, elapsedTime / DoorTime);
            yield return null;
        }

        isClosed = false;
        opening = false;
    }

    IEnumerator CloseAnim()
    {
        closing = true;
        var startTime = Time.time;
        var elapsedTime = 0f;

        while (elapsedTime < DoorTime)
        {
            elapsedTime = Time.time - startTime;
            leftWing.transform.localPosition = Vector3.Lerp(leftWing.transform.localPosition, closedLeftPos, elapsedTime / DoorTime);
            rightWing.transform.localPosition = Vector3.Lerp(rightWing.transform.localPosition, closedRightPos, elapsedTime / DoorTime);
            yield return null;
        }

        isClosed = true;
        closing = false;
    }
}
