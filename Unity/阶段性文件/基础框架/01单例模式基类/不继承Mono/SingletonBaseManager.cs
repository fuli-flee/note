using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 不继承MonoBehaviour的单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonBaseManager<T> where T : class
{
    private static T instance;

    protected static readonly object lockObj = new object();

    //用属性的方法
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        Type type = typeof(T);
                        ConstructorInfo info = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                            Type.EmptyTypes, null);
                        if (info != null)
                        {
                            instance = info.Invoke(null) as T;
                        }
                        else
                        {
                            Debug.LogError("没有得到相应的无参构造函数");
                        }
                    }
                }
            }
            return instance;
        }
    }
}
