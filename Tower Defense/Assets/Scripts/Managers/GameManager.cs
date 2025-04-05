using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int _numberOfWaves;
    private int currentWave;
    private GameState _gameState;
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
            ServiceLocator.Instance.Register(this);
            _gameState = GameState.BUILDING;
            currentWave = 1;
        }
        

    }

    private void Update()
    {
        if (currentWave <= _numberOfWaves)
        {
            Debug.Log(_gameState);
            switch (_gameState)
            {
                case GameState.BUILDING:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _gameState = GameState.DEFENDING;
                    }
                    //TODO faza budowania wież
                    break;
                case GameState.DEFENDING:
                    //TODO faza obrony
                    break;
            }
        }
    }

    public GameState GetGameState()
    {
        return _gameState;
    }
}
