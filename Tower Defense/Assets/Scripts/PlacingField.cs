using System;
using System.Resources;
using Towers;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlacingField : MonoBehaviour,
    IPointerClickHandler
{
    private ResourceManager _resourceManager;
    private GameObject _placementPanel;
    private bool _placementPanelActive;
    private bool _towerPlaced;
    private GameObject _upgradePanel;

    private AbstractTowerFactory _cannonFactory;
    private AbstractTowerFactory _ballistaFactory;
    private ITower _placedTower;

    private void Awake()
    {
        _cannonFactory = new CannonTowerFactory();
        _ballistaFactory = new BallistaTowerFactory();
        _placementPanelActive = false;
        _towerPlaced = false;
        _resourceManager = ServiceLocator.Instance.GetService<ResourceManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_placementPanelActive && !_towerPlaced)
        {
            _placementPanel = ServiceLocator.Instance.GetService<HUDController>().GetHUDView().gameObject.transform
                .Find("PlacementPanel").gameObject;
            _placementPanel.transform.position = eventData.position;
            _placementPanel.SetActive(true);
            _placementPanel.GetComponent<PlacementPanel>().SetPlacingField(this);

            _placementPanel.GetComponent<PlacementPanel>().SetCannonPriceText(
                _cannonFactory.GetTowerData().Cost);
            _placementPanel.GetComponent<PlacementPanel>().SetBallistaPriceText(
                _ballistaFactory.GetTowerData().Cost);

            _placementPanelActive = true;
        }
    }

    public void PlaceCannon()
    {
        if (_cannonFactory.GetTowerData().Cost <= _resourceManager.GetMoney())
        {
            TryPlaceTower(_cannonFactory);
        }

        SetPanelInactive();
    }

    public void PlaceBallista()
    {
        if (_ballistaFactory.GetTowerData().Cost <= _resourceManager.GetMoney())
        {
            TryPlaceTower(_ballistaFactory);
        }

        SetPanelInactive();
    }

    private void TryPlaceTower(AbstractTowerFactory factory)
    {
        _resourceManager.SpendMoney(factory.GetTowerData().Cost);
        // _placedTower = factory.CreateTower(transform.position);
        Tower tower = (Tower)factory.CreateTower(transform.position);
        tower.SetPlacingField(this);
        _placedTower = tower;
        _towerPlaced = true;


        ServiceLocator.Instance.GetService<AudioManager>().PlayClip("SFX", "Place_Tower");
    }

    private void SetPanelInactive()
    {
        _placementPanel.SetActive(false);
        _placementPanelActive = false;
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