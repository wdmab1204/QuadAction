using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingRequest : MonoBehaviour
{
    [SerializeField]
    Pool[] pools;

    static ObjectPoolingRequest instance = null;
    ObjectPool objectPooler;

    private void Awake()
    {
        instance = this;
        objectPooler = new ObjectPool(pools, transform);
    }

    public static PoolObject SpawnObject(string tag)
    {
        return instance.objectPooler.GetObject(tag);
    }

    public static void ReturnObject(string tag, PoolObject obj)
    {
        instance.objectPooler.ReturnObject(tag, obj);
    }
}
