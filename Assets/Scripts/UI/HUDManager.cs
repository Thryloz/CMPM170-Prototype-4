using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject indicatorPivot;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

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
}
