using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Waves", fileName = "Wave")]
    public class Wave : ScriptableObject {
        [field: SerializeField]
        public EnemyData[] EnemiesInWave { get; private set; }
        [field: SerializeField]
        public float EnemySpawnRate { get; private set; }
        [field: SerializeField]
        public int NumberToSpawn { get; private set; }
    }
    
}
