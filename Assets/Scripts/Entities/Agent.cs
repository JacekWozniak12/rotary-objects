using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour, IPooledObject, IHealthChangedObject
{
    public Action<int> HealthChanged;
    public Action Death;

    [SerializeField] private int startingHealth = 3, currentHealth;

    // public void 

    public void Hit(int damage = 1)
    {
        currentHealth -= damage;


        if(currentHealth <= 0)
        {

        }
    }

    protected IEnumerator ProcedureRotation()
    {

    }

    protected IEnumerator ProcedureFire()
    {

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