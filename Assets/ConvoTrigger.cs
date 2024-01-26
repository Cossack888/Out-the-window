using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoTrigger : MonoBehaviour
{
    [SerializeField] DialogueManager manager;

    [SerializeField] int id;
    private void OnTriggerEnter(Collider other)
    {

        manager.EnterStory(manager.dialogueContainers[id].dialog);
        gameObject.SetActive(false);
    }


}
