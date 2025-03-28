using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private Coroutine playingCoroutine;

    public SoundData Data { get; private set; }
    public LinkedListNode<SoundEmitter> Node { get; set; }

    public event Action<SoundEmitter> OnSoundPlay = delegate { };
    public event Action<SoundEmitter> OnSoundStop = delegate { };

    void Awake() => audioSource = GetComponent<AudioSource>();

    public void Initialize(SoundData data)
    {
        Data = data;

        audioSource.clip = data.clip;
        audioSource.outputAudioMixerGroup = data.mixerGroup;
        audioSource.loop = data.isLoop;
        audioSource.playOnAwake = data.isPlayOnAwake;

        audioSource.mute = data.isMute;
        audioSource.bypassEffects = data.bypassEffects;
        audioSource.bypassListenerEffects = data.bypassListenerEffects;
        audioSource.bypassReverbZones = data.bypassReverbZones;

        audioSource.priority = data.priority;
        audioSource.volume = data.volume;
        audioSource.pitch = data.pitch;
        audioSource.panStereo = data.panStereo;
        audioSource.spatialBlend = data.spatialBlend;
        audioSource.reverbZoneMix = data.reverbZoneMix;
        audioSource.dopplerLevel = data.dopplerLevel;
        audioSource.spread = data.spread;

        audioSource.minDistance = data.minDistance;
        audioSource.maxDistance = data.maxDistance;

        audioSource.ignoreListenerVolume = data.ignoreListenerVolume;
        audioSource.ignoreListenerPause = data.ignoreListenerPause;

        audioSource.rolloffMode = data.rolloffMode;
    }

    public void Play()
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
        }

        audioSource.Play();
        StartCoroutine(WaitForSoundToEnd());
        OnSoundPlay?.Invoke(this);
    }

    public void Stop()
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine = null;
        }

        audioSource.Stop();
        OnSoundStop?.Invoke(this);
    }

    private IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        Stop();
    }
}
