using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float spawnRate = 2.0f;
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        InvokeRepeating("SpawnBomb", 0f, spawnRate);
    }

    void SpawnBomb()
    {
        if (bombPrefab != null)
        {
            Vector2 spawnPosition = GetSpawnPositionAtTop();
            Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GetSpawnPositionAtTop()
    {
        // Get the camera's viewport bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Spawn position should be at the top edge of the camera view
        float spawnX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
        float spawnY = mainCamera.orthographicSize + 1; // Adjust as needed to position bombs slightly above the top edge

        return new Vector2(spawnX, spawnY);
    }
}
