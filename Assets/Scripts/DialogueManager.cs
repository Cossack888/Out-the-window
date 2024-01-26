using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    public DialogueContainer[] dialogueContainers;
  
    public Story story;
    public static DialogueVariables variables;
    public Text textPrefab;
    public Button buttonPrefab;
   
    
    [Header("load globals json")]
    [SerializeField] public TextAsset loadGlobalsJson;

    public int Health = 0;
    public bool finished = false;
    public bool reset = false;
    
    public int agression = 0;
    public int heart = 0;
    public int professional = 0;
    public int violence = 0;
    public string CurrentText;

   


    public Dictionary<string, TextAsset> dialogueChanger = new Dictionary<string, TextAsset>();



    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);
       
            refreshUI();
            if (variables == null)
            {
                variables = new DialogueVariables(loadGlobalsJson);
            }
            variables.StartListening(story);
       
    }

    private void Awake()
    {



    }

    void refreshUI()
    {
        //dialogVariables = new DialogVariables(globalsInkFile.filePath);
        // Debug.Log("new outcome is " + story.variablesState["outcome"]);

        eraseUI();
        
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = loadStoryChunk();
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);
            
                
            
          

            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate {
                chooseStoryChoice(choice);
            });

        }
    }

    void eraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue)
        {

            text = story.ContinueMaximally();
            CurrentText = text;

            if (text.Contains("(You are being agressive)"))
            {
                agression = agression + 1;
            }
            if (text.Contains("(You are being kind)"))
            {
                heart = heart + 1;
            }
            if (text.Contains("(You are being professional)"))
            {
                professional = professional + 1;
            }

            if (text.Contains("(You resorted to violence)"))
            {
                violence = violence + 1;
            }


            if (text.Contains("(END)"))
            {
                finished = true;
            }

            



        }
        Debug.Log("new outcome is " + story.variablesState["outcome"]);
        
        
        return text;
    }

    


    // public Ink.Runtime.Object GetVariableState(string variableName)
    // {
    //    Ink.Runtime.Object variableValue = null;
    //    dialogVariables.variables.TryGetValue(variableName, out variableValue);
    //    if (variableValue == null)
    //   {
    //       Debug.LogWarning("Ink Variable was found to be null: " + variableName);
    //   }
    //   return variableValue;
    //}
}