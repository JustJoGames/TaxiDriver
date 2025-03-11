using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueContainer : MonoBehaviour
{
    public TMP_Text characterName;
    public TMP_Text textboxText;
    public Color old_Text = Color.grey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterName.color = old_Text;
            textboxText.color = old_Text;
        }
    }
}
