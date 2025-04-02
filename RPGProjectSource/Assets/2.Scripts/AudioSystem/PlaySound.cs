using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private SoundData soundData;

    void Start() => Play();

    public void Play() => SoundManager.Instance.Builder().Play(soundData);
}
