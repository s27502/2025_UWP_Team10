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
        if (_tower == null || _placingField == null)
        {
            Debug.Log("nulllll");
            return;
        }
        var price = _tower.GetCost() * 0.3f;
        if (!(resourceManager.GetMoney() >= price))return;
        resourceManager.AddMoney( (int)(-price));
        _tower.setUpgradePanelActive(false);
        _tower.IncreaseDamage(5f);
        Close();
    }
    
    public void SellTower()
    {
        if (_tower == null || _placingField == null)
        {
            Debug.Log("nulllll");
            return;
        }
        
        resourceManager.AddMoney( (int)(_tower.GetCost() * 0.7f));
        _tower.setUpgradePanelActive(false);
        _placingField.SetTowerPlaced(false);
        Destroy(_tower.gameObject);
        Close();
        
    }
    public void UpgradeAtkSpeed()
    {
        if (_tower == null || _placingField == null)
        {
            Debug.Log("nulllll");
            return;
        }

        var price = _tower.GetCost() * 0.4f;
        if(!(resourceManager.GetMoney() >= price))return;
        resourceManager.AddMoney( (int)(-price));
        _tower.setUpgradePanelActive(false);
        _tower.IncreaseAttackSpeed(1f);
        Close();
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
