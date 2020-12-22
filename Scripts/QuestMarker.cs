using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{

    public string questToMark;
    public bool marksQuestAsComplete = true;
    public bool markOnEnter = true;
    public bool deactivateOnMark = true;
    
    private bool canMark;

    public void MarkQuest()
    {
        QuestsManager.instance.MarkQuestStatus(questToMark, marksQuestAsComplete);

        gameObject.SetActive(!deactivateOnMark);
    }

    private void Update()
    {
        if (canMark && Input.GetButtonDown("Fire1"))
        {
            MarkQuest();
            canMark = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (markOnEnter)
            {
                MarkQuest();
            }
            else
            {
                canMark = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canMark = false;
        }
    }
}
