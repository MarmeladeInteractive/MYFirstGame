using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab;      // Le prefab de l'obstacle à faire apparaître
    public float spawnRate = 2f;           // Temps entre chaque apparition d'obstacle (en secondes)
    public float spawnHeight = 2f;         // Hauteur de base d'apparition des obstacles
    public float verticalVariation = 2f;   // Amplitude de variation aléatoire verticale

    [Header("Progression")]
    public float acceleration = 0.01f;     // Pourcentage d'accélération (ex. 0.01 = +1% à chaque obstacle)
    private float currentSpeed = -1f;      // Vitesse actuelle des obstacles (initialisée plus tard)
    private float timer = 0f;              // Compteur de temps pour le spawn

    void Update()
    {
        // On incrémente le temps depuis le dernier spawn
        timer += Time.deltaTime;

        // Quand le temps est écoulé, on spawn un obstacle
        if (timer >= spawnRate)
        {
            SpawnObstacle();      // Fait apparaître un nouvel obstacle
            timer = 0f;           // Réinitialise le timer

            // Accélère le rythme de spawn (les obstacles apparaissent plus vite)
            spawnRate *= 1f - acceleration;

            // Augmente la vitesse des prochains obstacles
            currentSpeed *= 1f + acceleration;
        }
    }

    // Méthode qui instancie un obstacle avec variation de position
    void SpawnObstacle()
    {
        // Applique une variation aléatoire verticale à la position de base
        float randomYOffset = Random.Range(-verticalVariation, verticalVariation);

        // Position finale du spawn (toujours à X = 20 pour l'apparition hors écran à droite)
        Vector3 spawnPos = new Vector3(20f + randomYOffset, spawnHeight, 0f);

        // Crée l'obstacle dans la scène
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Si c'est le premier obstacle, on récupère sa vitesse initiale depuis le prefab
        if (currentSpeed < 0f)
            currentSpeed = obstacle.GetComponent<ObstacleMovement>().speed;

        // On applique la vitesse actuelle (qui augmente à chaque spawn)
        obstacle.GetComponent<ObstacleMovement>().speed = currentSpeed;
    }
}
