using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] LoadSceneMode loadMode;
    
    [SerializeField, Header("Debug")] private string sceneName;

#if UNITY_EDITOR
    public SceneAsset sceneAsset;

    void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;    
    }
#endif

    [ContextMenu("Load Scene")]
    public void LoadScene()
    {
        SceneLoader.LoadSceneAsync(sceneName, loadMode);
    }
}
