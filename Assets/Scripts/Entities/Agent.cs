using System.Collections;
using UnityEngine;
using System;

public class Agent : MonoBehaviour, IPooledObject, IHealthStateChanged
{
    public Action<int> HealthChanged { get; protected set; }
    public Action Death { get; protected set; }
    public IPoolingSystem PoolingSystem { get; protected set; }

    [SerializeField] private int startingHealth = 3, currentHealth;

    // public void 

    public void Hit(int damage = 1)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {

        }
    }

    protected IEnumerator ProcedureRotation()
    {
        yield return new WaitForSecond(0.1f);
    }

    protected IEnumerator ProcedureFire()
    {
        yield return new WaitForSecond(0.1f);
    }

    protected void OnDeath()
    {
        ReturnToPool();
    }


    #region Pooling System

    public void Init(IPoolingSystem poolingSystem)
    {

    }

    public void Spawn()
    {

    }

    public void ReturnToPool()
    {

    }

    #endregion
}