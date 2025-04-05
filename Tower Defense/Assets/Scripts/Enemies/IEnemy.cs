using UnityEngine;

namespace Enemies
{
    public interface IEnemy : IDamageable
    {
        void SetPath(Transform[] path);
        void Move();
        void Die();
    }
}