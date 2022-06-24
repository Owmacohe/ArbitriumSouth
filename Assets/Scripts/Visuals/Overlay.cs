using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Overlay : MonoBehaviour
{
    TMP_Text text;
    
    void Start()
    {
        text = GetComponent<TMP_Text>();

        HideOverlay();
    }

    void FixedUpdate()
    {
        if (Random.Range(0, 1500) == 0)
        {
            text.enabled = true;
            transform.localRotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
            
            Invoke(nameof(HideOverlay), Random.Range(1f, 2f));
        }
    }

    void HideOverlay()
    {
        text.enabled = false;
    }
}