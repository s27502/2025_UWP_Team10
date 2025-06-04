using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool {
    IPoolableObject GetObject();
    void ReleaseObject(IPoolableObject obj);
}

