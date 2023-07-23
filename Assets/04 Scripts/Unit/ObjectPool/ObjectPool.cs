using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public PoolObject prefab;
    public int size;
}

public class ObjectPool
{
    private Transform parentTransform;
    private Dictionary<string, Queue<PoolObject>> tagToQueuedictionary = new Dictionary<string, Queue<PoolObject>>();
    private Dictionary<string, Pool> tagToPoolDictionary = new Dictionary<string, Pool>();

    public ObjectPool(Pool[] pools, Transform parentTransform)
    {
        this.parentTransform = parentTransform;

        for (int i = 0; i < pools.Length; i++)
        {
            tagToPoolDictionary[pools[i].tag] = pools[i];
        }

        // 초기 풀 크기만큼 객체를 생성하여 풀에 추가합니다.
        for (int i = 0; i < pools.Length; i++)
        {
            tagToQueuedictionary[pools[i].tag] = new Queue<PoolObject>();
            for (int j = 0; j < pools[i].size; j++)
            {
                var obj = InstantiateObject(pools[i].prefab.gameObject).GetComponent<PoolObject>();
                AddToPool(pools[i].tag, obj);
            }
        }
    }

    public T GetObject<T>(string tag) => GetObject(tag).GetComponent<T>();

    public PoolObject GetObject(string tag)
    {
        Queue<PoolObject> queue = tagToQueuedictionary[tag];
        PoolObject obj = null;
        if (queue.Count == 0) obj = InstantiateObject(tagToPoolDictionary[tag].prefab.gameObject);
        else obj = queue.Dequeue(); obj.gameObject.SetActive(true);

        obj.pooler = this;
        obj.tag = tag;

        obj.OnObjectSpawn();

        return obj;
    }

    public void ReturnObject(string tag, PoolObject obj)
    {
        if (tagToQueuedictionary.ContainsKey(tag))
        {
            obj.gameObject.SetActive(false);
            AddToPool(tag, obj);
        }
    }

    public int GetCountActiveObject(string tag)
    {
        int count = 0;
        foreach(var obj in tagToQueuedictionary[tag])
        {
            if(obj.gameObject.activeSelf)
            {
                count++;
            }
        }

        return count;
    }

    private PoolObject InstantiateObject(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab, parentTransform).GetComponent<PoolObject>();
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void AddToPool(string tag, PoolObject obj)
    {
        tagToQueuedictionary[tag].Enqueue(obj);
    }
}