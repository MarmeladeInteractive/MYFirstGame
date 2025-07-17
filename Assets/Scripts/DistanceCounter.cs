using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Nécessaire pour utiliser TextMeshProUGUI

public class DistanceCounter : MonoBehaviour
{
    // Référence au composant TextMeshProUGUI qui affichera la distance à l'écran
    public TextMeshProUGUI distanceText;

    // Distance parcourue (en mètres)
    public float distance = 0f;

    // Vitesse fictive à laquelle avance le joueur (en m/s)
    public float speed = 5f;

    // Indique si le jeu est terminé (pour stopper le compteur)
    private bool isGameOver = false;

    void Update()
    {
        // Si le jeu est terminé, on ne met plus à jour la distance
        if (isGameOver) return;

        // On augmente la distance en fonction de la vitesse et du temps écoulé depuis la dernière image
        distance += speed * Time.deltaTime;

        // Mise à jour du texte affiché à l'écran avec la distance arrondie à l'entier
        distanceText.text = "Distance : " + Mathf.FloorToInt(distance) + " m";
    }

    // Appelé depuis le GameManager pour arrêter le compteur à la fin du jeu
    public void StopCounter()
    {
        isGameOver = true;
    }
}
