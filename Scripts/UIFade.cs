using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    public static UIFade instance;

    public Image fadeScreen;
    public float fadeSpeed = 0.3f;

    public bool shouldFadeToBlack = false;
    public bool shouldFadeFromBlack = false;

    private void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        fadeScreen.color = new Color(0, 0, 0, 0);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (shouldFadeToBlack)
        {
            float newAlpha = Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, newAlpha);

            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            float newAlpha = Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, newAlpha);


            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeFromBlack = false;
        shouldFadeToBlack = true;
    }


    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
