using System;
using Febucci.UI;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    Button butt;
    DeselectButton dsb;
    Inventory inv;
    
    bool hovering;

    void Start()
    {
        Check();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hovering)
        {
            butt.onClick.Invoke();
            butt.Select();
        }
    }

    void Check()
    {
        butt = GetComponent<Button>();
        dsb = FindObjectOfType<DeselectButton>();
        inv = FindObjectOfType<Inventory>();
    }

    public void StartHovering()
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

    public void StopHovering()
    {
        Check();
        
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

    void OnMouseOver()
    {
        if (!hovering)
        {
            StartHovering();   
        }
    }

    void OnMouseEnter()
    {
        Check();
    }

    void OnMouseExit()
    {
        StopHovering();
    }
}