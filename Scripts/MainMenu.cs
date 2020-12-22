using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueButton;

    private void Start()
    {
        continueButton.SetActive(PlayerPrefs.HasKey("Current_Scene"));
    }

    public void Continue()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
