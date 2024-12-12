using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnInterval = 2f; // Interval between enemy spawns
    public float initialSpeed = 2f; // Initial speed of enemies
    public float speedIncrement = 0.2f; // Speed increment over time
    public float timeToIncreaseSpeed = 5f; // Time interval to increase speed

    private float currentSpeed; // Current speed of enemies
    private float spawnRangeX; // Horizontal range for spawning enemies
    private float spawnHeight; // Vertical spawn position above the screen

    void Start()
    {
        // Calculate the spawn boundaries based on the camera's viewport
        Camera camera = Camera.main;
        spawnRangeX = camera.orthographicSize * camera.aspect; // Horizontal boundary
        spawnHeight = camera.orthographicSize + 1; // Slightly above the top of the screen

        currentSpeed = initialSpeed; // Set the initial speed of enemies

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval); // Start spawning enemies
        InvokeRepeating(nameof(IncreaseSpeed), timeToIncreaseSpeed, timeToIncreaseSpeed); // Gradually increase enemy speed
    }

    void SpawnEnemy()
    {
        // Randomize the X position within the camera's horizontal bounds
        float xPosition = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, 0);

        // Instantiate the enemy and set its speed
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetSpeed(currentSpeed); // Apply the current speed to the enemy
        }
    }

    void IncreaseSpeed()
    {
        currentSpeed += speedIncrement; // Increment the speed
        Debug.Log("Enemy speed increased to: " + currentSpeed);
    }
}
