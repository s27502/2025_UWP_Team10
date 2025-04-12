using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Waves;


public class WaveManager : MonoBehaviour
{
   
    public static WaveManager Instance { get; private set; }
    public UnityEvent OnWaveComplete;
    private GameManager _gameManager; 
    public Wave[] waves;
    private Wave currentWave;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] path;
    private int  waveIndex = 0;
    private WaveState currentState;
    private int enemyIndex = 0;
    private float spawnTimer;
    private int enemiesSpawned;
    private List<IEnemy> aliveEnemies = new List<IEnemy>();
    public UnityEvent OnWaveStart;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; 
        }
       
        ServiceLocator.Instance.Register<WaveManager>(this);
    }

    private void Start()
    {
        _gameManager = ServiceLocator.Instance.GetService<GameManager>();
        currentWave = waves[ waveIndex];
        SetState(WaveState.Wait);
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
                // wait for player input
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
        EnemyData enemyData = currentWave.EnemiesInWave[enemyIndex];
        Enemy newEnemy = Instantiate(enemyData.Prefab, spawnPoint.position, spawnPoint.rotation);
        newEnemy.SetPath(path);
        newEnemy.Initialize(enemyData);

        aliveEnemies.Add(newEnemy);

        enemyIndex++;
        enemiesSpawned++;
    }

    public void StartWave()
    {
        OnWaveStart?.Invoke();

        if (waveIndex >= waves.Length) return;
        currentWave = waves[waveIndex];
        waveIndex++;

        enemyIndex = 0;
        enemiesSpawned = 0;
        spawnTimer = 0f;
        SetState(WaveState.Spawn);
        OnWaveStart?.Invoke();

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
    public string GetEnemyTypesInCurrentWave()
    {
        if (currentWave == null || currentWave.EnemiesInWave == null) return "";

        HashSet<string> enemyTypeNames = new HashSet<string>();
        foreach (var enemyData in currentWave.EnemiesInWave)
        {
            if (enemyData != null)
            {
                enemyTypeNames.Add(enemyData.name);
            }
        }

        return $"Typy wrogów: {enemyTypeNames.Count} – {string.Join(", ", enemyTypeNames)}";
    }

    public Dictionary<string, int> GetEnemyTypeCounts()
    {
        Dictionary<string, int> counts = new Dictionary<string, int>();
        foreach (var enemyData in currentWave.EnemiesInWave)
        {
            string name = enemyData.name;
            if (counts.ContainsKey(name)) counts[name]++;
            else counts[name] = 1;
        }
        return counts;
    }


}