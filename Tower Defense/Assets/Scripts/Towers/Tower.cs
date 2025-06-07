using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class Tower : MonoBehaviour, IPointerClickHandler, ITower
    {
        [SerializeField] protected TowerData data;
        private GameObject _upgradePanel;
        private bool _upgradePanelActive;
        private IPointerClickHandler _pointerClickHandlerImplementation;
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_upgradePanelActive)
            {
                _upgradePanel = ServiceLocator.Instance.GetService<HUDController>().GetHUDView().gameObject.transform.Find("UpgradePanel").gameObject;
                _upgradePanel.transform.position = eventData.position;
                _upgradePanel.SetActive(true);
                _upgradePanel.GetComponent<UpgradePanel>().SetTower(this);
                _upgradePanelActive = true;
            }
        }
        
        
        public void setUpgradePanelActive(bool active)
        {
            _upgradePanelActive = active;
        }

        public virtual void Defend()
        {
            throw new System.NotImplementedException();
        }
        
        public string GetTowerName() => data.TowerName;
        public int GetCost() => data.Cost;
        public float GetAttackSpeed() => data.AttackSpeed;
        public float GetDamage() => data.Damage;
    }
}