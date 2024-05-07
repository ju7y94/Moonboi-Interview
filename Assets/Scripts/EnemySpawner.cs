using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int poolSize = 10;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float minX = -5f;
    [SerializeField] float maxX = 5f;
    [SerializeField] float minZ = -5f;
    [SerializeField] float maxZ = 5f;

    private List<GameObject> objectPool;
    private float lastSpawnTime;

    void Start()
    {
        // Create object pool
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectToSpawn);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        // Check if it's time to spawn a new object
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnObject();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnObject()
    {
        // Get an inactive object from the pool
        GameObject obj = GetInactiveObject();
        if (obj != null)
        {
            // Randomly select position within specified range
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            obj.transform.position = new Vector3(x, 0f, z);
            obj.SetActive(true);
        }
    }

    GameObject GetInactiveObject()
    {
        // Find an inactive object from the pool
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}