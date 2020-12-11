using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public Text dialogueText;
    public Text nameText;
    public GameObject dialogueBox;
    public GameObject nameBox;

    public string[] dialogueLines;
    public int currentLine;
    private bool justStarted = true;


    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (justStarted)
                {
                    justStarted = false;
                }
                else
                {
                    currentLine++;
                    if (currentLine >= dialogueLines.Length)
                    {
                        dialogueBox.SetActive(false);
                        GameManager.instance.dialogueActive = false;
                    }
                    else
                    {
                        CheckIfName();
                        dialogueText.text = dialogueLines[currentLine];
                    }
                }
            }
        }
    }

    public void ShowDialogue(string[] lines, bool isPerson)
    {
        dialogueLines = lines;
        currentLine = 0;
        justStarted = true;
        CheckIfName();
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        GameManager.instance.dialogueActive = true;
        nameBox.SetActive(isPerson);
    }

    public void CheckIfName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Substring(2);
            currentLine++;
        }
    }
}
