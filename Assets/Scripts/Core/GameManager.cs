using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

    public List<GameObject> enemies;

    public static event Action<bool> OnPauseGame;
    public static event Action OnGameOver;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
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
        Time.timeScale = 1.0f;
    }

    public void EndGame()
    {
        OnGameOver?.Invoke();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        AudioManager.Instance.PauseBGM();
    }

    public void HandlePauseGame()
    {
        if (!isPaused)
        {
            OnPauseGame.Invoke(isPaused);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            OnPauseGame.Invoke(isPaused);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            isPaused = false;
        }

    }
}
