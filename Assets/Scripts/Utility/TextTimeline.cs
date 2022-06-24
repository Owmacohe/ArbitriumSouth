using System;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTimeline : MonoBehaviour
{
    [SerializeField] bool isMainScene;
    [SerializeField] GameObject[] texts;

    int current;

    void Start()
    {
        foreach (GameObject i in texts)
        {
            if (!isMainScene)
            {
                i.SetActive(false);
            }
            
            if (!i.GetComponentInChildren<Button>())
            {
                i.GetComponent<TMP_Text>().text += "<?event>";
                i.GetComponent<TextAnimator>().onEvent += GoToNext;
            }
        }

        if (!isMainScene)
        {
            GoToNext();
        }
    }

    public void Reset()
    {
        current = 0;
        
        foreach (GameObject i in texts)
        {
            i.SetActive(false);
            
            if (!i.GetComponentInChildren<Button>())
            {
                i.GetComponent<TMP_Text>().text += "<?event>";
            }
        }

        GoToNext();
    }

    void GoToNext(string message = "event")
    {
        if (current < texts.Length)
        {
            texts[current].SetActive(true);

            current++;

            if (isMainScene && current > 1)
            {
                GoToNext();
            }
            
            if (texts[current-1].GetComponentInChildren<Button>())
            {
                GoToNext();
            }
        }
    }
}