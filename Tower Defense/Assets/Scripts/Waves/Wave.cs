using ScriptableObjects;

[System.Serializable]
public class Wave
{
    public EnemyData[] EnemiesInWave;
    public int NumberToSpawn;
    public float EnemySpawnRate;
}