using Enemies;
using ScriptableObjects;
using UnityEngine;

namespace ObjectPooling
{
    public class EnemyFactory : IObjectFactory
    {
        private EnemyData enemyData;

        public EnemyFactory(EnemyData data)
        {
            this.enemyData = data;
        }

        public IPoolableObject CreateNew()
        {
            Enemy enemy = GameObject.Instantiate(enemyData.Prefab);
            enemy.Initialize(enemyData);
            return enemy;
        }
    }
}