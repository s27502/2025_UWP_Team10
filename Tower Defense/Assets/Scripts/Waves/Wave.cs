using ScriptableObjects;

namespace Waves
{
    [System.Serializable]
    public class Wave
    {
        public EnemyData[] enemiesInWave;
        public int numberToSpawn;
        public float enemySpawnRate;
    }
}