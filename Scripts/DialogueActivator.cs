﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{

    public string[] lines;
    public bool isPerson = true;

    private bool canActivate = false;

    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1"))
        {
            DialogueManager.instance.ShowDialogue(lines, isPerson);
            canActivate = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
