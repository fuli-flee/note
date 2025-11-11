[toc]
***
# 一. 什么是热更新
游戏或软件更新时, 无需重新下载客户端进行安装
而是在应用程序启动的情况下, 在内部进行的资源或者代码的更新

***

# 二. C#调用Lua

## 2.1 Lua解析器
- Lua解析器 能够让我们在Unity中执行Lua
一般情况下 保持它的唯一性
    ```CSharp
    LuaEnv env = new LuaEnv();
    ```
- 执行Lua语言
    ```CSharp
    env.DoString("print('Lua代码')"); //输出 LUA: Lua代码
    ```
- 执行一个Lua脚本 Lua知识点 ：多脚本执行 require
    默认寻找脚本的路径 是在 Resources下 并且 因为在这里
    大概率是通过 Resources.Load 去加载Lua脚本  所以只支持 txt bytes等等
    所以Lua脚本 后缀要加一个txt
    ```CSharp
    env.DoString("require('Main')");
    ```
    - 在resource下的lua文件名为 
        ```txt
        Main.lua.txt
        ```
- 帮助我们清楚Lua中我们没有手动释放的对象 垃圾回收
    帧更新中定时执行 或者 切场景时执行
    ```CSharp
    env.Tick();
    ```
- 销毁Lua解析器 
    ```CSharp
    env.Dispose();
    ```

***
## 2.2 Lua文件加载重定向
xlua提供的一个 路径重定向 的方法
允许我们自定义 加载 Lua文件的规则
当我们执行Lua语言 require 时 相当于执行一个lua脚本
它就会 执行 我们自定义传入的这个函数
```CSharp
env.AddLoader(函数名);
```

该方法本质上是一个委托
相关源码为, 看不懂没关系, 你只要知道他传入的参数是一个返回值为byte[]委托就行了
```CSharp
public delegate byte[] CustomLoader(ref string filepath);

internal List<CustomLoader> customLoaders = new List<CustomLoader>();

public void AddLoader(CustomLoader loader)
{
    customLoaders.Add(loader);
}
```
所以我们在设置自定义函数时也要遵循对应规则
```CSharp
private byte[] MyCustomLoader(ref string filePath)
{
    string path = Application.dataPath + "/Lua/" + filePath + ".lua";
    if (File.Exists(path))
    {
        return File.ReadAllBytes(path);
    }
    else
    {
        Debug.LogWarning($"MyCustomLoader重定向失败, 文件名为: {filePath}");
    }

    return null;
}
```
AddLoader 方法会先执行 MyCustomLoader 方法, 若返回值为空, 则执行默认方法(去Resources文件夹下找), 如果还是找不到才会报错
***
## 2.3 lua解析管理器

构建一个唯一的lua解析器

需要注意的是:
- 在使用前别忘了打AB包, 路径为 `AssetBundle/PC`
- 压缩方式选 `LZ4` , 包名叫 `lua`
- 打包时xLua报错尝试 `XLua => Clear Generated Code`

ABMgr相关[详情看这](/Unity/阶段性文件/热更新/AB包/ABMgr.cs), 里面注释写的很清楚

**完整代码**
```CSharp
using System.IO;
using UnityEngine;
using XLua;

/// <summary>
/// Lua解析管理器
/// </summary>
public class LuaMgr : SingletonBaseManager<LuaMgr>
{
    private LuaEnv luaEnv;
    
    private LuaMgr(){}

    /// <summary>
    /// 获取lua脚本中的_G表
    /// </summary>
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (luaEnv != null)
        {
            return;
        }

        luaEnv = new LuaEnv();
        
        luaEnv.AddLoader(MyCustomLoader);
        luaEnv.AddLoader(MyCustomABLoader);
    }

    /// <summary>
    /// 执行lua语言
    /// </summary>
    /// <param name="str"></param>
    public void Dostring(string str)
    {
        if (luaEnv == null)
        {
            Debug.LogError("解析器未初始化");
        }

        luaEnv.DoString(str);
    }

    /// <summary>
    /// 释放lua 垃圾
    /// </summary>
    public void Tick()
    {
        if (luaEnv == null)
        {
            Debug.LogError("解析器未初始化");
        }
        
        luaEnv.Tick();
    }

    /// <summary>
    /// 销毁解析器
    /// </summary>
    public void Dispose()
    {
        if (luaEnv == null)
        {
            Debug.LogError("解析器未初始化");
        }
        
        luaEnv.Dispose();
        luaEnv = null;
    }

    /// <summary>
    /// 执行lua文件
    /// </summary>
    /// <param name="fileName"></param>
    public void DoLuaFile(string fileName)
    {
        string str = $"require('{fileName}')";
        Dostring(str);
    }

    /// <summary>
    /// lua的AB包加载
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private byte[] MyCustomABLoader(ref string filePath)
    {
        TextAsset lua = ABMgr.Instance.LoadRes<TextAsset>("lua",filePath + ".lua");
        if (lua != null)
        {
            return lua.bytes;
        }
        else
        {
            Debug.LogWarning($"MyCustomLoader重定向失败, 文件名为: {filePath}");
        }
        return null;
    }

    
    /// <summary>
    /// lua文件加载
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private byte[] MyCustomLoader(ref string filePath)
    {
        string path = Application.dataPath + "/Lua/" + filePath + ".lua";
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.LogWarning($"MyCustomLoader重定向失败, 文件名为: {filePath}");
        }

        return null;
    }
}
```
***
## 2.4 全局变量获取

涉及到的两个lua脚本
- Main.lua
    ```lua
    print("执行主脚本")
    require('Test')
    ```
- Test.lua
    ```lua
    print('执行Test脚本')

    testNumber = 1
    testBool = true
    testFloat = 1.2
    testString = "123"
    
    local testLocal = 10
    ```

**获取全局变量值**
- 只要给定的容器有足够空间可以装,便可以赋值
```CSharp
void Start()
{
    LuaMgr.Instance.Init();
    LuaMgr.Instance.DoLuaFile("Main");
    
    int i = LuaMgr.Instance.Global.Get<int>("testNumber");
    print(i); // 输出 1
    
    double d = LuaMgr.Instance.Global.Get<double>("testFloat");
    Debug.Log("testFloat_double:" + d);// 输出 testFloat_double:1.2
}
```

**修改全局变量值**
```CSharp
LuaMgr.Instance.Global.Set("testNumber", 50);
        
int i1 = LuaMgr.Instance.Global.Get<int>("testNumber");
print(i1);  //输出 50
```

***
## 2.5 全局函数获取

用委托去接收获取的全局函数