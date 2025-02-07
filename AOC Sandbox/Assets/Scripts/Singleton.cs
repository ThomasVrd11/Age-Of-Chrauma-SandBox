using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton<T> - Generic Singleton Pattern for Unity
    * ==================================================
 *
 * This class provides a thread-safe, generic singleton pattern for MonoBehaviours.
 * It ensures that only one instance of a given class exists within the scene,
 * and persists across scene loads unless explicitly destroyed.
 *
 * Key Features:
 * - Lazy initialization with thread safety using a lock.
 * - Prevents duplicate instances by destroying extra ones.
 * - Automatically creates an instance if none is found in the scene.
 * - Supports optional initialization via the Init() method.
 * - Handles application quit to prevent instance creation post-termination.
 *
 * Usage:
 * - Inherit from Singleton<T> where T is your MonoBehaviour class.
 * - Override Init() for custom initialization logic.
 */

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance; // * Holds the singleton instance
    private static readonly object _instanceLock = new object(); // * Ensures thread safety during initialization
    private static bool _quitting = false; // * Prevents instance recreation after quitting

    public static T Instance
    {
        get
        {
            lock (_instanceLock) // * Prevents race conditions when accessing the instance
            {
                if (_instance == null && !_quitting)
                {
                    _instance = GameObject.FindFirstObjectByType<T>(); // * Try to find an existing instance
                    
                    if (_instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).ToString()); // * Create new instance if none exists
                        _instance = go.AddComponent<T>();
                        DontDestroyOnLoad(_instance.gameObject); // * Persist across scene loads
                    }
                }
                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = gameObject.GetComponent<T>(); // * Assign the instance to the current object
            DontDestroyOnLoad(_instance.gameObject); // * Ensure persistence across scenes
        }
        else if (_instance.GetInstanceID() != GetInstanceID()) // * Prevent duplicate instances
        {
            Destroy(gameObject);
            throw new System.Exception(string.Format("Instance of {0} already exists, removing {1}", GetType().FullName, ToString()));
        }
        Init(); // * Call optional initialization method
    }

    protected virtual void OnApplicationQuit()
    {
        _quitting = true; // * Prevents new instances from being created after quitting
    }

    protected virtual void Init() { } // * Optional method for custom initialization logic
}
