using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float spawnInterval = 0.7f;
    public string tag;
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
            var obj = ObjectPoolingRequest.SpawnObject(tag);
            obj.transform.position = this.transform.position;
        }
    }
}