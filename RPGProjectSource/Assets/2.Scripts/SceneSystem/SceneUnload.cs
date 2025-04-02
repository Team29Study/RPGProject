using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneUnload : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset sceneAsset;

    void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }
#endif

    [SerializeField, Header("Debug")] private string sceneName;

    [ContextMenu("Load Scene")]
    public void UnloadScene()
    {
        SceneLoader.UnloadSceneAsnyc(sceneName);
    }

    public void UnloadCurrentScene()
    {
        SceneLoader.UnloadSceneAsnyc(SceneManager.GetActiveScene().name);
    }
}
