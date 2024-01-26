using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoTrigger : MonoBehaviour
{
    [SerializeField] DialogueManager manager;
    public GameObject ConvoCanvas;
    [SerializeField] int id;
    private void OnTriggerEnter(Collider other)
    {
        ConvoCanvas.SetActive(true);
        manager.EnterStory(manager.dialogueContainers[id].dialog);
    }

    private void OnTriggerExit(Collider other)
    {
        ConvoCanvas.SetActive(false);
    }
}
