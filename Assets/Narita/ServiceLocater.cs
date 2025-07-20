using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocater
{
    static Dictionary<Type, object> _services = new();

    public static void Set<T>(T service)
    {
        if (_services.ContainsKey(typeof(T)))
        {
            _services[typeof(T)] = service;
        }
        else
        {
            _services.Add(typeof(T), service);
        }
    }

    public static object Get<T>()
    {
        if (_services.TryGetValue(typeof(T), out var service))
        {
            return (T)service;
        }

        return default;
    }
}
