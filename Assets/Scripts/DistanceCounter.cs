using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DistanceCounter : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public float distance = 0f;
    public float speed = 5f;
    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        distance += speed * Time.deltaTime;
        distanceText.text = "Distance : " + Mathf.FloorToInt(distance) + " m";
    }

    public void StopCounter()
    {
        isGameOver = true;
    }
}