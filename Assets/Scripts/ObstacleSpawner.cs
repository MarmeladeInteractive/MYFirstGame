using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float spawnHeight = 2f;
    public float verticalVariation = 2f;  
    private float timer = 0f;

    void Update() {
        timer += Time.deltaTime;
        if (timer >= spawnRate) {
            float randomYOffset = Random.Range(-verticalVariation, verticalVariation);
            
            Instantiate(obstaclePrefab, new Vector3(20f +randomYOffset, spawnHeight, 0), Quaternion.identity);
            timer = 0f;
        }
    }
}
