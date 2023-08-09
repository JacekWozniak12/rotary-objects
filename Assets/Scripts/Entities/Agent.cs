using System.Collections;
using UnityEngine;
using System;

public class Agent : MonoBehaviour, IPooledObject, IHealthStateChanged
{
    public Action<int> HealthChanged { get; set; }
    public Action Death { get; set; }
    public IPoolingSystem PoolingSystem { get; protected set; }

    [SerializeField] private int startingHealth = 3, currentHealth;

    // public void 

    public void Hit(int damage = 1)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death?.Invoke();
            ReturnToPool();
        }
    }

    // Prolly Enable/Disable is not the best way but it suffices for now
    private void OnEnable()
    {
        StartCoroutine(ProcedureFire());
        StartCoroutine(ProcedureRotation());
    }

    private void OnDisable()
    {
        StopCoroutine(ProcedureFire());
        StopCoroutine(ProcedureRotation());
    }

    protected IEnumerator ProcedureRotation()
    {
        while (enabled)
        {
            float randomNumber = UnityEngine.Random.Range(0, 1f);
            yield return new WaitForSeconds(randomNumber);
            transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
        }
    }

    protected IEnumerator ProcedureFire()
    {
        while (enabled)
        {
            float randomNumber = UnityEngine.Random.Range(0, 1f);
            yield return new WaitForSeconds(randomNumber);
            Projectile.Fire(transform.forward, 75f);
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
    }

    public void ReturnToPool()
    {
        Death = null;
        HealthChanged = null;
        PoolingSystem.ReturnToPool(this);
        gameObject.SetActive(false);
    }

    #endregion
}