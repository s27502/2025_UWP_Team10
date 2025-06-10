using System;
using System.Collections.Generic;
using Enemies;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Waves;

public class WaveManager : SingletonDoNotDestroy<WaveManager>
{
    public UnityEvent OnWaveComplete;
    public UnityEvent OnWaveStart;

    [SerializeField] public Wave[] waves;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] path;
    [SerializeField] private WavePreviewManager wavePreviewManager;
    private GameManager _gameManager;
    private Wave currentWave;
    private int waveIndex = 0;
    private WaveState currentState;
    private int enemyIndex = 0;
    private float spawnTimer;
    private int enemiesSpawned;

    private readonly List<IEnemy> aliveEnemies = new();
    private readonly Dictionary<string, int> counts = new();
    private readonly SimpleEnemyFactory _factory = new();

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this) return;

        ServiceLocator.Instance.Register(this);
    }

    private void Start()
    {
        _gameManager = ServiceLocator.Instance.GetService<GameManager>();
        currentWave = waves[waveIndex];
        SetState(WaveState.Pause);
    }

    private void Update()
    {
        switch (currentState)
        {
            case WaveState.Spawn:
                HandleSpawning();
                break;

            case WaveState.Wait:
                if (aliveEnemies.Count == 0)
                {
                    SetState(WaveState.Pause);
                    OnWaveComplete?.Invoke();
                }
                break;

            case WaveState.Pause:
                break;
        }
    }

    private void HandleSpawning()
    {
        if (enemiesSpawned >= currentWave.NumberToSpawn)
        {
            SetState(WaveState.Wait);
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemyFromWave();
            spawnTimer = currentWave.EnemySpawnRate;
        }
    }

    private void SpawnEnemyFromWave()
    {
        EnemyData enemyData = currentWave.EnemiesInWave[
            UnityEngine.Random.Range(0, currentWave.EnemiesInWave.Length)
        ];
        IEnemy newEnemy = _factory.SpawnEnemy(enemyData, spawnPoint, path);

        aliveEnemies.Add(newEnemy);
        enemiesSpawned++;
        
        string name = enemyData.name;
        if (counts.ContainsKey(name)) counts[name]++;
        else counts[name] = 1;
        GetEnemyTypesInCurrentWave();
    }

    public void StartWave()
    {
        if (waveIndex >= waves.Length) return;

        OnWaveStart?.Invoke();

        currentWave = waves[waveIndex];
        waveIndex++;

        enemyIndex = 0;
        enemiesSpawned = 0;
        spawnTimer = 0f;
        counts.Clear();

        SetState(WaveState.Spawn);
    }
    

    private void SetState(WaveState newState)
    {
        currentState = newState;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        aliveEnemies.Remove(enemy);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            ServiceLocator.Instance.UnregisterService<WaveManager>();
        }
    }

    private void GetEnemyTypesInCurrentWave()
    {
        var fastcount = 0;
        var slowcount = 0;
        
        fastcount = GameObject.FindGameObjectsWithTag("FlyingEnemy").Length;
        slowcount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        wavePreviewManager.RefreshWavePreview(fastcount, slowcount);
        
    }

    

   
}
