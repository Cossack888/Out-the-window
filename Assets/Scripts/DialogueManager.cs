using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    //public TextAsset inkJSON;
    public DialogueContainer[] dialogueContainers;
    public Transform TextLocation;
    public Transform ButtonsLocation;
    public Story story;
    public static DialogueVariables variables;
    public TMP_Text textPrefab;
    public Button buttonPrefab;
    public Transform player;
    public GameObject ConvoCanvas;
    [Header("load globals json")]
    [SerializeField] public TextAsset loadGlobalsJson;

    public string CurrentText;




    public Dictionary<string, TextAsset> dialogueChanger = new Dictionary<string, TextAsset>();



    // Start is called before the first frame update
    void Start()
    {
        //story = new Story(dialogueContainers[0].dialog.text);

        //refreshUI();
        if (variables == null)
        {
            variables = new DialogueVariables(loadGlobalsJson);
        }
        // variables.StartListening(story);

    }

    private void Update()
    {
        if (story != null)
        {
            if (CurrentText.Contains("END"))
            {
                ExitStory();
                Debug.Log("starting new story");
            }
        }
    }

    public void EnterStory(TextAsset inkJson)
    {
        ConvoCanvas.SetActive(true);
        story = new Story(inkJson.text);
        variables.VariablesToStory(story); // Load global variables into the current story
        refreshUI();
        variables.StartListening(story);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
    public void ExitStory()
    {
        ConvoCanvas.SetActive(false);
        variables.StopListening(story);
        CurrentText = "";
        eraseUI();
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }



    void refreshUI()
    {
        //dialogVariables = new DialogVariables(globalsInkFile.filePath);
        // Debug.Log("new outcome is " + story.variablesState["outcome"]);

        eraseUI();

        TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;
        storyText.text = loadStoryChunk();
        storyText.transform.SetParent(TextLocation, false);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(ButtonsLocation, false);
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate
            {
                chooseStoryChoice(choice);
            });

            if (choiceText.text.Contains("END"))
            {
                choiceButton.onClick.AddListener(delegate
                {
                    ExitStory();
                });
            }
        }
    }

    void eraseUI()
    {
        if (TextLocation.childCount > 0)
        {
            Destroy(TextLocation.GetChild(0).gameObject);
        }

        for (int i = 0; i < ButtonsLocation.childCount; i++)
        {
            Destroy(ButtonsLocation.GetChild(i).gameObject);
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();

    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.ContinueMaximally();
            CurrentText = text;
        }
        else if (story.currentChoices.Count > 0)
        {
            // Handle choices if there are any
            refreshUI();
        }
        else
        {
            Debug.Log("End of conversation");
            ExitStory(); // Call your method to handle the end of the conversation
        }

        return text;
    }


    public bool GetVariableState(string variableName)
    {
        if (variables.variables.TryGetValue(variableName, out Ink.Runtime.Object variableValue))
        {
            // Check if the variableValue is truthy
            if (variableValue != null && variableValue.ToString().ToLower() == "true")
            {
                return true;
            }
            else
            {
                Debug.LogWarning("Ink Variable is not truthy (considered false): " + variableName);
                return false;
            }
        }
        else
        {
            Debug.LogWarning("Ink Variable not found: " + variableName);
            return false;
        }
    }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit()
    {
        variables.SaveVariables();
    }
}