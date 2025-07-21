using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Force appliquée vers le haut lors du saut
    public float jumpForce = 10f;

    public AudioClip jumpSound;     // Son du saut
    public AudioClip deathSound;    // Son de la mort
    public AudioClip pockSound;     // Son de la chute
    public AudioClip breathSound;   // Son de la soufle

    private AudioSource audioSource;
    // Référence au composant Rigidbody2D du joueur
    private Rigidbody2D rb;

    // Indique si le joueur est actuellement au sol
    private bool isGrounded = true;

    private Animator animator;

    void Start()
    {
        // On récupère le Rigidbody2D attaché au joueur
        rb = GetComponent<Rigidbody2D>();
        // On récupère l’audio
        audioSource = GetComponent<AudioSource>();
        // On récupère l’animator
        animator = GetComponent<Animator>();
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
            // Son de saut chute
            if (pockSound != null) audioSource.PlayOneShot(pockSound);
            isGrounded = true;

            // Relance l'animation quand on touche le sol
            animator.speed = 1f;

            // Espace est maintenue au moment d’atterrir ? → on saute automatiquement
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        // Si le joueur touche un obstacle, on lance la fin du jeu avec un délai
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Son de mort joué
            if (deathSound != null) audioSource.PlayOneShot(deathSound);

            // pour qu'il n'y ai pas de nouveau saut
            jumpForce = 0f;

            // pause anim à la mort
            animator.speed = 0f;

            Debug.Log("Game Over");
            FindObjectOfType<GameManager>().GameOver(); // Appelle la méthode GameOver du GameManager
        }
    }

    private void Jump()
    {
        if (jumpForce == 0f) return;
        // Son de saut joué
        if (jumpSound != null) audioSource.PlayOneShot(jumpSound);
        // Son de soufle joué
        if (breathSound != null) audioSource.PlayOneShot(breathSound);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

        // Met en pause l'animation pendant le saut
        animator.speed = 0f;
    }
}
