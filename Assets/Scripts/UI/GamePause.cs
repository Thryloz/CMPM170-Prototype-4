using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager;

    public void ResumeGame()
    {
        GameManager.Instance.HandlePauseGame();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToSettings()
    {
        hudManager.ShowSettingsMenu();
    }

    void OnDestroy()
    {
        // Ensure time scale is reset when script is destroyed
        Time.timeScale = 1f;
    }
}