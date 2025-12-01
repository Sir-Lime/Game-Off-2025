using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.Events;
using TMPro;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine.Animations;
using playerController;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogParent;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private float typingSpeed = 0.05f;

    private List<DialogueString> dialogList;
    private RigidbodyConstraints2D originalConstraints;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    private int currentDialogIndex = 0;

    private void Start()
    {
        originalConstraints = playerController.GetComponent<Rigidbody2D>().constraints;
        dialogParent.SetActive(false);
    }

    public void DialogStart(List<DialogueString> textToPrint)
    {
        dialogParent.SetActive(true);
        playerController.enabled = false;
        playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        dialogList = textToPrint;
        currentDialogIndex = 0;

        StartCoroutine(PrintDialog());
    }

    private IEnumerator PrintDialog()
    {
        while (currentDialogIndex < dialogList.Count)
        {
            DialogueString line = dialogList[currentDialogIndex];

            line.startDialogEvent?.Invoke();

            yield return StartCoroutine(TypeText(line.text));
            
            line.startDialogEvent?.Invoke();
        }
        DialogStop();
    }

    private IEnumerator TypeText(string text) 
    {
        dialogText.text = "";
        foreach(char letter in text.ToCharArray()) 
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        
        if(dialogList[currentDialogIndex].isEnd)
          DialogStop();  

          currentDialogIndex++;
    }

    private void DialogStop() 
    {
        StopAllCoroutines();
        dialogText.text = " ";
        dialogParent.SetActive(false);

        playerController.enabled = true;
        playerController.GetComponent<Rigidbody2D>().constraints = originalConstraints;
    }
}