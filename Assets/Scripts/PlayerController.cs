using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Force appliquée vers le haut lors du saut
    public float jumpForce = 10f;

    // Référence au composant Rigidbody2D du joueur
    private Rigidbody2D rb;

    // Indique si le joueur est actuellement au sol
    private bool isGrounded = true;

    void Start()
    {
        // On récupère le Rigidbody2D attaché au joueur
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Si la touche ESPACE est pressée et que le joueur est au sol, sauter
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) Jump();
    }

    // Détecte les collisions avec d'autres objets
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le joueur touche le sol, on réactive le saut
        if (collision.gameObject.CompareTag("Ground"))
        {
             isGrounded = true;

            // Espace est maintenue au moment d’atterrir ? → on saute automatiquement
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        // Si le joueur touche un obstacle, on lance la fin du jeu avec un délai
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            FindObjectOfType<GameManager>().GameOver(); // Appelle la méthode GameOver du GameManager
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }
}
