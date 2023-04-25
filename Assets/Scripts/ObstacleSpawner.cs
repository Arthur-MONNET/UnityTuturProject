using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject pointPrefab;

    [Header("Timers")]
    [SerializeField] float timeToSpawn = 2f;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] float obstacleCountIncreasePerWave = 0.2f;
    
    private float nextWaveTime = 0f;
    private int waveCount = 0;

    private void Start()
    {
        nextWaveTime = Time.time + timeToSpawn;
    }

    private void Update()
    {
        if(Time.time >= nextWaveTime)
        {
            waveCount++;
            spawnObstacles(Random.Range(-1, 1) + (int)(Mathf.Round(waveCount * obstacleCountIncreasePerWave)) + 1);
            nextWaveTime = Time.time + timeBetweenWaves;
        }
    }

    void spawnObstacles(int obstacleCount)
    {
        if( obstacleCount > spawnPoints.Length - 5)
        {
            obstacleCount = spawnPoints.Length - 5;
        }
        // liste d'index des spawnPoints
        List<int> spawnPointsIndex = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointsIndex.Add(i);
        }
        // on retire aléatoirement des index de la liste jusqu'à ce qu'il n'en reste plus que obstacleCount
        while (spawnPointsIndex.Count > obstacleCount)
        {
            spawnPointsIndex.RemoveAt(Random.Range(0, spawnPointsIndex.Count));
        }

        // on instancie un nombre de points aléatoire entre 0 et 4 (en faisant in rdm entre 0 et 2 et en mettant au carré puis en arrondissant) et on les place aux positions des spawnPoints dont les index ne sont pas dans la liste
        int pointCount = (int)Mathf.Round(Mathf.Pow(Random.Range(0f, 2f), 2));
        Debug.Log(pointCount);
        List<int> pointSpawnPointsIndex = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPointsIndex.Contains(i))
            {
                pointSpawnPointsIndex.Add(i);
            }
        }

        while (pointSpawnPointsIndex.Count > pointCount)
        {
            pointSpawnPointsIndex.RemoveAt(Random.Range(0, pointSpawnPointsIndex.Count));
        }

        for (int i = 0; i < pointSpawnPointsIndex.Count; i++)
        {
            Instantiate(pointPrefab, spawnPoints[pointSpawnPointsIndex[i]].position, Quaternion.identity);
        }

        // on instancie les obstacles aux positions des spawnPoints dont les index sont dans la liste
        for (int i = 0; i < spawnPointsIndex.Count; i++)
        {
            float randomScale = Random.Range(0.5f, 1.0f);
            Vector3 randomScaleVector = new Vector3(randomScale*2.5f, randomScale*4f, randomScale * 2.5f);
            Instantiate(obstaclePrefab, spawnPoints[spawnPointsIndex[i]].position, Quaternion.identity).transform.localScale = randomScaleVector;
        }
    }
}