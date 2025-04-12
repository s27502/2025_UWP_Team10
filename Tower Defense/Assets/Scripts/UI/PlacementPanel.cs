using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlacementPanel : MonoBehaviour
{
    private PlacingField _placingField;
    [SerializeField] TMP_Text _cannonPriceText;

    public void SetPlacingField(PlacingField placingField)
    {
        _placingField = placingField; ;
    }

    public void PlaceTower()
    {
        _placingField.PlaceTower();
        gameObject.SetActive(false);
    }

    public void SetCannonPriceText(int price)
    {
        _cannonPriceText.text = price.ToString() + "$";
    }

    public void Close()
    {
        _placingField.SetPlacementPanelActive(false);
        _placingField = null;
        gameObject.SetActive(false);
    }
}
