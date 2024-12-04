using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawnManager : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerUpPrefab;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI scoreTextInGame;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnRange = 9f;

    private int waveNumber = 1;

    private void Start()
    {
        SpawnEnemyWave(waveNumber);
        UpdateWaveText();
    }

    private void Update()
    {
        // Check if all enemies are defeated
        if (FindObjectsOfType<Enemy>().Length == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            UpdateWaveText();
        }
    }

    // Spawns a wave of enemies
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomSpawnPosition(), Quaternion.identity);
        }

        // Instantiate power-up with a Y offset of 1 unit
        Instantiate(powerUpPrefab, GenerateRandomSpawnPosition() + Vector3.up, Quaternion.identity);

    }

    // Generates a random position within spawn range
    private Vector3 GenerateRandomSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    // Updates the wave text in the UI
    private void UpdateWaveText()
    {
        if (scoreTextInGame != null)
        {
            scoreTextInGame.text = $"Wave: {waveNumber}";
        }
    }
}
