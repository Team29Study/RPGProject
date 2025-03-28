using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

[Serializable]
public class SoundData
{
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;
    public bool isLoop;
    public bool isPlayOnAwake;
    public bool frequentSound;
    public bool isMute;
    public bool bypassEffects;
    public bool bypassListenerEffects;
    public bool bypassReverbZones;
    public int priority = 128;
    public float volume = 1f;
    public float pitch = 1f;
    public float panStereo;
    public float spatialBlend;
    public float reverbZoneMix = 1f;
    public float dopplerLevel = 1f;
    public float spread;

    public float minDistance = 1f;
    public float maxDistance = 500f;

    public bool ignoreListenerVolume;
    public bool ignoreListenerPause;

    public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
}
