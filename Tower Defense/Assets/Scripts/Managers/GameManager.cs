using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonDoNotDestroy<GameManager>
{
    private int _numberOfWaves;
    private int currentWave;
    private GameState _gameState;
    private WaveManager _waveManager;
    public event Action<GameState> OnGameStateChanged;


    public string GetEnemyTypesAndCountForWave()
    {
        var waveManager = ServiceLocator.Instance.GetService<WaveManager>();
        var counts = waveManager.GetEnemyTypeCounts();
        string enemyInfo = "Typy wrogów:\n";
        return enemyInfo;
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this) return;

        ServiceLocator.Instance.Register(this);
        SetGameState(GameState.BUILDING);
        currentWave = 1;

        _waveManager = ServiceLocator.Instance.GetService<WaveManager>();
        _numberOfWaves = _waveManager.waves.Length;

        _waveManager.OnWaveComplete.AddListener(OnWaveComplete);
    }

    public string GetCurrentWaveEnemyTypes()
    {
        return ServiceLocator.Instance.GetService<WaveManager>()?.GetEnemyTypesInCurrentWave();
    }

    public string GetEnemyTypeInfoString()
    {
        var waveManager = ServiceLocator.Instance.GetService<WaveManager>();
        var enemyCounts = waveManager?.GetEnemyTypeCounts();

        if (enemyCounts == null || enemyCounts.Count == 0)
            return "Brak wrogów";

        List<string> parts = new List<string>();
        foreach (var pair in enemyCounts)
        {
            parts.Add($"{pair.Key}: {pair.Value}");
        }

        return string.Join(", ", parts);
    }

    private void Update()
    {
        if (currentWave <= _numberOfWaves)
        {
            switch (_gameState)
            {
                case GameState.BUILDING:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SetGameState(GameState.DEFENDING);
                        _waveManager.StartWave();
                    }
                    break;
                case GameState.DEFENDING:
                    break;
            }
        }
    }

    public GameState GetGameState() => _gameState;

    public void OnWaveComplete()
    {
        if (currentWave <= _numberOfWaves)
        {
            SetGameState(GameState.BUILDING);
            currentWave++;
            ServiceLocator.Instance.GetService<HUDController>()?.SetWave(currentWave);
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }
    
    private void SetGameState(GameState newState)
    {
        if (_gameState != newState)
        {
            _gameState = newState;
            OnGameStateChanged?.Invoke(_gameState);
        }
    }

    public int GetCurrentWave() => currentWave;
}
