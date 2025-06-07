using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : SingletonDoNotDestroy<ServiceLocator>
{
    private readonly Dictionary<Type, object> _services = new();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            DebugServices();
        }
    }

    public void Register<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            Debug.LogWarning($"Service of type {type.Name} is already registered. Replacing it.");
        }
        _services[type] = service;
    }

    public T GetService<T>() where T : class
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return service as T;
        }

        Debug.LogError($"Service of type {type.Name} is not registered.");
        return null;
    }

    public void UnregisterService<T>()
    {
        var type = typeof(T);
        _services.Remove(type);
    }

    public void DebugServices()
    {
        if (_services.Count == 0)
        {
            Debug.Log("No services are currently registered.");
            return;
        }

        Debug.Log("=== Registered Services ===");
        foreach (var service in _services)
        {
            Debug.Log($"Type: {service.Key.Name}, Instance: {service.Value}");
        }
        Debug.Log("===========================");
    }
}