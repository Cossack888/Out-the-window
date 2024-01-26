using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string variableName;

    private void Update()
    {
        //Debug.Log("Variable State: " + dialogueManager.GetVariableState("friendlyGreeting"));
        if (Input.GetKeyDown(KeyCode.Space) && dialogueManager.GetVariableState("friendlyGreeting") == true)
        {
            Debug.Log("Space key pressed and friendlyGreeting is true");
            dialogueManager.ExitStory();
            dialogueManager.EnterStory(dialogueManager.dialogueContainers[1].dialog);
        }
    }
}
