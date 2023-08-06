using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class SerializableCallbackBase<TReturn> : SerializableCallbackBase
{
    protected InvokableCallbackBase<TReturn> Func;

    public override void ClearCache()
    {
        base.ClearCache();
        Func = null;
    }

    protected InvokableCallbackBase<TReturn> GetPersistentMethod()
    {
        Type[] types = new Type[ArgRealTypes.Length + 1];
        Array.Copy(ArgRealTypes, types, ArgRealTypes.Length);
        types[^1] = typeof(TReturn);

        Type genericType = null;
        switch (types.Length)
        {
            case 1:
                genericType = typeof(InvokableCallback<>).MakeGenericType(types);
                break;
            case 2:
                genericType = typeof(InvokableCallback<,>).MakeGenericType(types);
                break;
            case 3:
                genericType = typeof(InvokableCallback<,,>).MakeGenericType(types);
                break;
            case 4:
                genericType = typeof(InvokableCallback<,,,>).MakeGenericType(types);
                break;
            case 5:
                genericType = typeof(InvokableCallback<,,,,>).MakeGenericType(types);
                break;
            default:
                throw new ArgumentException(types.Length + "args");
        }

        return Activator.CreateInstance(genericType, Target, MethodName) as
            InvokableCallbackBase<TReturn>;
    }
}

/// <summary> An inspector-friendly serializable function </summary>
[Serializable]
public abstract class SerializableCallbackBase : ISerializationCallbackReceiver
{
    /// <summary> Target object </summary>
    public Object Target
    {
        get => _target;
        set
        {
            _target = value;
            ClearCache();
        }
    }

    /// <summary> Target method name </summary>
    public string MethodName
    {
        get => _methodName;
        set
        {
            _methodName = value;
            ClearCache();
        }
    }

    public object[] Args
    {
        get { return args ??= _args.Select(x => x.GetValue()).ToArray(); }
    }

    private object[] args;

    public Type[] ArgTypes
    {
        get { return argTypes ??= _args.Select(x => Arg.RealType(x.argType)).ToArray(); }
    }

    public Type[] argTypes;

    public Type[] ArgRealTypes
    {
        get
        {
            return _argRealTypes ??= _args.Select(x => Type.GetType(x._typeName)).ToArray();
        }
    }

    private Type[] _argRealTypes;

    [SerializeField] protected Object _target;
    [SerializeField] protected string _methodName;
    [SerializeField] protected Arg[] _args;
    [SerializeField] protected bool _dynamic;
#pragma warning disable 0414
    [SerializeField] private string _typeName;
#pragma warning restore 0414

    [SerializeField] private bool dirty;

#if UNITY_EDITOR
    protected SerializableCallbackBase()
    {
        _typeName = base.GetType().AssemblyQualifiedName;
    }
#endif

    public virtual void ClearCache()
    {
        argTypes = null;
        args = null;
    }

    public void SetMethod(Object target, string methodName, bool dynamic, params Arg[] args)
    {
        _target = target;
        _methodName = methodName;
        _dynamic = dynamic;
        _args = args;
        ClearCache();
    }

    protected abstract void Cache();

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (dirty)
        {
            ClearCache();
            dirty = false;
        }
#endif
    }

    public void OnAfterDeserialize()
    {
#if UNITY_EDITOR
        _typeName = base.GetType().AssemblyQualifiedName;
#endif
    }
}

[System.Serializable]
public struct Arg
{
    public enum ArgType
    {
        Unsupported,
        Bool,
        Int,
        Float,
        String,
        Object
    }

    public bool boolValue;
    public int intValue;
    public float floatValue;
    public string stringValue;
    public Object objectValue;
    public ArgType argType;
    public string _typeName;

    public object GetValue()
    {
        return GetValue(argType);
    }

    public object GetValue(ArgType type)
    {
        switch (type)
        {
            case ArgType.Bool:
                return boolValue;
            case ArgType.Int:
                return intValue;
            case ArgType.Float:
                return floatValue;
            case ArgType.String:
                return stringValue;
            case ArgType.Object:
                return objectValue;
            default:
                return null;
        }
    }

    public static Type RealType(ArgType type)
    {
        switch (type)
        {
            case ArgType.Bool:
                return typeof(bool);
            case ArgType.Int:
                return typeof(int);
            case ArgType.Float:
                return typeof(float);
            case ArgType.String:
                return typeof(string);
            case ArgType.Object:
                return typeof(Object);
            default:
                return null;
        }
    }

    public static ArgType FromRealType(Type type)
    {
        if (type == typeof(bool)) return ArgType.Bool;
        else if (type == typeof(int)) return ArgType.Int;
        else if (type == typeof(float)) return ArgType.Float;
        else if (type == typeof(String)) return ArgType.String;
        else if (typeof(Object).IsAssignableFrom(type)) return ArgType.Object;
        else return ArgType.Unsupported;
    }

    public static bool IsSupported(Type type)
    {
        return FromRealType(type) != ArgType.Unsupported;
    }
}