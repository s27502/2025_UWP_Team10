using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class Tower : MonoBehaviour, IPointerClickHandler, ITower
    {
        [SerializeField] protected TowerData data;
        [SerializeField] protected GameObject baseModel;
        [SerializeField] protected GameObject upgradeModel;
        [SerializeField] private ParticleSystem shootEffect;

        protected PlacingField _placingField;
        private GameObject _upgradePanel;
        private bool _upgradePanelActive;

        protected List<Enemy> enemiesInRange = new();
        private float attackCooldown;
        private float bonusDamage = 0f;
        private float bonusAttackSpeed = 0f;

        protected virtual string GetEnemyTag() => "Enemy";
        protected virtual string GetAttackSound() => "Default_Shot";

        private void Update()
        {
            if (!gameObject.activeSelf) return;

            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f && enemiesInRange.Count > 0)
            {
                Defend();
                attackCooldown = Mathf.Max(0.05f, data.AttackSpeed - bonusAttackSpeed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GetEnemyTag()))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null && !enemiesInRange.Contains(enemy))
                    enemiesInRange.Add(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GetEnemyTag()))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null && enemiesInRange.Contains(enemy))
                    enemiesInRange.Remove(enemy);
            }
        }

        public void IncreaseDamage(float amount)
        {
            bonusDamage += amount;
        }

        public void IncreaseAttackSpeed(float amount)
        {
            bonusAttackSpeed += amount;
        }

        public virtual void Defend()
        {
            CleanupEnemies();

            if (enemiesInRange.Count == 0) return;

            Enemy target = null;

            while (enemiesInRange.Count > 0)
            {
                target = enemiesInRange[0];

                if (target == null || !target.gameObject.activeSelf)
                {
                    enemiesInRange.RemoveAt(0);
                    continue;
                }

                break;
            }

            if (target == null) return;

            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation;
            }

            float totalDamage = data.Damage + bonusDamage;
            target.TakeDamage(totalDamage);

            Debug.Log($"Attacking enemy: {target.name} for {totalDamage} dmg");

            ServiceLocator.Instance.GetService<AudioManager>().PlayClip("SFX", GetAttackSound());

            if (shootEffect != null)
            {
                shootEffect.Play();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_upgradePanelActive)
            {
                _upgradePanel = ServiceLocator.Instance.GetService<HUDController>().GetHUDView().gameObject.transform.Find("UpgradePanel").gameObject;
                _upgradePanel.transform.position = eventData.position;
                _upgradePanel.SetActive(true);

                UpgradePanel panel = _upgradePanel.GetComponent<UpgradePanel>();
                panel.SetTower(this);
                panel.SetPlacingField(_placingField);
                _upgradePanelActive = true;
            }
        }

        public void setUpgradePanelActive(bool active)
        {
            _upgradePanelActive = active;
        }

        public string GetTowerName() => data.TowerName;
        public int GetCost() => data.Cost;
        public float GetAttackSpeed() => data.AttackSpeed - bonusAttackSpeed;
        public float GetDamage() => data.Damage + bonusDamage;
        public TowerData GetData() => data;

        public void SetPlacingField(PlacingField placingField)
        {
            _placingField = placingField;
        }

        private void CleanupEnemies()
        {
            enemiesInRange.RemoveAll(e => e == null || !e.gameObject.activeSelf);
        }

        public void UpgradeModel()
        {
            baseModel.SetActive(false);
            upgradeModel.SetActive(true);
        }
    }
}
