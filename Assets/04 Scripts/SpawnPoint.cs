using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class ObjectPool<T>where T : UnityEngine.Component
    {
        private T prefab;
        private Transform parentTransform;
        private int initialPoolSize;
        private Queue<T> objectPool = new Queue<T>();

        public ObjectPool(T prefab, Transform parentTransform, int initialPoolSize)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            this.initialPoolSize = initialPoolSize;

            // 초기 풀 크기만큼 객체를 생성하여 풀에 추가합니다.
            for (int i = 0; i < initialPoolSize; i++)
            {
                T obj = InstantiateObject();
                AddToPool(obj);
            }
        }

        public T GetObject()
        {
            if (objectPool.Count == 0)
            {
                // 풀이 비어있는 경우 새로운 객체를 생성하여 반환합니다.
                T obj = InstantiateObject();
                return obj;
            }

            // 풀에서 재사용 가능한 객체를 가져와 반환합니다.
            T pooledObject = objectPool.Dequeue();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            AddToPool(obj);
        }

        private T InstantiateObject()
        {
            T obj = Object.Instantiate(prefab, parentTransform);
            obj.gameObject.SetActive(false);
            return obj;
        }

        private void AddToPool(T obj)
        {
            objectPool.Enqueue(obj);
        }
    }

    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Monster monsterPrefab;
        private ObjectPool<Monster> monsterPool;

        private void Awake()
        {
            monsterPool = new ObjectPool<Monster>(monsterPrefab, this.transform, 50);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                var monster = monsterPool.GetObject();
                monster.transform.position = this.transform.position;
            }
                
        }
    }
}