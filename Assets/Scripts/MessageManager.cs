using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public string[] keys;
    public Text[] mTexts;

    private Dictionary<string, Text> _messages;
    private Text _currentMessage;

    void Start()
    {
        _messages = new Dictionary<string, Text>();
        for (var i = 0; i < keys.Length; i++)
        {
            _messages.Add(keys[i], mTexts[i]);
        }
    }

    public void ShowMessage(string messageName)
    {
        if (_currentMessage) _currentMessage.gameObject.SetActive(false);
        var message = _messages[messageName];
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
