using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterStats[] playerStats;

    public bool gameMenuOpen = false;
    public bool dialogueActive = false;
    public bool fadingBetweenAreas = false;

    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
       PlayerController.instance.canMove = !(gameMenuOpen || dialogueActive || fadingBetweenAreas);
    }
}
