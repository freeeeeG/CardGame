using UnityEngine;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    public static UIManager Instance;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
