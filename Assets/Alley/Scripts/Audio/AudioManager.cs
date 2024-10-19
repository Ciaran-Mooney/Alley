using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private Dictionary<BoreholeAudioType, AudioRuntimeData> audioDataDictionary;

    private void Start()
    {
        audioDataDictionary = new();

        Instance = this;

        foreach(Transform t in this.transform)
        {
            var audio = t.GetComponent<BoreholeAudio>();
            if(audio != null)
            {
                audioDataDictionary.Add(audio.type, new AudioRuntimeData()
                {
                    audioSource = audio,
                    originalVolume = audio.source.volume
                });
            }
        }
    }

    public void Play(BoreholeAudioType type)
    {
        if(!audioDataDictionary.TryGetValue(type, out AudioRuntimeData audioData))
        {
            return;
        }

        if(audioData.fadeOutCoroutine != null)
        {
            audioData.isCancellationRequested = true;
            audioData.audioSource.source.volume = audioData.originalVolume;
        }

        audioData.audioSource.Play();
    }

    public void Stop(BoreholeAudioType type, float secondsTail = 1)
    {
        if (!audioDataDictionary.TryGetValue(type, out AudioRuntimeData audioData))
        {
            return;
        }

        if (!audioData.audioSource.source.isPlaying)
        {
            return;
        }

        if (audioData.fadeOutCoroutine != null)
        {
            return;
        }



        audioData.fadeOutCoroutine = StartCoroutine("StopAudioWithTail", new Tuple<BoreholeAudioType,float>(type, secondsTail));
        audioDataDictionary[type] = audioData;
    }

    private IEnumerator StopAudioWithTail(Tuple<BoreholeAudioType,float> tuple)
    {
        if(!audioDataDictionary.TryGetValue(tuple.Item1, out AudioRuntimeData audioData))
        {
            throw new Exception("Error retrieving audio source");
        }

        float elapsedTime = 0f;
        float originalVolume = audioData.audioSource.source.volume;

        while(audioData.audioSource.source.volume > 0)
        {
            if (audioData.isCancellationRequested)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(originalVolume, 0, elapsedTime / tuple.Item2);
            audioData.audioSource.source.volume = newVolume;
            yield return null; // Wait for the next frame
        }

        audioData.audioSource.Stop();
        audioData.audioSource.source.volume = originalVolume;
        audioData.fadeOutCoroutine = null;
        audioDataDictionary[tuple.Item1] = audioData;
    }
}

public class AudioRuntimeData
{
    public BoreholeAudio audioSource;
    public float originalVolume;
    public bool isCancellationRequested = false;
    public Coroutine fadeOutCoroutine;
}

public enum BoreholeAudioType
{
    PulleyY,
    Hooking,
    Drill,
    PulleyXZ
}
