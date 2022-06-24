using System;
using System.Collections;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    Button butt;
    DeselectButton dsb;
    Inventory inv;
    
    bool hovering;

    void Start()
    {
        butt = GetComponent<Button>();
        dsb = FindObjectOfType<DeselectButton>();
        inv = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hovering)
        {
            butt.onClick.Invoke();
            butt.Select();
        }
    }

    void OnMouseEnter()
    {
        hovering = true;
        butt.Select();

        if (inv != null && transform.IsChildOf(inv.transform))
        {
            inv.up = true;
        }

        GameObject temp = butt.transform.parent.gameObject;

        if (temp.CompareTag("South"))
        {
            temp.GetComponent<TextAnimator>().effectIntensityMultiplier = 50;
        }
    }
    
    void OnMouseExit()
    {
        hovering = false;
        dsb.Deselect();
        
        if (inv != null && transform.IsChildOf(inv.transform))
        {
            inv.up = false;
        }
        
        GameObject temp = butt.transform.parent.gameObject;

        if (temp.CompareTag("South"))
        {
            temp.GetComponent<TextAnimator>().effectIntensityMultiplier = 0;
        }
    }
}