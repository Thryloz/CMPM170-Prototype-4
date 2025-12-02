using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
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
