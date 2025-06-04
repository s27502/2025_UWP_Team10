using System.Collections.Generic;

namespace ObjectPooling
{
    public abstract class ObjectPool : IObjectPool {
        protected IObjectFactory factory;
        protected List<IPoolableObject> available = new List<IPoolableObject>();
        protected int maxPoolSize;

        protected List<IPoolableObject> allObjects = new List<IPoolableObject>();

        public ObjectPool(IObjectFactory factory, int maxSize) {
            this.factory = factory;
            this.maxPoolSize = maxSize;
        }

        public virtual IPoolableObject GetObject()
        {
            if (available.Count > 0) {
                var obj = available[0];
                available.RemoveAt(0);
                return obj;
            }

            if (allObjects.Count < maxPoolSize) {
                var newObj = factory.CreateNew();
                allObjects.Add(newObj);
                return newObj;
            }
            return null;
        }

        public virtual void ReleaseObject(IPoolableObject obj) {
            obj.Despawn();
            available.Add(obj);
        }
    }
}