using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    // Vitesse de déplacement de l'obstacle vers la gauche
    public float speed = 5f;

    void Update()
    {
        // Déplace l'obstacle vers la gauche à chaque frame
        // Time.deltaTime garantit que le déplacement est fluide et indépendant du nombre de FPS
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Si l'obstacle sort de l'écran (à gauche), on le détruit pour libérer de la mémoire
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
