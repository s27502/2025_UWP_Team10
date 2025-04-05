using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Waves;


public class WaveManager : MonoBehaviour
{
   
    public static WaveManager Instance { get; private set; }
    
    public UnityEvent OnWaveComplete;
    // private GameManager _gameManager; todo
    public Wave[] waves;
    private Wave currentWave;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] path;
    private int  waveIndex = 0;
    private WaveState currentState;
    private int enemyIndex = 0;
    private float spawnTimer;
    private int enemiesSpawned;
    private List<GameObject> aliveEnemies = new List<GameObject>();

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
        //#TODO Register WaveManager in the ServiceLocator
        //ServiceLocator.Instance.Register<WaveManager>(this);
        
    }

    private void Start()
    { 
        // _gameManager = ServiceLocator.Get<IGameManager>(); todo
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
                // wait for UI input
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
        GameObject enemyPrefab = currentWave.EnemiesInWave[enemyIndex];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        // newEnemy.SetPath(path); TODO
        aliveEnemies.Add(newEnemy);
        enemyIndex++;
        enemiesSpawned++;
    }

    private void StartWave()
    {
        if (waveIndex >= waves.Length)
        {
            //todo move to game menager when implemented
            Debug.Log("All waves complete.");
            return;
        }

        currentWave = waves[waveIndex];
        waveIndex++;

        enemyIndex = 0;
        enemiesSpawned = 0;
        spawnTimer = 0f;
        SetState(WaveState.Spawn);
        
    }
    
    private void SetState(WaveState newState)
    {
        currentState = newState;
    }
    
    public void OnNextWaveButtonClicked() // todo
    {
        if (currentState != WaveState.Pause) return;
        StartWave();
    }
    // #TODO Call this method in ENEMY CLASS when an enemy dies
    public void RemoveEnemy(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);  
    }
}