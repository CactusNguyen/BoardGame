using System;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public abstract class SerializableCallback<TReturn> : SerializableCallbackBase<TReturn>
{
    public TReturn Invoke()
    {
        if (Func == null) Cache();
        if (_dynamic)
        {
            InvokableCallback<TReturn> call = Func as InvokableCallback<TReturn>;
            return call.Invoke();
        }
        else
        {
            return Func.Invoke(Args);
        }
    }

    protected override void Cache()
    {
        if (_target == null || string.IsNullOrEmpty(_methodName))
        {
            Func = new InvokableCallback<TReturn>(null, null);
        }
        else
        {
            if (_dynamic)
            {
                Func = new InvokableCallback<TReturn>(Target, MethodName);
            }
            else
            {
                Func = GetPersistentMethod();
            }
        }
    }
}

public abstract class SerializableCallback<T0, TReturn> : SerializableCallbackBase<TReturn>
{
    public TReturn Invoke(T0 arg0)
    {
        if (Func == null) Cache();
        if (_dynamic)
        {
            InvokableCallback<T0, TReturn> call = Func as InvokableCallback<T0, TReturn>;
            return call.Invoke(arg0);
        }
        else
        {
            return Func.Invoke(Args);
        }
    }

    protected override void Cache()
    {
        if (_target == null || string.IsNullOrEmpty(_methodName))
        {
            Func = new InvokableCallback<T0, TReturn>(null, null);
        }
        else
        {
            if (_dynamic)
            {
                Func = new InvokableCallback<T0, TReturn>(Target, MethodName);
            }
            else
            {
                Func = GetPersistentMethod();
            }
        }
    }
}

public abstract class SerializableCallback<T0, T1, TReturn> : SerializableCallbackBase<TReturn>
{
    public TReturn Invoke(T0 arg0, T1 arg1)
    {
        if (Func == null) Cache();
        if (_dynamic)
        {
            InvokableCallback<T0, T1, TReturn> call = Func as InvokableCallback<T0, T1, TReturn>;
            return call.Invoke(arg0, arg1);
        }
        else
        {
            return Func.Invoke(Args);
        }
    }

    protected override void Cache()
    {
        if (_target == null || string.IsNullOrEmpty(_methodName))
        {
            Func = new InvokableCallback<T0, T1, TReturn>(null, null);
        }
        else
        {
            if (_dynamic)
            {
                Func = new InvokableCallback<T0, T1, TReturn>(Target, MethodName);
            }
            else
            {
                Func = GetPersistentMethod();
            }
        }
    }
}

public abstract class SerializableCallback<T0, T1, T2, TReturn> : SerializableCallbackBase<TReturn>
{
    public TReturn Invoke(T0 arg0, T1 arg1, T2 arg2)
    {
        if (Func == null) Cache();
        if (_dynamic)
        {
            InvokableCallback<T0, T1, T2, TReturn> call = Func as InvokableCallback<T0, T1, T2, TReturn>;
            return call.Invoke(arg0, arg1, arg2);
        }
        else
        {
            return Func.Invoke(Args);
        }
    }

    protected override void Cache()
    {
        if (_target == null || string.IsNullOrEmpty(_methodName))
        {
            Func = new InvokableCallback<T0, T1, T2, TReturn>(null, null);
        }
        else
        {
            if (_dynamic)
            {
                Func = new InvokableCallback<T0, T1, T2, TReturn>(Target, MethodName);
            }
            else
            {
                Func = GetPersistentMethod();
            }
        }
    }
}

public abstract class SerializableCallback<T0, T1, T2, T3, TReturn> : SerializableCallbackBase<TReturn>
{
    public TReturn Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        if (Func == null) Cache();
        if (_dynamic)
        {
            InvokableCallback<T0, T1, T2, T3, TReturn> call = Func as InvokableCallback<T0, T1, T2, T3, TReturn>;
            return call.Invoke(arg0, arg1, arg2, arg3);
        }
        else
        {
            return Func.Invoke(Args);
        }
    }

    protected override void Cache()
    {
        if (_target == null || string.IsNullOrEmpty(_methodName))
        {
            Func = new InvokableCallback<T0, T1, T2, T3, TReturn>(null, null);
        }
        else
        {
            if (_dynamic)
            {
                Func = new InvokableCallback<T0, T1, T2, T3, TReturn>(Target, MethodName);
            }
            else
            {
                Func = GetPersistentMethod();
            }
        }
    }
}