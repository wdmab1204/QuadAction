using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Monster monsterPrefab;
        private IObjectPool<Monster> monsterPool;

        private void Awake()
        {
            monsterPool = new ObjectPool<Monster>(CreateMonster, OnGet, OnRelease, OnnDestroy, maxSize: 20);
        }

        private Monster CreateMonster()
        {
            Monster monster = Instantiate(monsterPrefab);
            monster.transform.position = this.transform.position;
            monster.SetPool(monsterPool);
            return monster;
        }

        private void OnGet(Monster monster)
        {
            monster.gameObject.SetActive(true);
            monster.transform.position = this.transform.position;
        }

        private void OnRelease(Monster monster)
        {
            monster.gameObject.SetActive(false);
        }

        private void OnnDestroy(Monster monster)
        {
            Destroy(monster.gameObject);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                monsterPool.Get();
            }
                
        }
    }
}