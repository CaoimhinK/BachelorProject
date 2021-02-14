using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public string[] keys;
    public Text[] mTexts;
    public GameObject exit;

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
        exit.gameObject.SetActive(false);
        var message = _messages[messageName];
        message.gameObject.SetActive(true);
        gameObject.SetActive(true);
        _currentMessage = message;
    }

    public void ShowMessage(string messageName, float time)
    {
        if (_currentMessage) _currentMessage.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        var message = _messages[messageName];
        message.gameObject.SetActive(true);
        gameObject.SetActive(true);

        var timeString = FormatTimeString(time);

        message.text = string.Format(message.text, timeString);

        _currentMessage = message;
    }

    public void HideMessage()
    {
        exit.gameObject.SetActive(false);
        _currentMessage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        if (_currentMessage) _currentMessage.gameObject.SetActive(false);
        exit.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }

    private string FormatTimeString(float time)
    {
      var span = System.TimeSpan.FromSeconds(time);
      var mins = span.Minutes;
      var minString = (mins > 0)
        ? string.Format("{0:D2} minutes, ", span.Minutes)
        : "";
      var secString = string.Format("{0:D2} seconds",
          span.Seconds);

      return minString + secString;
    }
}
