using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;


public class DialougeManage : MonoBehaviour
{
    public Canvas canvas;
    public ScrollRect scrollRect;

    [Header("InkJSON")]
    // Set this file to your compiled json asset
    public TextAsset inkAsset;
    static Story ourStory;

    public GameObject customButton;
    public GameObject optionPanel;
    bool isChoosing;

    public GameObject dialogueContainer;
    public Transform dialogueContent;
    static Choice choiceSelected;

    private void Awake()
    {
        ourStory = new Story(inkAsset.text);
        choiceSelected = null;
        isChoosing = false;
    }

    private void Start()
    {

        AdvanceStory();
    }

    private void Update()
    {
        if (isChoosing == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (ourStory.canContinue)
                {
                    AdvanceStory();

                    if (ourStory.currentChoices.Count != 0) //Are there any choices?
                    {
                        StartCoroutine(ShowChoices());

                    }

                }
                else
                {
                    FinishDialogue();
                }
                
            }

        }
        else if(isChoosing == true)
        {

            return;
        }

    }

    private void FinishDialogue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Debug.Log("End of Dialogue!");
    }


    void AdvanceStory()
    {
        isChoosing = false;

        string text = ourStory.Continue();

        foreach (string tag in ourStory.currentTags)
        {
            if (tag.StartsWith("name;"))
            {
                string[] parts = tag.Split(';');
                string characterName = parts[1];

                GameObject currentDialogue = Instantiate(dialogueContainer, dialogueContent, true);
                currentDialogue.GetComponent<DialogueContainer>().characterName.text = characterName;
                currentDialogue.GetComponent<DialogueContainer>().textboxText.text = text;

                UIX.UpdateLayout(canvas.transform);
                scrollRect.verticalNormalizedPosition = 0f;
            }

            if (tag.StartsWith("scene; A"))
            {
                SceneManager.LoadScene("FinalCutScene_A");
            }

            if (tag.StartsWith("scene; B"))
            {
                SceneManager.LoadScene("FinalCutScene_B");
            }
        }

    }

    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        AdvanceStory();

        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }


        UIX.UpdateLayout(canvas.transform);
        scrollRect.verticalNormalizedPosition = 0f;

        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
    }


    IEnumerator ShowChoices()
    {
        isChoosing = true;
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = ourStory.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        ourStory.ChooseChoiceIndex(choiceSelected.index);
     

    }

    IEnumerator TypeSentence(string text)
    {
       GetComponent<DialogueContainer>().textboxText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            GetComponent<DialogueContainer>().textboxText.text += letter;
            yield return null;
        }
    }
}
