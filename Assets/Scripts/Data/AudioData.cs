using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Data", menuName = "Audio Data")]
public class AudioData : SerializedScriptableObject
{
    [SerializeField] private Dictionary<AudioType, ClipData> audioClips = new();

    // only exposes keys to force getting clips through the method "GetAudioClip"
    // it has extra checkings
    public IEnumerable<AudioType> AudioTypes => audioClips.Keys;
    
    public ClipData GetClipData(AudioType audioType)
    {
        if (audioClips.TryGetValue(audioType, out ClipData clipData))
        {
            if (clipData == null) Debug.LogError($"Key exists, but value isn't assigned {audioType}");    
            return clipData;
        }
        Debug.LogError($"Attempting to get a non registered audio type {audioType}");
        return null;
    }

    public AudioClip GetAudioClip(AudioType audioType) => GetClipData(audioType).audioClip;
}

[Serializable]
public class ClipData
{
    public AudioClip audioClip;
    public float volume = 1f;
    public bool loop = false;
    public bool playOnAwake = false;
    public bool isOneShot = false;
}
