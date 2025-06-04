using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolableObject
{
    void Spawn(Vector3 position, Vector3 direction);
    void Despawn();
    void SetPool(IObjectPool pool);
}

