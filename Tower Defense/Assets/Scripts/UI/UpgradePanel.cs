using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private BasicTower _tower;

    public void SetBasicTower(BasicTower tower)
    {
        _tower = tower;
        Debug.Log("Tower set");
        Debug.Log(_tower);
    }

    public void UpgradeDamage()
    {
        //atk++
        Debug.Log("Upgrade Damage");
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
