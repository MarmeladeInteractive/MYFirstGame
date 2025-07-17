using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gamePanel;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI recordText;

    private bool isGameOver = false;

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Time.timeScale = 0f;

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        float finalDistance = Mathf.FloorToInt(FindObjectOfType<DistanceCounter>().distance);
        finalDistanceText.text = "Distance parcourue : " + finalDistance + " m";

        float previousRecord = PlayerPrefs.GetFloat("BestDistance", 0f);
        if (finalDistance > previousRecord)
        {
            PlayerPrefs.SetFloat("BestDistance", finalDistance);
            recordText.text = "Nouveau record : " + finalDistance + " m";
        }
        else
        {
            recordText.text = "Record : " + Mathf.FloorToInt(previousRecord) + " m";
        }

        FindObjectOfType<DistanceCounter>().StopCounter();
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetRecord()
    {
        PlayerPrefs.DeleteKey("BestDistance");
        Replay();
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
