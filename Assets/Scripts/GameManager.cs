using UnityEngine;
using TMPro; // Required for TextMeshPro


public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    public TextMeshProUGUI pauseButtonText;

    public bool isPaused = false; // Pause state

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int score = 0;

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

     // Pause/Resume game logic
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pause the game
            pauseButtonText.text = "Resume"; // Change button text
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            pauseButtonText.text = "Pause"; // Change button text
        }
    }

}
