using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    public string transitionName;

    void Start()
    {
        if (PlayerController.instance && transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = transform.position;
        }

        if (UIFade.instance )
        {
            UIFade.instance.FadeFromBlack();
            GameManager.instance.fadingBetweenAreas = false;
        }
    }
}
