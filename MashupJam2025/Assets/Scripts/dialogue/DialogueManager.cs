using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private Button[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField]
    private TextAsset currentStoryJSON = null;
    private Story currentStory;

    [SerializeField] private  Button Continue;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        for (int index = 0; index < choices.Length; index++)
        {
            choicesText[index] = choices[index].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void StartStory() => StartStory(currentStoryJSON);
    public void StartStory(TextAsset inkJSON)
    {
        currentStoryJSON = inkJSON;
        currentStory = new Story(currentStoryJSON.text);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        RefreshView();
    }
    public void RefreshView()
    {
        if(currentStory.canContinue)
        {
            string text = currentStory.Continue();
            text = text.Trim();
            dialogueText.text = text;
        }
        else
        {
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
        }

        DisplayChoices();
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        Continue.gameObject.SetActive(currentChoices.Count == 0);
        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError($"More choices were given than the UI can support. Number of choices given: {currentChoices.Count}");
        }

        for (int index = 0; index < choices.Length; index++)
        {
            if(index < currentChoices.Count)
            {
                // enable and initialize the choices up to the amount of choices for this line of dialogue
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = currentChoices[index].text;
            }
            else
            {
                // go through the remaining choices the UI supports and make sure they're hidden
                choices[index].gameObject.SetActive(false);
            }

        }
    }
    public void MakeChoice(int choiceIndex)
    {
        Debug.Log($"Choice {choiceIndex}");
        currentStory.ChooseChoiceIndex(choiceIndex);
        RefreshView();
    }
}