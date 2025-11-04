using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 用于里氏替换装载子类的父类
/// </summary>
public abstract class EventBaseInfo
{
    
}

/// <summary>
/// 用来包裹对应观察者函数委托的类
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventInfo<T> : EventBaseInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

/// <summary>
/// 无参数重载
/// </summary>
public class EventInfo : EventBaseInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventCenter : SingletonBaseManager<EventCenter>
{
    private Dictionary<string, EventBaseInfo> eventDic = new Dictionary<string, EventBaseInfo>();
    
    private EventCenter() { }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventName">事件名字</param>
    public void EventTrigger<T>(string eventName, T info)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions?.Invoke(info);
        }
    }
    
    /// <summary>
    /// 触发无参数事件 
    /// </summary>
    /// <param name="eventName">事件名字</param>
    public void EventTrigger(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions?.Invoke();
        }
    }
    
    /// <summary>
    /// 添加事件监听者
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener<T>(string eventName, UnityAction<T> func)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions += func;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo<T>(func));
        }
    }
    
    /// <summary>
    /// 添加无参数事件监听者 
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="func"></param>
    public void AddEventListener(string eventName, UnityAction func)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions += func;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo(func));
        }
    }

    /// <summary>
    /// 移除事件监听者
    /// </summary>
    /// <param name="eventName"></param>
    public void RemoveEventListener<T>(string eventName, UnityAction<T> func)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions -= func;
        }
    }
    
    /// <summary>
    /// 移除无参数事件监听者 
    /// </summary>
    /// <param name="eventName"></param>
    public void RemoveEventListener(string eventName, UnityAction func)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions -= func;
        }
    }

    /// <summary>
    /// 移除所有事件监听者
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }

    /// <summary>
    /// 指定事件清空
    /// </summary>
    /// <param name="eventName"></param>
    public void Clear(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic.Remove(eventName);
        }
    }
}
