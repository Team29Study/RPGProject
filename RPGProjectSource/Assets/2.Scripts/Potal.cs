using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TriggerArea))]
public class Potal : MonoBehaviour
{
    private TriggerArea triggerArea;
    private SceneLoad loader;

    public UnityEvent OnPotal;

    void Awake()
    {
        triggerArea = GetComponent<TriggerArea>();
        loader = GetComponent<SceneLoad>();
    }

    void OnEnable() => triggerArea.OnTrigger += (col) => OnPotal.Invoke();

}
