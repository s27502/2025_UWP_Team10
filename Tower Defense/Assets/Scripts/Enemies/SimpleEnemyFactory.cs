using ScriptableObjects;
using UnityEngine;

namespace Enemies
{
    public class SimpleEnemyFactory : MonoBehaviour
    {
        
        public IEnemy SpawnEnemy(EnemyData enemyData, Transform spawnPoint, Transform[] path)
        {
            Enemy newEnemy = Instantiate(enemyData.Prefab, spawnPoint.position, spawnPoint.rotation);
            newEnemy.SetPath(path);
            newEnemy.Initialize(enemyData);
            return newEnemy;
        }
    }
}