// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    [SerializeField] private List<T> Items = new();

    public void Add(T thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
        else
        {
            Debug.LogError("Add an object in set twice");
        }
    }

    public void Remove(T thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);
        else
        {
            Debug.LogError("Failed to remove an object from set. Set doesn't contain the object");
        }
    }
}