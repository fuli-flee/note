using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ABMgr : SingletonAutoMono<ABMgr>
{
    private Dictionary<string, AssetBundle> abDict = new Dictionary<string, AssetBundle>();
    
    //主包
    private AssetBundle mainAB = null;
    //主包配置文件
    private AssetBundleManifest mainManifest = null;

    //AB包路径
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// 主包名
    /// </summary>
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "ios";
#elif UNITY_ANDROID
            return "Android";
# else
            return "PC";
#endif
        }
    }

    /// <summary>
    /// 同步加载AB包
    /// </summary>
    /// <param name="abName">包名</param>
    public void LoadAB(string abName)
         {
             //加载主包
             if (mainAB == null)
             {
                 mainAB = AssetBundle.LoadFromFile(PathUrl + "/" + MainABName);
                 mainManifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
             }
     
             //这个只是为了能复用而声明的
             AssetBundle ab;
             //加载所有ab包依赖
             string[] strs = mainManifest.GetAllDependencies(abName);
             //加载需要使用的包的依赖包
             for (int i = 0; i < strs.Length; ++i)
             {
                 if (!abDict.ContainsKey(strs[i]))
                 {
                     ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                     abDict.Add(strs[i],ab);
                 }
             }
             
             //加载需要使用的包
             if (!abDict.ContainsKey(abName))
             {
                 ab = AssetBundle.LoadFromFile(PathUrl + abName);
                 abDict.Add(abName, ab);
             }
         }

    /// <summary>
    /// 同步加载AB包资源
    /// </summary>
    /// <param name="abName">需要使用的包名</param>
    /// <param name="resName">包中需要使用的资源名</param>
    /// <param name="needInstantiate">是否直接实例化(仅限资源类型为GameObject)</param>
    /// <returns>需要的资源</returns>
    public Object LoadRes(string abName, string resName, bool needInstantiate = true)
    {
        LoadAB(abName);
        
        //返回需要的包中资源
        Object obj = abDict[abName].LoadAsset(resName);
        if (obj is GameObject && needInstantiate)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }
    
    /// <summary>
    /// 同步加载AB包资源
    /// </summary>
    /// <param name="abName">需要使用的包名</param>
    /// <param name="resName">包中需要使用的资源名</param>
    /// <param name="type">资源类型</param>
    /// <param name="needInstantiate">是否直接实例化(仅限资源类型为GameObject)</param>
    /// <returns>需要的资源</returns>
    public Object LoadRes(string abName, string resName, System.Type type, bool needInstantiate = true)
    {
        LoadAB(abName);

        Object obj = abDict[abName].LoadAsset(resName,type);
        if (obj is GameObject && needInstantiate)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }
    
    /// <summary>
    /// 同步加载AB包资源
    /// </summary>
    /// <param name="abName">需要使用的包名</param>
    /// <param name="resName">包中需要使用的资源名</param>
    /// <param name="needInstantiate">是否直接实例化(仅限资源类型为GameObject)</param>
    /// <returns>需要的资源</returns>
    public T LoadRes<T>(string abName, string resName, bool needInstantiate = true)where T: Object
    {
        LoadAB(abName);

        T obj = abDict[abName].LoadAsset<T>(resName);
        if (obj is GameObject && needInstantiate)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }
    
    
    /// <summary>
    /// 异步加载AB包资源
    /// </summary>
    /// <param name="abName">需要使用的包名</param>
    /// <param name="resName">包中需要使用的资源名</param>
    /// <param name="needInstantiate">是否直接实例化(仅限资源类型为GameObject)</param>
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callback, bool needInstantiate = true)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callback, needInstantiate));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callback, bool needInstantiate = true)
    {
        LoadAB(abName);

        AssetBundleRequest abr = abDict[abName].LoadAssetAsync(resName);
        yield return abr;

        if (abr.asset is GameObject && needInstantiate)
        {
            callback(Instantiate(abr.asset));
        }
        else
        {
            callback(abr.asset);
        }
    }
    
    /// <summary>
    /// 异步加载AB包资源
    /// </summary>
    /// <param name="abName">需要使用的包名</param>
    /// <param name="resName">包中需要使用的资源名</param>
    /// <param name="type">资源类型</param>
    /// <param name="callback">回调函数</param>
    /// <param name="needInstantiate">是否直接实例化(仅限资源类型为GameObject)</param>
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callback, bool needInstantiate = true)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, type, callback, needInstantiate));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callback, bool needInstantiate = true)
    {
        LoadAB(abName);

        AssetBundleRequest abr = abDict[abName].LoadAssetAsync(resName,type);
        yield return abr;

        if (abr.asset is GameObject && needInstantiate)
        {
            callback(Instantiate(abr.asset));
        }
        else
        {
            callback(abr.asset);
        }
    }

    /// <summary>
    /// 异步加载AB包资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    /// <param name="needInstantiate"></param>
    /// <typeparam name="T"></typeparam>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callback, bool needInstantiate = true) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callback, needInstantiate));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callback, bool needInstantiate = true) where T : Object
    {
        LoadAB(abName);

        AssetBundleRequest abr = abDict[abName].LoadAssetAsync<T>(resName);
        yield return abr;

        if (abr.asset is GameObject && needInstantiate)
        {
            callback(Instantiate(abr.asset) as T);
        }
        else
        {
            callback((abr.asset) as T);
        }
    }
    
    /// <summary>
    /// 单个包卸载
    /// </summary>
    /// <param name="abName"></param>
    public void UnLoad(string abName)
    {
        if (abDict.ContainsKey(abName))
        {
            abDict[abName].Unload(false);
            abDict.Remove(abName);
        }
    }

    /// <summary>
    /// 卸载所有包
    /// </summary>
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDict.Clear();
        mainAB = null;
        mainManifest = null;
    }
}
