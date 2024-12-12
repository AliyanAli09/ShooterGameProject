using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float bulletSpeed = 8f;
    public AudioClip hitSound; // Assign an audio clip for the hit sound
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Move bullet upwards
        Vector2 pos = transform.position;
        pos.y += bulletSpeed * Time.deltaTime;
        transform.position = pos;

        // Destroy bullet if it goes out of screen
        Vector2 maxPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y > maxPosition.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Access the Player script and update score
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.AddScore(10); // Add 10 points per enemy
            }

            // Play hit sound
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            // Destroy the enemy
            Destroy(collision.gameObject);

            // Delay destruction of the bullet to allow sound to play
            Destroy(gameObject, hitSound.length);
        }
    }
}
