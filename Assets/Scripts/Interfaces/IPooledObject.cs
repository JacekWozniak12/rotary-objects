using UnityEngine;

public interface IPooledObject
{
    /// <summary>
    /// Unity shorthand
    /// </summary>
    GameObject gameObject { get; }
    
    /// <summary>
    /// System that created the object
    /// </summary>
    IPoolingSystem PoolingSystem { get; }

    /// <summary>
    /// What should happen when object is initialized within pool
    /// </summary>
    void Init(IPoolingSystem system);

    /// <summary>
    /// What should happen when object is taken from pool
    /// </summary>
    void Spawn();

    /// <summary>
    /// What should happen when object is returning to the pool
    /// </summary>
    void ReturnToPool();
}