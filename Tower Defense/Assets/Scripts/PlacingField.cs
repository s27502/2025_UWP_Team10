using System;
using System.Resources;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlacingField : MonoBehaviour, 
    IPointerClickHandler
{
    private GameObject _tower;
    private ResourceManager _resourceManager;
    private GameObject _placementPanel;
    private bool _placementPanelActive;
    private bool _towerPlaced;
    private UpgradePanel _upgradePanel;


    private void Awake()
    {
        _tower = transform.Find("Tower").gameObject;
        _placementPanelActive = false;
        _upgradePanel.GetComponent<UpgradePanel>().SetPlacingField(this);
        _towerPlaced = false;
        _resourceManager = ServiceLocator.Instance.GetService<ResourceManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _placementPanel = ServiceLocator.Instance.GetService<HUDController>().GetHUDView().gameObject.transform.Find("PlacementPanel").gameObject;
        
        
        if (!_placementPanelActive && !_towerPlaced)
        {
            _placementPanel.transform.position = eventData.position;
            _placementPanel.SetActive(true);
            _placementPanel.GetComponent<PlacementPanel>().SetPlacingField(this);
            _placementPanel.GetComponent<PlacementPanel>().SetCannonPriceText(100);// update aby cena brala sie z wiezy
            _placementPanelActive = true;
        }


    }

    public void PlaceTower()
    {
        
        if (_tower.gameObject.GetComponent<BasicTower>().GetCost() <= _resourceManager.GetMoney())
        { 
            _tower.SetActive(true);
            _resourceManager.SpendMoney(_tower.gameObject.GetComponent<BasicTower>().GetCost());
            _placementPanel.SetActive(false);
            _placementPanelActive = false;
            _towerPlaced = true;
        }
        else
        {
            _placementPanel.SetActive(false);
            _placementPanelActive = false;
        }

    }
    public void SetTowerPlaced(bool towerPlaced)
    {
        _towerPlaced = towerPlaced;
    }
    public void SetPlacementPanelActive(bool active)
    {
        _placementPanelActive = active;
    }
}