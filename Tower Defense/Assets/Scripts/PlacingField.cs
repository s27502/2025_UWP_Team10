using System;
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

    private void Awake()
    {
        _tower = transform.Find("Tower").gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Object Clicked: " + gameObject.name);
        _tower.SetActive(true);
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