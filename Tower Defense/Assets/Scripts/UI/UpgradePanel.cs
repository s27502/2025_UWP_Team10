using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private Tower _tower;
    private PlacingField _placingField;
    [SerializeField] ResourceManager resourceManager;

    public void SetTower(Tower tower)
    {
        _tower = tower;
    }

    public void SetPlacingField(PlacingField placingField)
    {
        _placingField = placingField;
    }

    public void UpgradeDamage()
    {
        //atk++
        Debug.Log("Upgrade Damage");
    }
    public void SellTower()
    {
        resourceManager.AddMoney( (int)(_tower.GetCost() * 0.7f));
        _tower.gameObject.SetActive(false);
        _placingField.SetTowerPlaced(false);
        Close();
    }
    public void UpgradeAtkSpeed()
    {
        //atkspeed++
        Debug.Log("Upgrade Atk Speed");
    }

    public void SetClosest()
    {
        //tower closest strategy
        Debug.Log("SetClosest");
    }

    public void SetStrongest()
    {
        //tower strongest strategy
        Debug.Log("SetStrongest");
    }

    public void Close()
    {
        _tower.setUpgradePanelActive(false);
        _tower = null;
        gameObject.SetActive(false);
    }
}
