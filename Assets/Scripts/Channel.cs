using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Channel : ScriptableObject
{
    private readonly List<UnityAction> _respondActions = new();
    [SerializeField] private List<Object> _listeners = new();

    public void AddListener(UnityAction action, Object listener)
    {
        if (_respondActions.Contains(action))
        {
            Debug.LogWarning("Action is added twice to a channel");
        }
        
        _respondActions.Add(action);
        _listeners.Add(listener);
    }

    public void RemoveListener(UnityAction action, Object listener)
    {
        if (!_respondActions.Contains(action))
        {
            Debug.LogError("Failed to remove listener from channel");
            return;
        }

        _respondActions.Remove(action);
        _listeners.Remove(listener);
    }

    public void Raise()
    {
        foreach (var respond in _respondActions)
            respond.Invoke();
    }
}
