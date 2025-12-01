using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueString> dialogStrings = new List<DialogueString>();
    private bool hasSpoken = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !hasSpoken) {
            other.gameObject.GetComponent<DialogueManager>().DialogStart(dialogStrings);
            Debug.Log(other.name + "\nWorks!");
            hasSpoken = true;
        }
    }
}

[System.Serializable]
public class DialogueString 
{
    public string text; //Represents NPC text
    public bool isEnd; //Represent if the line is the final line for the convo

    [Header("Trigger Events")]
    public UnityEvent startDialogEvent;
    public UnityEvent endDialogEvent;
}