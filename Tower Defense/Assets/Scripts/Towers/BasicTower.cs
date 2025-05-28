using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BasicTower : MonoBehaviour, IPointerClickHandler, ITower
{
    [SerializeField] private int _cost;
    [SerializeField] private float _atkSpeed;

    [SerializeField] private float _dmg;
    private GameObject _upgradePanel;
    private bool _upgradePanelActive;
    private IPointerClickHandler _pointerClickHandlerImplementation;

    public int GetCost() => _cost;
    public float GetAtkSpeed() => _atkSpeed;
    public float GetDmg() => _dmg;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_upgradePanelActive)
        {
            _upgradePanel = ServiceLocator.Instance.GetService<HUDController>().GetHUDView().gameObject.transform.Find("UpgradePanel").gameObject;
            _upgradePanel.transform.position = eventData.position;
            _upgradePanel.SetActive(true);
            _upgradePanel.GetComponent<UpgradePanel>().SetBasicTower(this);
            _upgradePanelActive = true;
        }
        Debug.Log("click");
    }

    public void setUpgradePanelActive(bool active)
    {
        _upgradePanelActive = active;
    }

    public void Defend()
    {
        throw new System.NotImplementedException();
    }
}
