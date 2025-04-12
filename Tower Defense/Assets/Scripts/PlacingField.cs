using System;
using System.Resources;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlacingField : MonoBehaviour, 
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private GameObject _tower;
    private ResourceManager _resourceManager;
    [SerializeField] private GameObject _placementPanel;
    private bool _placementPanelActive;
    private bool _towerPlaced;

    private void Awake()
    {
        _tower = transform.Find("Tower").gameObject;
        _placementPanelActive = false;
        _towerPlaced = false;
        _resourceManager = ServiceLocator.Instance.GetService<ResourceManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_placementPanelActive && !_towerPlaced)
        {
            _placementPanel.transform.position = eventData.position;
            _placementPanel.SetActive(true);
            _placementPanelActive = true;
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
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

    }
}