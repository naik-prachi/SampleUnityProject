using System.Collections;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;  // Reference to the bomb prefab
    public Vector2 spawnAreaMin;   // Bottom-left corner of the spawn area
    public Vector2 spawnAreaMax;   // Top-right corner of the spawn area
    public float spawnInterval = 2f;  // Interval between bomb spawns in seconds
    public int bombsPerInterval = 2;  // Number of bombs to spawn every interval

    void Start()
    {
        // Start the coroutine to spawn bombs continuously
        StartCoroutine(SpawnBombsOverTime());
    }

    // Coroutine to spawn bombs repeatedly
    IEnumerator SpawnBombsOverTime()
    {
        while (true)  // Infinite loop for continuous spawning
        {
            // Spawn the specified number of bombs
            for (int i = 0; i < bombsPerInterval; i++)
            {
                SpawnBomb();
            }

            // Wait for the specified interval before spawning again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Function to spawn a bomb at a random position within the spawn area
    void SpawnBomb()
    {
        // Generate a random position within the defined spawn area
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instantiate the bomb at the random position with no rotation
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }
}