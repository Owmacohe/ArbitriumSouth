using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectButton : MonoBehaviour
{
    EventSystem es;

    void Start()
    {
        es = FindObjectOfType<EventSystem>();
    }

    public void Deselect()
    {
        es.SetSelectedGameObject(null);
    }
}