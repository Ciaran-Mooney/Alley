using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoreholeAudio : MonoBehaviour
{
    [HideInInspector]
    public AudioSource source;
    public BoreholeAudioType type;

    // Start is called before the first frame update
    void Awake()
    {
        source = this.GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    public void Stop()
    {
        source.Stop();
    }
}
