using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    public IPoolingSystem PoolingSystem { get; protected set; }

    [SerializeField] private float timeBeforeDespawn = 10;
    [SerializeField] private Rigidbody rb;

    [Header("Dynamic")]
    [SerializeField] private bool fired;
    [SerializeField] private float timeFromSpawn;

    #region Unity Methods

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInParent<Agent>() is Agent agent)
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

    public static void Fire(Vector3 pos, Vector3 dir, float force)
    {
        if(GameLoopManager.I.Projectiles.GetObject() is not Projectile)
        {
            Debug.LogError("Wrong prefab in projectiles pool");
        }

        var proj = (Projectile) GameLoopManager.I.Projectiles.GetObject();
        proj.transform.position = pos;
        proj.Spawn();
        proj.rb.AddForce(dir.normalized * force);
    }

    #region Pooling System

    public void Init(IPoolingSystem poolingSystem)
    {
        PoolingSystem = poolingSystem;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        fired = true;
    }

    public void ReturnToPool()
    {
        PoolingSystem.ReturnToPool(this);
        fired = false;
        gameObject.SetActive(false);
    }

    #endregion
}