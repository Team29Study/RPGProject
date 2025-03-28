using UnityEngine;

public class SoundBuilder
{
    private readonly SoundManager soundManager;

    private Vector3 position = Vector3.zero;
    private bool randomPitch;

    public SoundBuilder(SoundManager manager)
    {
        soundManager = manager;
    }

    public SoundBuilder WithPosition(Vector3 pos)
    {
        position = pos;

        return this;
    }

    public SoundBuilder WithRandomPitch()
    {
        randomPitch = true;

        return this;
    }

    public void Play(SoundData data)
    {
        if (data == null || !soundManager.CanPlaySound(data))
            return;

        var emitter = soundManager.Get();

        emitter.Initialize(data);

        emitter.transform.position = position;
        emitter.transform.parent = soundManager.transform;

        if (data.frequentSound)
        {
            emitter.Node = soundManager.frequentEmitters.AddLast(emitter);
        }

        emitter.Play();
    }
}
