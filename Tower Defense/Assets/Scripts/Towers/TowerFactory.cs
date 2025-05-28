namespace Towers
{
    public abstract class TowerFactory
    {
        public abstract ITower CreateTower(string towerName);
    }
}