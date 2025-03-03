using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private char delimiter = '*';   // added to message, used to parse messages from text string
    const float _timeToLive = 3;   // the amount of time a message remains on screen

    public void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            DisplayMessage("This is a message, yo!");

        if (Input.GetKeyDown(KeyCode.P))
            DisplayMessage("Totally different message.");
    }

    /// <summary>
    /// The string, msg, will be appended to the textfield and removed after _timeToLive seconds
    /// </summary>
    /// <param name="s"></param>
    /// <param name="ttl"></param>
    public void DisplayMessage(string msg)
    {
        msg = delimiter + " " + msg + "\n";        // add the delimiter and a space to the start of the message string, and a new line character to the end
        textMesh.text = msg + textMesh.text;               // add the message to the start of the TextMesh component
        StartCoroutine(DisplayMessageRoutine());
    }

    public IEnumerator DisplayMessageRoutine()
    {
        yield return new WaitForSecondsRealtime(_timeToLive);

        string tmp = textMesh.text;         // this may be unnecessary...
        bool delimiterReached = false;
        int escape = 200;                   // just in case I screwed up

        // remove characters from the end of the string until (and including) the delimiter is reached
        while (!delimiterReached && escape > 0)
        {
            if (tmp[tmp.Length - 1] == delimiter)
                delimiterReached = true;

            tmp = tmp.Substring(0, tmp.Length - 1);

            escape--;
            if (escape <= 0)
                Debug.LogError("loop exited by escape case");
        }

        textMesh.text = tmp;
    }
}