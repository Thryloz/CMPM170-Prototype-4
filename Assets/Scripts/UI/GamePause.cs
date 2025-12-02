using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    [Header("Pause Menu UI")]
    [SerializeField] public GameObject pauseMenuPanel;
    [SerializeField] public Button homeButton;
    [SerializeField] private Button playButton;

    [Header("Settings")]
    [SerializeField] public string mainMenuSceneName = "MainMenuScene";

    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause menu is hidden at start
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        // Setup button listeners
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(GoToMainMenu);
        }

        if (playButton != null)
        {
            playButton.onClick.AddListener(ResumeGame);
        }
    }

    void Update()
    {
        //// Check for ESC key press
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isPaused)
        //    {
        //        ResumeGame();
        //    }
        //    else
        //    {
        //        PauseGame();
        //    }
        //}
    }

    public void PauseGame()
    {
        isPaused = true;

        // Show pause menu
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }

        // Freeze game time
        Time.timeScale = 0f;

        // Optional: Unlock cursor for menu interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;

        // Hide pause menu
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        // Resume game time
        Time.timeScale = 1f;

        // Optional: Lock cursor back for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoToMainMenu()
    {
        // Resume time before loading new scene
        Time.timeScale = 1f;

        // Load main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }

    void OnDestroy()
    {
        // Ensure time scale is reset when script is destroyed
        Time.timeScale = 1f;
    }
}