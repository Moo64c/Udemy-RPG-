using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public string questToCheck;
    public bool activeIfComplete;

    private bool initialCheckDone = false;

    void Start()
    {
        CheckCompletion();
    }

    void Update()
    {
        if (!initialCheckDone)
        {
            initialCheckDone = true;
            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        if (QuestsManager.instance != null && QuestsManager.instance.CheckQuestComplete(questToCheck))
        {
            objectToActivate.SetActive(activeIfComplete);
        }
    }
}
