using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueContainer : MonoBehaviour
{
    public TMP_Text characterName;
    public TMP_Text textboxText;
    public Color old_Text = Color.grey;
    public string words;
    public AudioSource click;
    float typingSpeed = .016f;

    public bool canContinue;

    public void Start()
    {
        canContinue = false;
        click = GetComponent<AudioSource>();
        words = textboxText.text;
        textboxText.text = string.Empty;
        StartCoroutine(TypeSentence());

    }
    // Update is called once per frame
    public void Update()
    {
       if (Input.GetKeyDown(KeyCode.Mouse0))
       {
            characterName.color = old_Text;
            textboxText.color = old_Text;
           
        }

       if (TypeSentence() != null)
       {
            StopCoroutine(TypeSentence());
       }
    }

    public IEnumerator TypeSentence()
    {
        
        bool isAddingRichTextTag = false;

        foreach (char letter in words)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                textboxText.text = words;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                textboxText.text += letter;
                 
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                textboxText.text += letter;
                click.Play();
                yield return new WaitForSeconds(typingSpeed);
            }
        }
            

        yield return null;
 
    }
}
