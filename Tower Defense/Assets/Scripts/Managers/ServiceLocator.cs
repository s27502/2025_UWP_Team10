using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }

    private readonly Dictionary<Type, object> _services = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate ServiceLocator found. Destroying this one.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        // Wciśnij F9 żeby wyświetlić listę serwisów
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
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
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