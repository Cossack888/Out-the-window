using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "dialog", order = 1)]
public class DialogueContainer : ScriptableObject
{
    public string ID;

    public TextAsset dialog;
}