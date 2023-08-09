using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PoolingSystem : MonoBehaviour, IPoolingSystem
{
    [Header("Settings")]
    [SerializeField] private GameObject poolingObject;
    [SerializeField] private int baseAmount;

    [Header("Dynamic")]
    [SerializeField] private Queue<IPooledObject> available;
    [SerializeField] private List<IPooledObject> all;

    public int CurrentAmount => available.Count;
    public int AllAmount => all.Count;

    private void OnValidate()
    {
        ObjectTypeCheck();
    }

    private void Awake()
    {
        available = new();
        all = new();

        if (!ObjectTypeCheck())
        {
            Debug.LogError($"{poolingObject.name} is empty or have improper gameObject", poolingObject);
            enabled = false;
            return;
        }

        AddPoolObjects(baseAmount);
    }

    public void AddPoolObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            InstantiatePoolObject();
        }
    }

    public void RemovePoolObjects(int amount)
    {
        for (int i = all.Count - amount - 1; i < all.Count; i++)
        {
            Destroy(all[i].gameObject);
        }

        all.RemoveRange(all.Count - amount - 1, amount);
        available.Clear();

        foreach (var a in all)
        {
            available.Enqueue(a);
        }
    }

    protected void InstantiatePoolObject()
    {
        GameObject current = Instantiate(poolingObject);
        current.name = current.name.Replace("(Clone)", "");

        var pooled = current.GetComponent<IPooledObject>();
        pooled.Init(this);
        all.Add(pooled);
        available.Enqueue(pooled);
    }

    public IPooledObject GetObject()
    {
        if (available.Count > 0)
        {
            return available.Dequeue();
        }
        else
        {
            InstantiatePoolObject();
            return available.Dequeue();
        }
    }

    public void ReturnToPool(IPooledObject pooledObject)
    {
        available.Enqueue(pooledObject);
    }

    private bool ObjectTypeCheck()
    {
        if (poolingObject != null && poolingObject.GetComponent<IPooledObject>() == null)
        {
            Debug.LogError($"{poolingObject.name} is not implementing IPooledObject interface", poolingObject);
            return false;
        }

        if (poolingObject == null)
        {
            return false;
        }

        return true;
    }
}