
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject endPanel;
    public TextMeshProUGUI endText;

    private bool gameEnded = false;

    void Awake()
    {
        Instance = this;

        // Start the game paused
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        endPanel.SetActive(false);
    }

    public void StartGame()
    {
        // Hide starting screen and begin gameplay
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EndGame(bool playerWon)
    {
        // Prevent the ending screen from appearing twice
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (playerWon)
        {
            endText.text = "YOU WIN!";
        }
        else
        {
            endText.text = "GAME OVER!";
        }

        // Display the ending screen
        endPanel.SetActive(true);

        // Pause gameplay
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
