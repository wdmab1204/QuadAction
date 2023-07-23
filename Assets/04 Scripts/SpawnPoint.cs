using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    const float spawnInterval = 0.7f;
    int count = 20;
    
    private void Start()
    {
        StartCoroutine("UpdateSpawning");
    }

    IEnumerator UpdateSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            var obj = ObjectPoolingRequest.SpawnObject("EnemyA");
            obj.transform.position = this.transform.position;
        }
    }
}