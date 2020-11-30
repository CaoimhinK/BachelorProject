using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public Dictionary<string, Text> messages;

    public string[] keys;
    public Text[] mTexts;

    private Text _currentMessage;

    void Start()
    {
        messages = new Dictionary<string, Text>();
        for (var i = 0; i < keys.Length; i++)
        {
            messages.Add(keys[i], mTexts[i]);
        }
    }

    public void ShowMessage(string name)
    {
        if (_currentMessage) _currentMessage.gameObject.SetActive(false);
        var message = messages[name];
        message.gameObject.SetActive(true);
        gameObject.SetActive(true);
        _currentMessage = message;
    }

    public void HideMessage()
    {
        _currentMessage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
