using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string targetScene;

    //loads a new scene based on the given string
    void OnMouseDown()
    {
        //if the target scene is "Exit", it just quits
        if (targetScene == "Exit")
        {
            Application.Quit();
            Debug.Log("Game Quit"); //intentionally left after the quit command
        }

        SceneManager.LoadScene(targetScene);
    }
}
