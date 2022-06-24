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
        source.pitch = Random.Range(-3f, 3f);
        source.Play();
    }
}