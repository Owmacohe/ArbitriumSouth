using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClickSound : MonoBehaviour
{
    AudioSource source;
    AudioClip click;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("click");
        source.volume = 0.5f;
        source.playOnAwake = false;
    }

    public void Click()
    {
        source.pitch = Random.Range(0.3f, 1.5f);
        source.Play();
    }
}