using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    GenerateBackgroundText background;
    string nextScene, endingDescription;

    void Start()
    {
        background = FindObjectOfType<GenerateBackgroundText>();
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    public void LoadScene(string name, string description)
    {
        if (!description.Equals(""))
        {
            endingDescription = description;
        }
        
        nextScene = name;
        Invoke(nameof(WaitLoadScene), 0.1f);
    }

    public void LoadScene(string name)
    {
        LoadScene(name, "");
    }

    void WaitLoadScene()
    {
        SceneManager.LoadSceneAsync(nextScene);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Ending"))
        {
            FindObjectOfType<TMP_Text>().text = endingDescription;
        }
        
        if (background != null)
        {
            if (scene.name.Equals("Entry") || scene.name.Equals("Ending"))
            {
                background.WaitSetColours(GenerateBackgroundText.Layouts.Entry);
                background.changesPerFrame = 200;
            }
            else if (scene.name.Equals("Prologue"))
            {
                background.WaitSetColours(GenerateBackgroundText.Layouts.Prologue);
                background.changesPerFrame = 100;
            }
            else if (scene.name.Equals("Main Scene"))
            {
                background.WaitSetColours(GenerateBackgroundText.Layouts.Main);
                background.changesPerFrame = 10;
            }
            else if (scene.name.Equals("Epilogue"))
            {
                background.WaitSetColours(GenerateBackgroundText.Layouts.Epilogue);
                background.changesPerFrame = 100;
            }   
        }

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("UI"))
        {
            i.GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}