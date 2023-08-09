using System;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject poolingObject;
    [SerializeField] private int baseAmount;

    [Header("Dynamic")]
    [SerializeField] private Queue<IPooledObject> available;
    [SerializeField] private List<IPooledObject> all;

    private void OnValidate()
    {
        ObjectTypeCheck();
    }

    private void Awake()
    {
        if (!ObjectTypeCheck())
        {
            Debug.LogError($"{poolingObject.name} is empty or have improper gameObject", poolingObject);
            enabled = false;
            return;
        }

        for (int i = 0; i < baseAmount; i++)
        {
        }
    }

    public IPooledObject GetObject()
    {
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