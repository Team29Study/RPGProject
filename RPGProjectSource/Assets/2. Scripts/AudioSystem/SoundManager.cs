using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundEmitter prefab;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int initialCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;
    [SerializeField] private int maxSoundInstance = 30;

    private IObjectPool<SoundEmitter> emitterPool;
    private readonly List<SoundEmitter> activeEmitters = new();
    public readonly LinkedList<SoundEmitter> frequentEmitters = new();

    public SoundBuilder Builder() => new SoundBuilder(this);

    void Start() => InitializeSoundPool();

    public bool CanPlaySound(SoundData data)
    {
        if (!data.frequentSound)
            return true;

        if (frequentEmitters.Count >= maxSoundInstance)
        {
            try
            {
                frequentEmitters.First.Value.Stop();
                return true;
            }
            catch
            {

            }

            return false;
        }

        return true;
    }

    public SoundEmitter Get() => emitterPool.Get();

    public void ReturnToPool(SoundEmitter emitter)
    {
        emitterPool.Release(emitter);
    }

    public void StopAll()
    {
        activeEmitters.ForEach(e => e.Stop());
        frequentEmitters.Clear();
    }

    private void InitializeSoundPool()
    {
        emitterPool = new ObjectPool<SoundEmitter>(
            CreateSoundEmitter,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPoolObject,
            collectionCheck,
            initialCapacity,
            maxPoolSize
            );
    }

    private SoundEmitter CreateSoundEmitter()
    {
        var soundEmitter = Instantiate(prefab);

        soundEmitter.OnSoundStop += ReturnToPool;
        soundEmitter.gameObject.SetActive(false);

        return soundEmitter;
    }

    private void OnTakeFromPool(SoundEmitter emitter)
    {
        emitter.gameObject.SetActive(true);
        activeEmitters.Add(emitter);
    }

    private void OnReturnToPool(SoundEmitter emitter)
    {
        if (emitter.Node != null)
        {
            frequentEmitters.Remove(emitter.Node);
            emitter.Node = null;
        }
        emitter.gameObject.SetActive(false);
        activeEmitters.Remove(emitter);
    }

    private void OnDestroyPoolObject(SoundEmitter emitter)
    {
        Destroy(emitter);
    }
}
