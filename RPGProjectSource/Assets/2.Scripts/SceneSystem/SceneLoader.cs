using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static HashSet<string> activeScenes = new();

    public static event Action<Scene> OnSceneLoaded = delegate { };
    public static event Action<Scene> OnSceneUnloaded = delegate { };

    public static void LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        if (activeScenes.Contains(sceneName))
            return;

        InternalLoadSceneAsync(sceneName, mode).Forget();
    }

    public static void UnloadSceneAsnycTest(string sceneName)
    {
        UnloadSceneAsny(sceneName).Forget();
    }

    private static async UniTask InternalLoadSceneAsync(string sceneName, LoadSceneMode mode)
    {
        var op = SceneManager.LoadSceneAsync(sceneName, mode);

        op.allowSceneActivation = false;

        foreach (var iter in activeScenes)
        {
            await UnloadSceneAsny(iter);
        }

        await UniTask.WaitWhile(() => op.progress < 0.9f);

        op.allowSceneActivation = true;

        await UniTask.WaitUntil(() => op.isDone);

        if (mode == LoadSceneMode.Additive)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            OnSceneLoaded?.Invoke(SceneManager.GetSceneByName(sceneName));
            activeScenes.Add(sceneName);
        }
    }

    private static async UniTask UnloadSceneAsny(string sceneName)
    {
        var op = SceneManager.UnloadSceneAsync(sceneName);

        await op;

        OnSceneUnloaded?.Invoke(SceneManager.GetSceneByName(sceneName));
        activeScenes.Remove(sceneName);
    }
}
