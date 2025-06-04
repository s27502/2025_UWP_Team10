namespace ObjectPooling
{
    public class EnemyPool : ObjectPool
    {
        public EnemyPool(IObjectFactory factory, int maxSize) : base(factory, maxSize) { }
    }
}