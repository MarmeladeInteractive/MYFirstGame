using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float spawnHeight = 2f;
    public float verticalVariation = 2f;

    [Header("Progression")]
    public float acceleration = 0.01f;
    private float currentSpeed = -1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0f;

            spawnRate *= 1f - acceleration;
            currentSpeed *= 1f + acceleration;
        }
    }

    void SpawnObstacle()
    {
        float randomYOffset = Random.Range(-verticalVariation, verticalVariation);
        Vector3 spawnPos = new Vector3(20f, spawnHeight + randomYOffset, 0f);

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        if (currentSpeed < 0f)currentSpeed = obstacle.GetComponent<ObstacleMovement>().speed;

        obstacle.GetComponent<ObstacleMovement>().speed = currentSpeed;
    }
}
