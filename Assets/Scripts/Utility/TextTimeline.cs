using System;
using System.Collections;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTimeline : MonoBehaviour
{
    [SerializeField] bool isMainScene;
    [SerializeField] GameObject[] texts;

    int current;
    GenerateBackgroundText background;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        if (isMainScene)
        {
            if (background == null)
            {
                background = FindObjectOfType<GenerateBackgroundText>();
            }
            
            if (background != null)
            {
                background.WaitSetColours(GenerateBackgroundText.Layouts.FullMain); 
            }
        }

        current = 0;
        
        foreach (GameObject i in texts)
        {
            i.SetActive(false);
            
            if (!i.GetComponentInChildren<Button>())
            {
                i.GetComponent<TMP_Text>().text += "<?event>";
                i.GetComponent<TextAnimator>().onEvent += GoToNext;
            }
        }

        GoToNext();
    }

    void GoToNext(string message = "event")
    {
        Invoke(nameof(StopAllHovering), 0.01f);
        
        if (current < texts.Length)
        {
            texts[current].SetActive(true); // TODO: this flickers before the TextAnimator picks it up and starts typing

            current++;

            if (isMainScene && current > 1)
            {
                ShowAll();
                return;
            }
            
            if (!isMainScene && texts[current-1].GetComponentInChildren<Button>())
            {
                GoToNext();
            }
        }
    }

    void ShowAll()
    {
        if (background != null)
        {
            background.WaitSetColours(GenerateBackgroundText.Layouts.Main);   
        }

        Invoke(nameof(StopAllHovering), 0.01f);
        
        foreach (GameObject i in texts)
        {
            if (!i.GetComponentInChildren<Button>())
            {
                i.GetComponent<TextAnimator>().onEvent -= GoToNext;
            }
            
            i.SetActive(true);
        }
    }

    void StopAllHovering()
    {
        foreach (ButtonHover i in FindObjectsOfType<ButtonHover>())
        {
            i.StopHovering();
        }
    }
}