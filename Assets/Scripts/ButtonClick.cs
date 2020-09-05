using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string targetScene;

    //loads a new scene based on the given string
    private void OnMouseDown()
    {
        //if the target scene is "Exit", it just quits
        if (targetScene == "Quit")
        {
            Application.Quit();
            Debug.Log("Game Quit"); //intentionally left after the quit command for editor debugging
        }
        else
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
