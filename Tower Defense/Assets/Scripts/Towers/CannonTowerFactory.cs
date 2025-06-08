using System;
using UnityEngine;

namespace Towers
{
    public class CannonTowerFactory : AbstractTowerFactory
    {
        private GameObject _cannonPrefab;
        private TowerData _cannonData;
        private String path = "Prefabs/Towers/Cannon";
        public CannonTowerFactory()
        {
            _cannonPrefab = Resources.Load<GameObject>(path);
            _cannonData = _cannonPrefab.GetComponent<Tower>().GetData();
        }

        public override ITower CreateTower(Vector3 position)
        {
            GameObject tower = GameObject.Instantiate(_cannonPrefab);
            tower.transform.position = position;
            return tower.GetComponent<ITower>();
        }

        public override TowerData GetTowerData()
        {
            return _cannonData;
        }
    }
}