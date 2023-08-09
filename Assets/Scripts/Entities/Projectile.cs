using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    public IPoolingSystem PoolingSystem { get; protected set; }

    [SerializeField] private float timeBeforeDespawn = 10;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer tr;

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

        if (!enabled)
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
        var proj = (Projectile)GameLoopManager.I.Projectiles.GetObject();
        Debug.Log(proj);
        proj.transform.position = pos;
        proj.transform.forward = dir;
        proj.Spawn();
        proj.rb.AddForce(dir * force, ForceMode.Impulse);
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
        tr.enabled = true;
        tr.emitting = true;
    }

    public void ReturnToPool()
    {
        timeFromSpawn = 0;
        tr.emitting = false;
        tr.enabled = false;
        gameObject.SetActive(false);
        PoolingSystem.ReturnToPool(this);
        fired = false;
    }

    #endregion
}