using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Speed of the player's movement
    public GameObject PlayerBullet; // Reference to PlayerBullet prefab
    public GameObject PlayerBulletPosition01; // Bullet spawn position 1
    public GameObject PlayerBulletPosition02; // Bullet spawn position 2

    public int lives = 3; // Player lives
    public TextMeshProUGUI livesText; // Reference to TextMeshPro UI Text to display lives
    public TextMeshProUGUI gameOverText; // Reference to TextMeshPro UI Text for Game Over
    public TextMeshProUGUI scoreText; // Reference to TextMeshPro UI Text for score

    private int score = 0; // Player score
    private bool isGameOver = false; // To track if the game is over
    private SpriteRenderer spriteRenderer; // Reference to the player's SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        UpdateLivesUI(); // Initialize the lives UI
        UpdateScoreUI(); // Initialize the score UI
        gameOverText.gameObject.SetActive(false); // Hide Game Over text initially
    }

    void Update()
    {
        if (isGameOver)
        {
            return; // Stop further processing if the game is over
        }

        // Handle player movement
        float horizontal = Input.GetAxis("Horizontal"); // For left-right movement
        float vertical = Input.GetAxis("Vertical"); // For up-down movement

        Vector3 movement = new Vector3(horizontal, vertical, 0f) * speed * Time.deltaTime;
        transform.position += movement; // Apply movement to the player's position
        transform.rotation = Quaternion.identity; // Keep the player's rotation fixed

        // Shoot bullets when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullets();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives--; // Decrement lives
            UpdateLivesUI(); // Update the lives UI
            Destroy(collision.gameObject); // Destroy the enemy

            if (lives <= 0)
            {
                HandlePlayerDeath(); // Handle player death
            }
        }
    }

    void ShootBullets()
    {
        // Instantiate bullets at the starting positions
        GameObject bullet01 = Instantiate(PlayerBullet);
        bullet01.transform.position = PlayerBulletPosition01.transform.position;

        GameObject bullet02 = Instantiate(PlayerBullet);
        bullet02.transform.position = PlayerBulletPosition02.transform.position;
    }

    public void AddScore(int amount)
    {
        score += amount; // Increment score
        UpdateScoreUI(); // Update the score UI
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives; // Update the lives Text UI
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the score Text UI
        }
    }

    void HandlePlayerDeath()
    {
        Debug.Log("Player has no lives remaining!");
        isGameOver = true; // Set the game over flag

        // Hide the player's sprite
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Disable the sprite renderer
        }

        GameOver(); // Trigger the Game Over logic
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true); // Display the Game Over text
            gameOverText.text = "Game Over!"; // Set the Game Over message
        }
        Time.timeScale = 0f; // Stop the game
    }
}
