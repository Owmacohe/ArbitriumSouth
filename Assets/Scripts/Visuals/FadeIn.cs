using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float wait = 3;
    [SerializeField] float speed = 1;
    
    TMP_Text[] texts;
    float[] textIntervals;
    
    Image[] images;
    float[] imageIntervals;

    int current;

    void Start()
    {
        speed = 150f / speed;
        
        texts = GetComponentsInChildren<TMP_Text>();
        textIntervals = new float[texts.Length];

        for (int i = 0; i < texts.Length; i++)
        {
            textIntervals[i] = texts[i].color.a / speed;
            
            Color temp = texts[i].color;
            texts[i].color = new Color(temp.r, temp.g, temp.b, 0);
        }
        
        images = GetComponentsInChildren<Image>();
        imageIntervals = new float[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            imageIntervals[i] = images[i].color.a / speed;
            
            Color temp = images[i].color;
            images[i].color = new Color(temp.r, temp.g, temp.b, 0);
        }
        
        Invoke(nameof(Fade), wait);
    }

    void Fade()
    {
        if (current < speed)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                Color temp = texts[i].color;
                texts[i].color = new Color(temp.r, temp.g, temp.b, temp.a + textIntervals[i]);
            }
            
            for (int i = 0; i < images.Length; i++)
            {
                Color temp = images[i].color;
                images[i].color = new Color(temp.r, temp.g, temp.b, temp.a + imageIntervals[i]);
            }
            
            current++;
            
            Invoke(nameof(Fade), 0.01f);
        }
    }
}