using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIManager instance;
    public UIManager Instance { get { return instance; } }

    private BaseUI baseUI;
    public BaseUI BaseUI
    {
        get { return baseUI; }
        private set { baseUI = value; } }

    private ShopUI shopUI;
    public ShopUI ShopUI
    {
        get { return shopUI; }
        private set { shopUI = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
