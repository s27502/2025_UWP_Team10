using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    private Dictionary<Type, object> _services;
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; 
            _services = new Dictionary<Type, object>();
        }
        
    }

    public void Register<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type)) {
            throw new InvalidOperationException
                ($"Service of type {type.Name} is already registered.");
        }
        _services[type] = service;
    }
    
    public T GetService<T>() where T : class {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service)) {
            return service as T;
        }
        throw new InvalidOperationException
            ($"Service of type {type.Name} is not registered.");
    }
    public void UnregisterService<T>() {
        var type = typeof(T);
        if (_services.ContainsKey(type)) {
            _services.Remove(type);
        }
    }
}
