using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private BasicTower _tower;
    private PlacingField _placingField;
    [SerializeField] ResourceManager resourceManager;

    public void SetBasicTower(BasicTower tower)
    {
        _tower = tower;
        Debug.Log("Tower set");
        Debug.Log(_tower);
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
        Debug.Log("Sell tower");
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
