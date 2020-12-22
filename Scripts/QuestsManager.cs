 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{

    public string[] questMarkerNames;
    public bool[] questMarkersComplete;

    public static QuestsManager instance;

    void Start()
    {
        instance = this;

        questMarkersComplete = new bool[questMarkerNames.Length];
    }

    private void Update()
    {
    }

    public int GetQuestNumber(string questName)
    {
        for (int index = 0; index < questMarkerNames.Length; index++)
        {
            if (questMarkerNames[index] == questName)
            {
                return index;
            }
        }

        Debug.LogError("Quest not found: " + questName);
        return 0;
    }

    public bool CheckQuestComplete(string questName)
    {
        int questNumber = GetQuestNumber(questName);
        if (questNumber != 0)
        {
            return questMarkersComplete[questNumber];
        }
        return false;
    }

    public void MarkQuestStatus(string questName, bool isComplete)
    {
        questMarkersComplete[GetQuestNumber(questName)] = isComplete;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        for (int index = 0; index < questObjects.Length; index++)
        {
            questObjects[index].CheckCompletion();
        }
    }

    public void SaveQuestData()
    {
        for (int index = 0; index < questMarkerNames.Length; index++)
        {
            PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[index], questMarkersComplete[index] ? 1 : 0);
        }
    }

    public void LoadQuestData()
    {
        for (int index = 0; index < questMarkerNames.Length; index++)
        {
           var prefsKey = "QuestMarker_" + questMarkerNames[index];
           questMarkersComplete[index] = PlayerPrefs.HasKey(prefsKey) && PlayerPrefs.GetInt(prefsKey) == 1;
        }
    }
}
