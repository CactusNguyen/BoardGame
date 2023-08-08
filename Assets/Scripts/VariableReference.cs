using System;
using UnityEngine;

[Serializable]
public class VariableReference<T>
{
    [SerializeField] private bool _useConstant = true;
    [SerializeField] private T _constantValue;
    [SerializeField] private VariableObject<T> _variable;

    public VariableReference()
    { }

    public VariableReference(T value)
    {
        _useConstant = true;
        _constantValue = value;
    }

    public T Value
    {
        get => _useConstant ? _constantValue : _variable.Value;
        set
        {
            if (_useConstant)
                _constantValue = value;
            else
                _variable.Value = value;
        }
    }
}

[Serializable]
public class IntegerReference : VariableReference<int>
{
    public static implicit operator int(IntegerReference reference)
    {
        return reference.Value;
    }
}

[Serializable]
public class BoolReference : VariableReference<bool>
{
    public static implicit operator bool(BoolReference reference)
    {
        return reference.Value;
    }
}

[Serializable]
public class FloatReference : VariableReference<float>
{
    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }
}