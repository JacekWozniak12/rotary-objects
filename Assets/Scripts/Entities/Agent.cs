using System.Collections;
using UnityEngine;
using System;

public class Agent : MonoBehaviour, IPooledObject
{
    public Action<int> HealthChanged { get; set; }
    public Action Death { get; set; }
    public Action Rotate { get; set; }
    public Action Show { get; set; }
    public Action Hide { get; set; }
    public IPoolingSystem PoolingSystem { get; protected set; }

    [SerializeField] private int startingHealth = ConstGameSettings.DEFAULT_HEALTH;
    [SerializeField] private Transform barrel;
    [SerializeField] private MeshRenderer[] renderers;
    [SerializeField] private Collider[] colliders;

    [Header("Dynamic")]
    [SerializeField] private int currentHealth;
    [SerializeField] private bool wasHit;

    private void Reset()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void Hit(int damage = 1)
    {
        if (wasHit)
        {
            return;
        }

        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Death?.Invoke();
            ReturnToPool();
            return;
        }

        Hide?.Invoke();
        wasHit = true;
    }

    // Prolly Enable/Disable is not the best way but it suffices for now
    private void OnEnable()
    {
        StartCoroutine(ProcedureFire());
        StartCoroutine(ProcedureRotation());
        StartCoroutine(ProcedureHide());
        GameLoopManager.I.GameFinished += ReturnToPool;
    }

    private void OnDisable()
    {
        GameLoopManager.I.GameFinished -= ReturnToPool;
        StopCoroutine(ProcedureFire());
        StopCoroutine(ProcedureRotation());
        StopCoroutine(ProcedureHide());
    }

    protected IEnumerator ProcedureRotation()
    {
        while (enabled)
        {
            float randomNumber = UnityEngine.Random.Range(ConstGameSettings.DELAY_MIN_ROTATION, 1f);
            yield return new WaitForSeconds(ConstGameSettings.DELAY_MAX_ROTATION);

            transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
            Rotate?.Invoke();
        }
    }

    protected IEnumerator ProcedureFire()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(ConstGameSettings.DELAY_FIRE);

            if (wasHit)
            {
                continue;
            }

            Projectile.Fire(barrel.position, transform.forward, ConstGameSettings.PROJECTILE_FORCE);
        }
    }

    protected IEnumerator ProcedureHide()
    {
        while (enabled)
        {
            yield return new WaitUntil(() => wasHit);

            foreach (var mr in renderers)
            {
                mr.enabled = false;
            }

            foreach (var c in colliders)
            {
                c.enabled = false;
            }

            yield return new WaitForSeconds(ConstGameSettings.DELAY_HIT_RESPAWN);

            foreach (var mr in renderers)
            {
                mr.enabled = true;
            }

            foreach (var c in colliders)
            {
                c.enabled = true;
            }

            GameLoopManager.I.SetObjectWithinMargins(transform);
            wasHit = false;
            Show?.Invoke();
        }
    }

    #region Pooling System

    public void Init(IPoolingSystem poolingSystem)
    {
        PoolingSystem = poolingSystem;
    }

    public void Spawn()
    {
        currentHealth = startingHealth;
        gameObject.SetActive(true);
        HealthChanged?.Invoke(currentHealth);
        Show?.Invoke();
    }

    public void ReturnToPool()
    {
        Hide?.Invoke();
        Death = null;
        HealthChanged = null;
        wasHit = false;
        PoolingSystem.ReturnToPool(this);
        gameObject.SetActive(false);
    }

    #endregion
}