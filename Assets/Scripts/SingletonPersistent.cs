using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    Debug.LogError(typeof(T).ToString() + " is not found.");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Debug.Log(typeof(T).ToString() + " Singleton instance created.");
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogWarning(typeof(T).ToString() + " Singleton instance already exists. Destroying this new instance.");
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This method is meant to be overridden by subclasses
    }
}