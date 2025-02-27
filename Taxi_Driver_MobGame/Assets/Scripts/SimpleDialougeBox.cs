using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleDialougeBox : MonoBehaviour
{
    [SerializeField]
    [TextArea]

    private List<string> _dialouge;
    private int linesindex;
    private TMP_Text text;
    private CanvasGroup group;
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!started)
            {
                linesindex = 0;
                text.SetText(_dialouge[linesindex]);
                group.alpha = 1;
                started = true;
            }
            else if (linesindex < _dialouge.Count)
            {
                text.SetText(_dialouge[linesindex++]);
            }
            else
                group.alpha  = 0;

        }
    }
}
