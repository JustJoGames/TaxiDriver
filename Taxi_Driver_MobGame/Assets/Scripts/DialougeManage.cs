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

    public DialogueContainer contain;
    public string words;


    public void Awake()
    {
        contain = GetComponent<DialogueContainer>();
        ourStory = new Story(inkAsset.text);
        choiceSelected = null;

    }

    public void Start()
    {

    }

    public void Update()
    {
       
    }

    public void FinishDialogue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("End of Dialogue!");
    }


    public void AdvanceStory()
    {
        ParseTags();
        StopAllCoroutines();

        UIX.UpdateLayout(canvas.transform);
        scrollRect.verticalNormalizedPosition = 0f;

        if (ourStory.currentChoices.Count != 0) //Are there any choices?
        {
            StartCoroutine(ShowChoices());

        }

        if (!ourStory.canContinue && ourStory.currentChoices.Count == 0)
        {
            FinishDialogue();
        }

    }

    public void AdvanceFromDecision()
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
   
    public void ParseTags()
    {
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
                scrollRect.verticalNormalizedPosition = -1f;
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
}
