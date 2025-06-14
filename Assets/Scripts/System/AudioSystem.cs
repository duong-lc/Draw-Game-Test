using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class AudioSystem : IAudioService
{
    [SerializeField] private AudioData audioData;

    private Dictionary<AudioType, AudioSource> _audioSources = new();
    private AudioSource _oneShotAudioSource;
        
    public void Initialize()
    {
        _oneShotAudioSource = new GameObject("[OneShot_AudioSource]").AddComponent<AudioSource>();
        Debug.Log($"[OneShot_AudioSource] created");
        
        foreach (var key in audioData.AudioTypes)
        {
            var clipData = audioData.GetClipData(key);
            Assert.IsNotNull(clipData);

            if (clipData.isOneShot) continue;
            
            var audioComp = new GameObject("[AudioSource] " + key).AddComponent<AudioSource>();
            audioComp.playOnAwake = clipData.playOnAwake;
            audioComp.loop = clipData.loop;
            audioComp.clip = clipData.audioClip;

            if (_audioSources.TryAdd(key, audioComp) == false) 
                Debug.LogError($"Adding audio clip {key} to audio system, but it already exists.");
            else
                Debug.Log($"[AudioSource]  + {key} created");
        }
        
        Debug.Log("[AudioSystem] Initialized");
    }
    
    public void PlaySound(AudioType type)
    {
        if(_audioSources.TryGetValue(type, out var audioSource))
            audioSource.Play();
        else // fallback
            PlayOneShot(type);
    }

    public void PlayOneShot(AudioType type)
    {
        _oneShotAudioSource.PlayOneShot(audioData.GetAudioClip(type));
    }

    public void StopSound(AudioType type)
    {
        if (_audioSources.TryGetValue(type, out var audioSource)) {
            audioSource.Stop();
            return;
        }
        Debug.LogWarning($"Audio source not found type {type}");
    }

    public void StopAllOneShotSounds()
    {
        _oneShotAudioSource.Stop();
    }
}

public interface IAudioService
{
    void Initialize();
    void PlaySound(AudioType type);
    void StopSound(AudioType type);
    void StopAllOneShotSounds();
}

public enum AudioType
{
    Music,
    Button,
    Win,
    Lose,
    CountTick,
}