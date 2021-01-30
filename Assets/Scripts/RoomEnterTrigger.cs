using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomEnterTrigger : MonoBehaviour
{
    public InputHandler input;
    public string enterMessageName;
    private bool _hasBeenShown;
    private bool _messageDone;

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasBeenShown)
            StartCoroutine(nameof(Wait));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_hasBeenShown)
            StopCoroutine(nameof(Wait));
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        input.currentMessageName = enterMessageName;
        input.TriggerEnterMessage();
        _hasBeenShown = true;
    }
}
