using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Overlay : MonoBehaviour
{
    TMP_Text text;
    bool isShowing, isMoving;
    Vector3 dir;
    CRTPostProcess post;
    
    void Start()
    {
        text = GetComponent<TMP_Text>();
        post = FindObjectOfType<CRTPostProcess>();

        HideOverlay();
    }

    void FixedUpdate()
    {
        if (!isShowing && Random.Range(0, 1200) == 0)
        {
            transform.localPosition = GetRandomVector3() * 100f;
            
            text.enabled = true;
            transform.localRotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));

            float overlayTime = Random.Range(1f, 2f);
            
            if (Random.Range(0, 2) == 0)
            {
                Invoke(nameof(StartMoving), overlayTime / 3f);
            }

            isShowing = true;
            
            Invoke(nameof(HideOverlay), overlayTime);
        }

        if (isShowing)
        {
            post.blueOffset.x = Random.Range(0f, 0.01f);
        }

        if (isMoving)
        {
            transform.localPosition += (dir - transform.localPosition).normalized / Random.Range(1f, 5f);
        }
    }

    Vector3 GetRandomVector3()
    {
        return new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        );
    }

    void StartMoving()
    {
        dir = GetRandomVector3() * 100f;
        isMoving = true;
    }

    void HideOverlay()
    {
        text.enabled = false;
        post.blueOffset.x = 0;
        
        isShowing = false;
        isMoving = false;
    }
}