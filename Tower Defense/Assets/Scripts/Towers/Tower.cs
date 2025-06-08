using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class Tower : MonoBehaviour, IPointerClickHandler, ITower
    {
        [SerializeField] protected TowerData data;
        private PlacingField _placingField;                                             
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

        public virtual void Defend(){}
       
        
        public string GetTowerName() => data.TowerName;
        public int GetCost() => data.Cost;
        public float GetAttackSpeed() => data.AttackSpeed;
        public float GetDamage() => data.Damage;

        public TowerData GetData()
        {
            return data;
        }
        public void SetPlacingField(PlacingField placingField)
        {
            _placingField = placingField;
        }
        
    }
}