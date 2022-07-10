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
                TMP_Text temp = i.GetComponent<TMP_Text>();
                
                if (!isMainScene)
                {
                    temp.text = ChoiceController.SearchAndFormat(temp.text, false);
                }
                
                temp.text += "<?event>";
                i.GetComponent<TextAnimator>().onEvent += GoToNext;
            }
        }

        if (isMainScene)
        {
            ShowText(0);
        }
        else
        {
            GoToNext();   
        }
    }

    void GoToNext(string message = "event")
    {
        Invoke(nameof(StopAllHovering), 0.01f);
        
        if (isMainScene)
        {
            ShowAll();
            return;
        }
        
        if (current < texts.Length)
        {
            ShowText(current);

            current++;

            if (texts[current-1].GetComponentInChildren<Button>())
            {
                GoToNext();
            }
        }
    }

    void ShowText(int index)
    {
        TMP_Text temp = texts[index].GetComponent<TMP_Text>();
        string str = temp.text;
        
        temp.text = "";
        texts[index].SetActive(true);
        temp.text = str;
    }

    void ShowAll()
    {
        if (background != null)
        {
            background.WaitSetColours(GenerateBackgroundText.Layouts.Main);   
        }

        Invoke(nameof(StopAllHovering), 0.01f);
        
        for (int i = 0; i < texts.Length; i++)
        {
            if (!texts[i].GetComponentInChildren<Button>())
            {
                texts[i].GetComponent<TextAnimator>().onEvent -= GoToNext;
            }
            
            ShowText(i);
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