using Enemies;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/EnemyData", fileName = "NewEnemyData")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float MaxHP { get; private set; }
        [field: SerializeField] public Enemy Prefab { get; private set; }
        [field: SerializeField] public int Reward { get; private set; }
     
        [field: SerializeField] public int Damage { get; private set; }
        
    }
}