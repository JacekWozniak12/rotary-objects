using UnityEngine;
using UnityEngine.Assertions;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance;

    /// <summary>
    /// shorthand for instance
    /// </summary>
    public static T I => Instance;

    private void Awake()
    {
        Assert.IsNull(Instance, $"One or more object of type {typeof(T)}");
        Instance = this;
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        
    }
}