using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    void OnMouseDown()
    {
        switch (gameObject.tag)
        {
            case "Start":
                SceneManager.LoadScene("Prologue");
                break;
            case "Exit":
                Application.Quit();
                break;
            case "Settings":
                SceneManager.LoadScene("Settings");
                break;
        }
    }
}
