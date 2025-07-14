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
    private int roadsSpawned = 0; // Untuk menghitung jalan yang sudah di-spawn
    private float roadLength = 16f; // SESUAIKAN dengan panjang prefab jalanmu
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
        // Pengaman jika player atau jalan belum siap
        if (playerTransform == null || activeRoads.Count < 2) return;

        // Cek posisi mobil relatif terhadap jalan KEDUA di antrean.
        // Kita gunakan 'activeRoads[1]' (jalan kedua), bukan perhitungan matematis.
        // Kita gunakan '<' karena arah maju kita adalah ke sumbu X negatif.
        if (playerTransform.position.x < activeRoads[1].transform.position.x)
        {
            SpawnRoad();
            DeleteOldestRoad();
        }
    }

    private void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, new Vector3(-spawnZ, 0f, 0), Quaternion.Euler(-90, 0, 0));
        activeRoads.Add(road);
        spawnZ += roadLength;

        // Tambah penghitung setiap kali jalan dibuat
        roadsSpawned++;

        // HANYA panggil SpawnObjects jika sudah lebih dari 2 jalan yang dibuat
        if (roadsSpawned > 2)
        {
            SpawnObjects(road.transform);
        }
    }

    private void DeleteOldestRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }

    private void SpawnObjects(Transform roadTransform)
    {
        // Menentukan batas lebar jalan untuk spawn objek
        // Sesuaikan angka 3f jika jalan Anda lebih lebar atau sempit
        float roadWidth = 3f;

        // Spawn Koin
        for (int i = 0; i < coinsPerRoad; i++)
        {
            // Posisi X sekarang untuk panjang, dan Z untuk lebar
            Vector3 randomPos = new Vector3(
                Random.Range(roadTransform.position.x - roadLength / 2, roadTransform.position.x + roadLength / 2), // Acak di sepanjang jalan (sumbu X)
                0.2f, // Ketinggian koin dari jalan
                Random.Range(-roadWidth, roadWidth)  // Acak di lebar jalan (sumbu Z)
            );
            Instantiate(coinPrefab, randomPos, coinPrefab.transform.rotation);
        }

        // Spawn Barrel
        for (int i = 0; i < barrelsPerRoad; i++)
        {
            // Posisi X sekarang untuk panjang, dan Z untuk lebar
            // Turunkan nilai Y agar barel menapak di jalan
            Vector3 randomPos = new Vector3(
                Random.Range(roadTransform.position.x - roadLength / 2, roadTransform.position.x + roadLength / 2),
                0f, // Coba nilai ini
                Random.Range(-roadWidth, roadWidth)
            );
            Instantiate(barrelPrefab, randomPos, barrelPrefab.transform.rotation);
        }
    }
}