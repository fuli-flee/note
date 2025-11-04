using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    //容器上限 场景上同时存在的对象的上限个数
    private int maxNum;
    
    //未使用的对象的容器
    private Stack<GameObject> dataStack = new Stack<GameObject>();
    //使用中的对象的容器
    private List<GameObject> usedList = new List<GameObject>();
    
    private GameObject rootObj;

    public int Count => dataStack.Count;

    public int UsedCount => usedList.Count;
    public bool NeedCreate => usedList.Count < maxNum;

    public PoolData(string name,GameObject obj, GameObject usedObj)
    {
        if (PoolMgr.isOpenLayout)
        {
            rootObj = new GameObject(name);
            rootObj.transform.SetParent(obj.transform);
        }
        
        PushUsedList(usedObj);

        
        PoolObj poolObj = usedObj.GetComponent<PoolObj>();
        if (obj == null)
        {
            Debug.LogError("请为使用缓存池功能的预设体对象挂载PoolObj脚本, 用于设置数量上限");
            return;
        }
        maxNum = poolObj.maxNum;
    }

    public GameObject Pop()
    {
        GameObject data;

        if (Count > 0)
        {
            data = dataStack.Pop();
            PushUsedList(data);
        }

        else
        {
            data = usedList[0];
            usedList.RemoveAt(0);
            usedList.Add(data);
        }
        
        if (PoolMgr.isOpenLayout)
            data.transform.SetParent(null);
        data.SetActive(true);
        return data;
    }

    public void Push(GameObject data)
    {
        data.SetActive(false);
        if (PoolMgr.isOpenLayout)
            data.transform.SetParent(rootObj.transform);
        dataStack.Push(data);
        usedList.Remove(data);
    }

    public void PushUsedList(GameObject usedObj)
    {
        usedList.Add(usedObj);
    }
}

/// <summary>
/// 缓存池模块管理器
/// </summary>
public class PoolMgr : SingletonBaseManager<PoolMgr>
{
    public GameObject poolObj;
    //是否开启自动布局
    public static bool isOpenLayout = true;
    
    private PoolMgr() { }
    private Dictionary<string, PoolData> poolDict = new Dictionary<string, PoolData>();
    /// <summary>
    /// 获得缓存池中的对象, 若没有则加载Resource文件夹下资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public GameObject GetObj(string path)
    {
        GameObject obj;
        if (poolObj == null && isOpenLayout)
            poolObj = new GameObject("Pool");
        
        if (!poolDict.ContainsKey(path) || 
            (poolDict[path].Count == 0 && poolDict[path].NeedCreate))
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(path));
            obj.name = path;

            if (!poolDict.ContainsKey(path))
            {
                poolDict.Add(path,new PoolData(path,poolObj, obj));
            }

            else
            {
                poolDict[path].PushUsedList(obj);
            }
        }
        else 
        {
            obj = poolDict[path].Pop();
        }
        return obj;
    }

    /// <summary>
    /// 将对象失活并存入到缓存池
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        poolDict[obj.name].Push(obj);
    }

    /// <summary>
    /// 清除缓存池中数据, 主要用于切换场景
    /// </summary>
    public void ClearPool()
    { 
        poolDict.Clear();
        poolObj = null;
    }
}
