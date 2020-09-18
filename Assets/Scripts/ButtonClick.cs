//***************************************************************************
//
//Design, programming, and art by: Owen Hellum
//Alpha "completed" as of 17/09/2020
//Visit Arbitrium South's itch.io page at: https://omch.itch.io/arbitrium
//
//***************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string targetScreen;

    //loads a new scene based on the given string
    private void OnMouseDown()
    {
        //if the target scene is "Exit", it just quits
        if (targetScreen == "Quit")
        {
            Application.Quit();
            Debug.Log("Game Quit"); //intentionally left after the quit command for editor debugging
        }
        else
        {
            SceneManager.LoadScene(targetScreen);
        }
    }
}
