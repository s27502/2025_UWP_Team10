using System.Collections.Generic;
using ObjectPooling;
using ScriptableObjects;
using UnityEngine;

namespace Enemies
{
    public class SimpleEnemyFactory : MonoBehaviour
    {
        
        private Dictionary<EnemyData, EnemyPool> enemyPools = new();

        public IEnemy SpawnEnemy(EnemyData enemyData, Transform spawnPoint, Transform[] path)
        {
            if (!enemyPools.TryGetValue(enemyData, out var pool))
            {
                pool = new EnemyPool(new EnemyFactory(enemyData), 50);
                enemyPools[enemyData] = pool;
            }

            var obj = pool.GetObject() as Enemy;

            if (obj is null) return null;

            obj.SetPool(pool);
            obj.SetPath(path);
            obj.Spawn(spawnPoint.position, (path[0].position - spawnPoint.position).normalized);
            return obj;
        }
        
        
        
        /*public IEnemy SpawnEnemy(EnemyData enemyData, Transform spawnPoint, Transform[] path)
        {
            Enemy newEnemy = Instantiate(enemyData.Prefab, spawnPoint.position, spawnPoint.rotation);
            newEnemy.SetPath(path);
            newEnemy.Initialize(enemyData);
            return newEnemy;
        }*/

        
    }
}