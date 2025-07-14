using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int initialRoads = 5; // Jumlah jalan awal
    [SerializeField] private int coinsPerRoad = 3;
    [SerializeField] private int barrelsPerRoad = 2;


    private List<GameObject> activeRoads = new List<GameObject>();
    private float roadLength = 6.18f; // SESUAIKAN dengan panjang prefab jalanmu
    private float spawnZ = 0f;

    void Start()
    {
        // Buat jalan awal
        for (int i = 0; i < initialRoads; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        // Jika player sudah melewati batas, spawn jalan baru dan hapus yang lama
        if (playerTransform.position.z - roadLength > spawnZ - (initialRoads * roadLength))
        {
            SpawnRoad();
            DeleteOldestRoad();
        }
    }

    private void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, Vector3.forward * spawnZ, Quaternion.identity);
        activeRoads.Add(road);
        spawnZ += roadLength;

        // Spawn koin dan rintangan di jalan yang baru dibuat
        SpawnObjects(road.transform);
    }

    private void DeleteOldestRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }

    private void SpawnObjects(Transform roadTransform)
    {
        // Spawn Koin
        for (int i = 0; i < coinsPerRoad; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-4f, 4f), 1f, Random.Range(roadTransform.position.z - roadLength / 2, roadTransform.position.z + roadLength / 2));
            Instantiate(coinPrefab, randomPos, coinPrefab.transform.rotation);
        }

        // Spawn Barrel
        for (int i = 0; i < barrelsPerRoad; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(roadTransform.position.z - roadLength / 2, roadTransform.position.z + roadLength / 2));
            Instantiate(barrelPrefab, randomPos, barrelPrefab.transform.rotation);
        }
    }
}