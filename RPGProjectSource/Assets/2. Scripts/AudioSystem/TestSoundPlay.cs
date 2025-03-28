using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundPlay : MonoBehaviour
{
    [SerializeField] private SoundData soundData;

    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            SoundManager.Instance.CreateSoundBuilder().Play(soundData);
        }
    }
}
