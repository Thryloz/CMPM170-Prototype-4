using System;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

    public TempestController player;
    public HUDAimIndicator aimIndicator;

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);

    }


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<TempestController>();
    }

    void Update()
    {
        
    }

    public void EndGame()
    {

    }
}
