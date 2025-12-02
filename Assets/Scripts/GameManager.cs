using System;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;


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
