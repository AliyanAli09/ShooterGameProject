using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed of player movement
    float maxSpeed = 3f;
    // Speed of rotation
    float rotSpeed = 180f;
    // Radius for boundary checks
    float playerBoundaryRadius = 0.5f;

    void Update()
    {
        // Get current position
        Vector2 pos = transform.position;

        // Movement logic (Vertical and Horizontal)
        pos.y += Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime;
        pos.x += Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;

        // Apply vertical boundary constraints
        if (pos.y + playerBoundaryRadius > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - playerBoundaryRadius;
        }
        if (pos.y - playerBoundaryRadius < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + playerBoundaryRadius;
        }

        // Get the horizontal boundaries based on the camera's aspect ratio
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        if (pos.x + playerBoundaryRadius > screenWidth)
        {
            pos.x = screenWidth - playerBoundaryRadius;
        }
        if (pos.x - playerBoundaryRadius < -screenWidth)
        {
            pos.x = -screenWidth + playerBoundaryRadius;
        }

        // Update position
        transform.position = pos;

        // Rotation logic (rotate the player while pressing Horizontal)
        Quaternion rot = transform.rotation; // Grab the current rotation
        float z = rot.eulerAngles.z; // Get z angle
        z = z - Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime; // Apply rotation
        rot = Quaternion.Euler(0, 0, z); // Set the new rotation
        transform.rotation = rot;
    }
}
