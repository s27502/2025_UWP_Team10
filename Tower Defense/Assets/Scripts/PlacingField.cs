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

    private void Awake()
    {
        _tower = transform.Find("Tower").gameObject;
        _resourceManager = ServiceLocator.Instance.GetService<ResourceManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (_tower.gameObject.GetComponent<BasicTower>().GetCost() <= _resourceManager.GetMoney())
        {
            Debug.Log("Object Clicked: " + gameObject.name);
            _tower.SetActive(true);
            _resourceManager.SpendMoney(_tower.gameObject.GetComponent<BasicTower>().GetCost());
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
}