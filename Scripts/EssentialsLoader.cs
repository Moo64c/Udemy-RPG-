using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameManager;
    public GameObject audioManager;

    void Start()
    {
        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if (PlayerController.instance == null)
        {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        if (AudioManager.instance == null)
        {
            AudioManager.instance = Instantiate(audioManager).GetComponent<AudioManager>();
        }
    }
}
