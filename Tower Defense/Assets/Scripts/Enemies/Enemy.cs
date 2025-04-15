
using ScriptableObjects;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        protected EnemyData data;
        private float currentHP;
        protected Transform[] path;
        private int pathIndex = 0;
        private const float PathThreshold = 0.1f;
        private Vector3 direction;
        public void Initialize(EnemyData enemyData)
        {
            data = enemyData;
            currentHP = data.MaxHP;
        }

        public void SetPath(Transform[] pathPoints)
        {
            path = pathPoints;
            pathIndex = 0;
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            if (path == null || pathIndex >= path.Length)
                return;

            Transform targetPoint = path[pathIndex];
            float _speed = data.Speed * Time.deltaTime;

            direction = (targetPoint.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed);

            if (Vector3.Distance(transform.position, targetPoint.position) < PathThreshold)
            {
                pathIndex++;

                if (pathIndex >= path.Length)
                {
                    ReachGoal();
                }
            }
        }

        private void ReachGoal()
        {
            // TODO: Deal damage to base
            Die();
        }

        public void TakeDamage(float amount)
        {
            currentHP -= amount;

            if (currentHP <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            // TODO play VFX, sound and increase money
            ServiceLocator.Instance.GetService<WaveManager>()?.RemoveEnemy(this); //#TODO probably subscribe to OnEnemyDeath method in gamemanager and then pass this to wave manager
            ServiceLocator.Instance.GetService<ResourceManager>()?.DealDamage(1);
            Destroy(gameObject);
        }

        public void Kill()
        {
            ServiceLocator.Instance.GetService<ResourceManager>()?.AddMoney(data.Reward);
            Die();
        }
    }
}