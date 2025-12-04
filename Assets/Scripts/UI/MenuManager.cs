using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true); 
    }
    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
    }
}
