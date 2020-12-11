using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public AreaEntrance theEntrance;
    public string areaTransitionName;
    public string areaToLoad;
    public float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    void Start()
    {
        theEntrance.transitionName = areaTransitionName;
    }

    private void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0f)
            {
                SceneManager.LoadScene(areaToLoad);
                shouldLoadAfterFade = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            shouldLoadAfterFade = true;
            UIFade.instance.FadeToBlack();
            PlayerController.instance.areaTransitionName = areaTransitionName;
            GameManager.instance.fadingBetweenAreas = true;
        }
    }
}
