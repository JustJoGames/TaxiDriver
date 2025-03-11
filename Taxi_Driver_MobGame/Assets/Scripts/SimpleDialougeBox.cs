using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SimpleDialougeBox : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private List<string> _dialouge;
    private int linesindex;
    public TMP_Text text;
    private CanvasGroup group;
    private bool started;

    private char delimiter = '*';   // added to message, used to parse messages from text string
    const float _timeToLive = 10;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }


    public void Button()
    {
        if (!started)
        {
            linesindex = 0;
            DisplayMessage(_dialouge[linesindex]);
            group.alpha = 1;
            started = true;
        }
        else if (linesindex < _dialouge.Count)
        {
            DisplayMessage(_dialouge[linesindex++]);
          

        }
        else if(linesindex == _dialouge.Count)
        {
            SceneManager.LoadScene("Next Scene");
        }
        else
            group.alpha = 0;
    }

    public void DisplayMessage(string msg)
    {
        msg = delimiter + " " + msg + "\n";        // add the delimiter and a space to the start of the message string, and a new line character to the end
        text.text = msg + text.text;               // add the message to the start of the TextMesh component
        StartCoroutine(DisplayMessageRoutine());
    }

    public IEnumerator DisplayMessageRoutine()
    {
        yield return new WaitForSecondsRealtime(_timeToLive);

        string tmp = text.text;         // this may be unnecessary...
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

        text.text = tmp;
    }

}
