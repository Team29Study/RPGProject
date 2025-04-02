using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneLoad : MonoBehaviour
{
    [SerializeField] LoadSceneMode loadMode;

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
    public void LoadScene()
    {
        SceneLoader.LoadSceneAsync(sceneName, loadMode);
    }
}
