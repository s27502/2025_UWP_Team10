
using ScriptableObjects;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IEnemy, IPoolableObject
    {
        protected EnemyData data;
        private float currentHP;
        protected Transform[] path;
        private int pathIndex = 0;
        private const float PathThreshold = 0.1f;
        private Vector3 direction;
        private IObjectPool _pool;

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
            ServiceLocator.Instance.GetService<ResourceManager>()?.DealDamage(data.Damage);
            Die();
        }
        
        

        public void TakeDamage(float amount)
        {
            currentHP -= amount;

            var enemyHpBar = this.gameObject.GetComponent<EnemyHpBar>();
            enemyHpBar.UpdateHpBar(currentHP, data.MaxHP);
            
            if (currentHP <= 0)
            {
                Die();
                enemyHpBar.UpdateHpBar(1, 1);
            }
            ServiceLocator.Instance.GetService<AudioManager>().PlayClip("SFX", "Enemy_Hit");
        }

        public void Die()
        {
            // TODO play VFX, sound and increase money
            ServiceLocator.Instance.GetService<ResourceManager>().AddMoney(data.Reward);
            ServiceLocator.Instance.GetService<WaveManager>()?.RemoveEnemy(this); //#TODO probably subscribe to OnEnemyDeath method in gamemanager and then pass this to wave manager
            //Destroy(gameObject);
            _pool?.ReleaseObject(this);
        }

        public void Kill()
        {
            ServiceLocator.Instance.GetService<ResourceManager>()?.AddMoney(data.Reward);
            Die();
        }

        public void Spawn(Vector3 position, Vector3 forwardDirection)
        {
            gameObject.SetActive(true);
            transform.position = position;
            transform.forward = forwardDirection;
    
            currentHP = data.MaxHP;
            pathIndex = 0;
        }

        public void Despawn()
        {
            gameObject.SetActive(false);
        }

        public void SetPool(IObjectPool pool)
        {
            _pool = pool;
        }
    }
}