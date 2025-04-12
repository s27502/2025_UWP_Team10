using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int _numberOfWaves;
    private int currentWave;
    private GameState _gameState;
    private WaveManager _waveManager;
    
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
            currentWave = 0;
        }
        
        //_waveManager = ServiceLocator.Instance.GetService<WaveManager>();
        //_numberOfWaves = _waveManager.waves.Length;
        
        //_waveManager.OnWaveComplete.AddListener(OnWaveComplete);
    }

    private void Update()
    {
        if (currentWave <= _numberOfWaves)
        {
            //Debug.Log(_gameState);
            switch (_gameState)
            {
                case GameState.BUILDING:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _gameState = GameState.DEFENDING;
                        //_waveManager.StartWave();
                    }
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
    
    public void OnWaveComplete()
    {
        
        if (currentWave <= _numberOfWaves)
        {
            _gameState = GameState.BUILDING; 
            currentWave++;
            ServiceLocator.Instance.GetService<HUDController>()?.SetWave(currentWave);
        }
        else
        {
            Debug.Log("All waves completed!");
            // #TODO Implement endgame logic or UI to show completion
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
