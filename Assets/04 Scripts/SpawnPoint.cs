using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    class ObjectPool
    {
        private Transform parentTransform;
        private int initialPoolSize;
        private Dictionary<string, Queue<GameObject>> tagToQueuedictionary = new Dictionary<string, Queue<GameObject>>();
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
                tagToQueuedictionary[pools[i].tag] = new Queue<GameObject>();
                for(int j=0; j < pools[i].size; j++)
                {
                    var obj = InstantiateObject(pools[i].prefab);
                    AddToPool(pools[i].tag, obj);
                }
            }
        }

        public T GetObject<T>(string tag, Vector3 position = default, Quaternion rotation = default) => GetObject(tag, position, rotation).GetComponent<T>();

        public GameObject GetObject(string tag, Vector3 position = default, Quaternion rotation = default)
        {
            Queue<GameObject> queue = tagToQueuedictionary[tag];
            GameObject obj = null;
            if (queue.Count == 0) obj = InstantiateObject(tagToPoolDictionary[tag].prefab);
            else obj = queue.Dequeue(); obj.gameObject.SetActive(true);

            obj.transform.position = position == default ? Vector3.zero : position;
            obj.transform.rotation = rotation == default ? Quaternion.identity : rotation;
            return obj;
        }

        public void ReturnObject(string tag, GameObject obj)
        {
            if (tagToQueuedictionary.ContainsKey(tag))
            {
                obj.gameObject.SetActive(false);
                AddToPool(tag, obj);
            }
        }

        private GameObject InstantiateObject(GameObject prefab)
        {
            GameObject obj = Object.Instantiate(prefab, parentTransform);
            obj.gameObject.SetActive(false);
            return obj;
        }

        private void AddToPool(string tag,GameObject obj)
        {
            tagToQueuedictionary[tag].Enqueue(obj);
        }
    }

    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Pool[] pools;
        private ObjectPool monsterPool;

        readonly string[] strs = { "BlueMob", "PuppleMob" };

        private void Awake()
        {
            monsterPool = new ObjectPool(pools, this.transform);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                monsterPool.GetObject(strs[Random.Range(0,strs.Length)]).transform.position = transform.position;
            }
                
        }
    }
}