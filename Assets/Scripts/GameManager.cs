using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Pour recharger la scène
using TMPro; // Pour gérer les textes TextMeshPro

public class GameManager : MonoBehaviour
{
    // Référence au panneau affiché après la défaite
    public GameObject gameOverPanel;

    // Référence au panneau principal du jeu (UI en cours de partie)
    public GameObject gamePanel;

    // Texte qui affiche la distance parcourue à la fin
    public TextMeshProUGUI finalDistanceText;

    // Texte qui affiche le record (meilleure distance)
    public TextMeshProUGUI recordText;

    // Empêche de relancer GameOver plusieurs fois
    private bool isGameOver = false;

    // Méthode appelée lorsqu'on touche un obstacle
    public void GameOver()
    {
        StartCoroutine(GameOverDelay());

        // Empêche d'exécuter cette méthode plusieurs fois
        if (isGameOver) return;
        isGameOver = true;

        // Cache le HUD principal, montre le menu de fin
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        // Récupère la distance finale parcourue
        float finalDistance = Mathf.FloorToInt(FindObjectOfType<DistanceCounter>().distance);
        finalDistanceText.text = "Distance parcourue : " + finalDistance + " m";

        // Vérifie s'il y a un nouveau record
        float previousRecord = PlayerPrefs.GetFloat("BestDistance", 0f);
        if (finalDistance > previousRecord)
        {
            PlayerPrefs.SetFloat("BestDistance", finalDistance); // Enregistre le nouveau record
            recordText.text = "Nouveau record : " + finalDistance + " m";
        }
        else
        {
            recordText.text = "Record : " + Mathf.FloorToInt(previousRecord) + " m";
        }

        // Stoppe le compteur de distance
        FindObjectOfType<DistanceCounter>().StopCounter();
    }

        // Coroutine qui attend 0.5 seconde avant de couper le jeu
    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1f); // Attend 0.5 seconde

        // Met le jeu en pause
        Time.timeScale = 0f;
    }

    // Redémarre la scène actuelle
    public void Replay()
    {
        Time.timeScale = 1f; // On remet le temps à la normale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Réinitialise le record enregistré dans PlayerPrefs
    public void ResetRecord()
    {
        PlayerPrefs.DeleteKey("BestDistance");
        Replay(); // Redémarre la partie après avoir supprimé le record
    }

    // Quitte l'application (ne fonctionne que dans une build, pas dans l'éditeur)
    public void QuitGame()
    {
        Application.Quit();
    }
}
