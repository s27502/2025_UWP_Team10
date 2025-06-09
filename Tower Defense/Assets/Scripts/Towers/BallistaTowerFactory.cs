using System;
using UnityEngine;

namespace Towers
{
    public class BallistaTowerFactory : AbstractTowerFactory
    {
        private GameObject _ballistaPrefab;
        private String path = "Prefabs/Towers/Ballista";
        
        private TowerData _ballistaData;
        public BallistaTowerFactory()
        {
            _ballistaPrefab = Resources.Load<GameObject>(path);
            _ballistaData = _ballistaPrefab.GetComponent<Tower>().GetData();
        }

        public override ITower CreateTower(Vector3 position)
        {
            GameObject tower = GameObject.Instantiate(_ballistaPrefab);
            tower.transform.position = position;
            return tower.GetComponent<ITower>();
        }

        public override TowerData GetTowerData()
        {
            return _ballistaData;
        }
    }
}