using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicController : MonoBehaviour
{
    [SerializeField] float beatInterval = 3.5f;
    [SerializeField] AudioClip[] beats, fillers;

    AudioSource beatsSource, fillersSource;
    AudioClip lastBeat, lastFiller;

    void Start()
    {
        beatsSource = gameObject.AddComponent<AudioSource>();
        beatsSource.playOnAwake = false;
        
        fillersSource = gameObject.AddComponent<AudioSource>();
        fillersSource.playOnAwake = false;

        Beat();
    }

    void FixedUpdate()
    {
        if (!fillersSource.isPlaying && Random.Range(0, 150) == 0)
        {
            lastFiller = Play(fillersSource, fillers, lastFiller, Random.Range(0.1f, 0.25f));
        }
    }

    void Beat()
    {
        lastBeat = Play(beatsSource, beats, lastBeat, Random.Range(0.25f, 0.5f));
        Invoke(nameof(Beat), beatInterval);
    }

    AudioClip Play(AudioSource source, AudioClip[] clips, AudioClip lastClip, float volume)
    {
        AudioClip temp = clips[Random.Range(0, clips.Length)];

        while (temp.Equals(lastClip))
        {
            temp = clips[Random.Range(0, clips.Length)];
        }

        source.clip = temp;
        source.volume = volume;
        source.Play();

        return temp;
    }
}