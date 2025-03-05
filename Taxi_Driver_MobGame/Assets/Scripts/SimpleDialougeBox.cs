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
    public TMP_Text text2;
    private CanvasGroup group;
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
        text2 = GetComponent<TMP_Text>();
        text = GetComponent<TMP_Text>();
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }


    public void Button()
    {
        if (!started)
        {
            linesindex = 0;
            text.SetText(_dialouge[linesindex]);
            text2.SetText(_dialouge[linesindex]);
            group.alpha = 1;
            started = true;
        }
        else if (linesindex < _dialouge.Count)
        {
            text.SetText(_dialouge[linesindex++]);
            text2.SetText(_dialouge[linesindex++]);

        }
        else if(linesindex == _dialouge.Count)
        {
            SceneManager.LoadScene("Next Scene");
        }
        else
            group.alpha = 0;
    }
  

}
