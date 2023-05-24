using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Overlay : MonoBehaviour
{
    [SerializeField]
    AudioSource staticSound;
    [SerializeField]
    float staticSpeed = 1.5f;
    [SerializeField]
    Vector2 volumeBounds = new Vector2(0.2f, 0.8f);
    [SerializeField]
    Vector2 pitchBounds = new Vector2(1, 1.5f);
    
    TMP_Text text;
    bool isShowing, isMoving, isStaticUp;
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
        if (!isShowing && Random.Range(0, 300) == 0)
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
            
            isStaticUp = true;
            VolumeUp();
            PitchUp();
            
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
        
        isStaticUp = false;
        VolumeDown();
        PitchDown();
    }

    void VolumeUp()
    {
        if (isStaticUp && staticSound.volume < volumeBounds.y)
        {
            staticSound.volume += 0.1f * staticSpeed;

            if (staticSound.volume > volumeBounds.y)
            {
                staticSound.volume = volumeBounds.y;
            }
            else
            {
                Invoke(nameof(VolumeUp), 0.1f);
            }
        }
    }
    
    void PitchUp()
    {
        if (isStaticUp && staticSound.pitch < pitchBounds.y)
        {
            staticSound.pitch += 0.1f * staticSpeed;

            if (staticSound.pitch > pitchBounds.y)
            {
                staticSound.pitch = pitchBounds.y;
            }
            else
            {
                Invoke(nameof(PitchUp), 0.1f);
            }
        }
    }
    
    void VolumeDown()
    {
        if (!isStaticUp && staticSound.volume > volumeBounds.x)
        {
            staticSound.volume -= 0.1f * staticSpeed;
            
            if (staticSound.volume < volumeBounds.x)
            {
                staticSound.volume = volumeBounds.x;
            }
            else
            {
                Invoke(nameof(VolumeDown), 0.1f);
            }
        }
    }
    
    void PitchDown()
    {
        if (!isStaticUp && staticSound.pitch > pitchBounds.x)
        {
            staticSound.pitch -= 0.1f * staticSpeed;
            
            if (staticSound.pitch < pitchBounds.x)
            {
                staticSound.pitch = pitchBounds.x;
            }
            else
            {
                Invoke(nameof(PitchDown), 0.1f);
            }
        }
    }
}