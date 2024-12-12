using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2f;

    void Update()
    {
        // Move enemy downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Destroy the enemy if it goes off-screen
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y - 1)
        {
            Destroy(gameObject);
        }
    }

    // Method to set the speed dynamically
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
