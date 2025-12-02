using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

    public List<GameObject> enemies;
    public GameObject gameover;

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        RestartGame();

    }

    void Update()
    {
        if (enemies.Count == 0)
        {
            EndGame();
            return;
        }
             
        foreach (GameObject enemy in new List<GameObject>(enemies))
        {
            if (enemy == null)
                enemies.Remove(enemy);
        }
    }

    public void RestartGame()
    {
        gameover.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void EndGame()
    {
        gameover.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f; 
    }
}
