using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject indicatorPivot;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject settingsMenu;

    private void OnEnable()
    {
        TempestController.OnObtainRubble += ShowRubbleAiming;
        TempestController.OnReleaseRubble += HideRubbleAiming;

        GameManager.OnGameOver += ShowGameOverMenu;
        GameManager.OnPauseGame += HandlePauseMenu;
    }

    private void OnDisable()
    {
        TempestController.OnObtainRubble -= ShowRubbleAiming;
        TempestController.OnReleaseRubble -= HideRubbleAiming;

        GameManager.OnGameOver -= ShowGameOverMenu;
        GameManager.OnPauseGame -= HandlePauseMenu;
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        crossHair.SetActive(false);
        indicatorPivot.SetActive(false);

        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void ShowRubbleAiming()
    {
        crossHair.SetActive(true);
        indicatorPivot.SetActive(true);
    }

    private void HideRubbleAiming()
    {
        crossHair.SetActive(false);
        indicatorPivot.SetActive(false);
    }

    private void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    private void HandlePauseMenu(bool isPaused)
    {
        pauseMenu.SetActive(!isPaused);
    }

    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void SettingsToPause()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
