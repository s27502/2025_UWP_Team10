using System;
using System.Resources;
using Towers;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlacingField : MonoBehaviour, 
    IPointerClickHandler
{
    private GameObject _cannon;
    private GameObject _ballista;
    private ResourceManager _resourceManager;
    private GameObject _placementPanel;
    private bool _placementPanelActive;
    private bool _towerPlaced;
    private GameObject _upgradePanel;


    private void Awake()
    {
        _cannon = transform.Find("Cannon").gameObject;
        _ballista = transform.Find("Ballista").gameObject;
        _placementPanelActive = false;
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
            _placementPanel.GetComponent<PlacementPanel>().SetCannonPriceText(_cannon.GetComponent<Cannon>().GetCost());// update aby cena brala sie z wiezy
            _placementPanel.GetComponent<PlacementPanel>().SetBallistaPriceText(_ballista.GetComponent<Ballista>().GetCost());
            _placementPanelActive = true;
        }


    }

    public void PlaceCannon() 
    {
        _upgradePanel = ServiceLocator.Instance.GetService<HUDController>().GetHUDView().gameObject.transform.Find("UpgradePanel").gameObject;
        _upgradePanel.GetComponent<UpgradePanel>().SetPlacingField(this);
        if (_cannon.gameObject.GetComponent<Tower>().GetCost() <= _resourceManager.GetMoney())
        { 
            _cannon.SetActive(true);
            _resourceManager.SpendMoney(_cannon.gameObject.GetComponent<Cannon>().GetCost());
            _placementPanel.SetActive(false);
            _placementPanelActive = false;
            _towerPlaced = true;
            ServiceLocator.Instance.GetService<AudioManager>().PlayClip("SFX","Place_Tower");
        }
        else
        {
            _placementPanel.SetActive(false);
            _placementPanelActive = false;
        }
    }
    
    public void PlaceBallista() 
    {
        _upgradePanel = ServiceLocator.Instance.GetService<HUDController>().GetHUDView().gameObject.transform.Find("UpgradePanel").gameObject;
        _upgradePanel.GetComponent<UpgradePanel>().SetPlacingField(this);
        if (_cannon.gameObject.GetComponent<Tower>().GetCost() <= _resourceManager.GetMoney())
        { 
            _ballista.SetActive(true);
            _resourceManager.SpendMoney(_ballista.gameObject.GetComponent<Ballista>().GetCost());
            _placementPanel.SetActive(false);
            _placementPanelActive = false;
            _towerPlaced = true;
            ServiceLocator.Instance.GetService<AudioManager>().PlayClip("SFX","Place_Tower");
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