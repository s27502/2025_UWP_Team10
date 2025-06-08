using UnityEngine;

namespace Towers
{
    public abstract class AbstractTowerFactory
    {
        public abstract ITower CreateTower(Vector3 position);
        public abstract TowerData GetTowerData();
    }
}