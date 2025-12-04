using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject credits;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
        credits.SetActive(false);
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
        howToPlay.SetActive(false);
    }

    public void HideCredits()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
        credits.SetActive(false);
    }

    public void HideHowToPlay()
    {
        howToPlay.SetActive(false);
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
}
