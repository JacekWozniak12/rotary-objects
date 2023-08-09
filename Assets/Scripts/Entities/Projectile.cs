using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    public IPoolingSystem PoolingSystem { get; protected set; }

    [SerializeField] private float timeBeforeDespawn = 10;

    [Header("Dynamic")]
    [SerializeField] private bool fired;
    [SerializeField] private float timeFromSpawn;

    #region Unity Methods

    private void OnCollisionEnter(Collision other)
    {
        if (other is Agent agent)
        {
            agent.Hit();
            ReturnToPool();
        }
    }

    private void FixedUpdate()
    {
        if (!fired)
        {
            return;
        }

        timeFromSpawn += Time.fixedDeltaTime;

        if (timeFromSpawn > timeBeforeDespawn)
        {
            ReturnToPool();
        }
    }

    #endregion

    #region Pooling System

    public void Init(IPoolingSystem poolingSystem)
    {

    }

    public void Spawn()
    {

    }

    public void ReturnToPool()
    {
        PoolingSystem.ReturnToPool(this);
        fired = false;
        gameObject.SetActive(false);
    }

    #endregion
}