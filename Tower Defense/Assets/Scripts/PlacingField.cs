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
        
        if (_resourceManager == null)
        {
            Debug.LogError("ResourceManager nie został przypisany!");
            Debug.Log("Object Clicked: " + gameObject.name);
            _tower.SetActive(true);
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