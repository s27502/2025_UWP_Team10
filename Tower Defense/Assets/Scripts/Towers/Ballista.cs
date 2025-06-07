using System.Collections.Generic;
using Enemies;
using Towers;
using UnityEngine;

public class Ballista : Tower
{
    private List<Enemy> enemiesInRange = new();
    private float attackCooldown;

    private void Update()
    {
        if (!gameObject.activeSelf) return;


        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f && enemiesInRange.Count > 0)
        {
            Defend();
            attackCooldown = data.AttackSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlyingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
                enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FlyingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemiesInRange.Contains(enemy))
                enemiesInRange.Remove(enemy);
        }
    }

    public override void Defend()
    {
        if (enemiesInRange.Count == 0) return;

        Enemy target = enemiesInRange[0];

        if (target == null)
        {
            enemiesInRange.RemoveAt(0);
            return;
        }
        
        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f); 
        }

        Debug.Log($"Attacking enemy: {target.name} for {data.Damage} dmg");
        target.TakeDamage(data.Damage);
    }

}