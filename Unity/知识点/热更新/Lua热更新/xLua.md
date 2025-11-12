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

涉及的lua脚本
- Test.lua
    ```lua
    --无参无返回
    testFun = function()
        print("无参无返回")
    end

    --有参有返回
    testFun2 = function(a)
        print("有参有返回")
        return a + 1
    end

    --多返回
    testFun3 = function(a)
        print("多返回值")
        return 1, 2, false, "123", a
    end

    --变长参数
    testFun4 = function(a, ...)
        print("变长参数")
        print(a)
        arg = {...}
        for k,v in pairs(arg) do
            print(k,v)
        end
    end
    ```

### 2.5.1 无参无返回值
用委托去接收获取的全局函数
```CSharp
public delegate void CustomCall();

void Start()
{
    LuaMgr.Instance.Init();
    LuaMgr.Instance.DoLuaFile("Main");
    
    CustomCall call = LuaMgr.Instance.Global.Get<CustomCall>("testFun");
    call();
}
```

共有四种方式接收返回值
```CSharp
//委托
public delegate void CustomCall();
CustomCall call = LuaMgr.GetInstance().Global.Get<CustomCall>("testFun");

//Unity自带委托
UnityAction ua = LuaMgr.GetInstance().Global.Get<UnityAction>("testFun");

//C#提供的委托
Action ac = LuaMgr.GetInstance().Global.Get<Action>("testFun");

//Xlua提供的一种 获取函数的方式 少用
LuaFunction lf = LuaMgr.GetInstance().Global.Get<LuaFunction>("testFun");
```

### 2.5.2 有参有返回值
对于有参有返回值的函数, 需要对声明的委托加上`[CSharpCallLua]`的特性
并重新生成一次Xlua相关代码 `Xlua => Generated Code`

```CSharp
[CSharpCallLua]
public delegate int CustomCall2(int a);

void Start()
{
    CustomCall2 call2 = LuaMgr.GetInstance().Global.Get<CustomCall2>("testFun2");
    print(call2(10));
}
```
自己声明的委托其实挺麻烦的, 所以我们常用的一般是下面的C#自带的泛型委托
```CSharp
//C#自带的泛型委托 方便我们使用
Func<int, int> sFun = LuaMgr.GetInstance().Global.Get<Func<int, int>>("testFun2");
Debug.Log("有参有返回：" + sFun(20));

//Xlua提供的
LuaFunction lf2 = LuaMgr.GetInstance().Global.Get<LuaFunction>("testFun2");
Debug.Log("有参有返回：" + lf2.Call(30)[0]);
```

### 2.5.3 多返回值
第一个返回值为默认返回值
用ref或者out去接收返回值, 也可以用 LuaFunction 去接收

- out
    ```CSharp
    [CSharpCallLua]
    public delegate int CustomCall3(int a, out int b, out bool c, out string d, out int e);

    void Start()
    {
        CustomCall3 call3 = LuaMgr.GetInstance().Global.Get<CustomCall3>("testFun3");
        int b;
        bool c;
        string d;
        int e;
        Debug.Log("第一个返回值：" + call3(100, out b, out c, out d, out e));
        Debug.Log(b + "_" + c + "_" + d + "_" + e);
    }
    ```

- ref
    ```CSharp
    [CSharpCallLua]
    public delegate int CustomCall4(int a, ref int b, ref bool c, ref string d, ref int e);

    void Start()
    {
        CustomCall4 call4 = LuaMgr.GetInstance().Global.Get<CustomCall4>("testFun3");
        int b1 = 0;
        bool c1 = true;
        string d1 = "";
        int e1 = 0;
        Debug.Log("第一个返回值：" + call4(200, ref b1, ref c1, ref d1, ref e1));
        Debug.Log(b1 + "_" + c1 + "_" + d1 + "_" + e1);
    }
    ```
- LuaFunction
    ```CSharp
    LuaFunction lf3 = LuaMgr.GetInstance().Global.Get<LuaFunction>("testFun3");
    object[] objs = lf3.Call(1000);
    for( int i = 0; i < objs.Length; ++i )
    {
        Debug.Log("第" + i + "个返回值是：" + objs[i]);
    }
    ```

### 2.5.4 变长参数

- 委托
    ```CSharp
    [CSharpCallLua]
    //变长参数的类型 是根据实际情况来定的
    //声明为 object[] 当然是最省事的, 但是定好类型后可以少一些拆装箱
    public delegate void CustomCall5(string a, params object[] args);

    void Start()
    {
        CustomCall5 call5 = LuaMgr.GetInstance().Global.Get<CustomCall5>("testFun4");
        call5("123", 1, 2, 3, 4, 5, 566, 7, 7, 8, 9, 99);
    }
    ```
- LuaFunction
    ```CSharp
    LuaFunction lf4 = LuaMgr.GetInstance().Global.Get<LuaFunction>("testFun4");
    lf4.Call("456", 6, 7, 8, 99, 1);
    ```
***
## 2.6 List和Dictionary映射table
- 涉及到的lua脚本
    ```lua
    testList = {"123", "123", true, 1, 1.2}

    testDic = {
        ["1"] = 1,
        [true] = 1,
        [false] = true,
        ["123"] = false
    }
    ```

- list
    ```CSharp
    //不指定类型用Object
    List<object> list3 = LuaMgr.GetInstance().Global.Get<List<object>>("testList2");
    ```
- Dictionary
    ```CSharp
    Dictionary<object, object> dic3 = LuaMgr.GetInstance().Global.Get<Dictionary<object, object>>("testDic2");
    ```
***
## 2.7 类映射table
- 涉及到的lua脚本
    ```lua
    testClas = {
        testInt = 2,
        testBool = true,
        testFloat = 1.2,
        testString = "123",

        -- 嵌套类
        testInClass = {
            testInInt = 5
        },

        -- 嵌套接口
        testInInterface = {
            testInString = "扶离"
        },

        testFun = function()
            print("123123123")
        end
    }
    ```
- 先声明一个对应的类
    在这个类中去声明成员变量
    名字一定要和 Lua那边的一样
    需声明为公共的, 私有和保护 没办法赋值
    这个自定义中的 变量 可以更多也可以更少
    如果变量比 lua中的少 就会忽略它
    如果变量比 lua中的多 不会赋值 也会忽略
    ```CSharp
    public class CallLuaClass
    {
        public int testInt;
        public bool testBool;
        public float testFloat;
        public float testString;
        public UnityAction testFun;

        public int i; //这个变量在接取时找不到lua代码中的对应变量, 会赋予其默认值 0

        public void Test()
        {
            Debug.Log(testInt);
        }
    }

    void Start()
    {
        CallLuaClass obj = LuaMgr.GetInstance().Global.Get<CallLuaClass>("testClas");
    }
    ```
- 嵌套类
    ```CSharp
    public class CallLuaClass
    {
        public CallLuaInClass testInClass;
    }

    public class CallLuaInClass
    {
        public int testInInt;
    }

    public class Lecture04_CallFunction : MonoBehaviour
    {
        void Start()
        {
            LuaMgr.Instance.Init();
            LuaMgr.Instance.DoLuaFile("Main");

            CallLuaClass obj = LuaMgr.Instance.Global.Get<CallLuaClass>("testClas");
            
            Debug.Log("嵌套：" + obj.testInClass.testInInt); // 输出 5
        }
    }
    ```

***
## 2.8 接口映射table
- 涉及到的lua脚本和上面一样
- 接口中是不允许有成员变量的 所以用属性来接收
    接口和类规则一样 其中的属性多了少了 不影响结果 无非就是忽略他们
    嵌套几乎和类一样 无非 是要遵循接口的规则
    ```CSharp
    [CSharpCallLua]
    public interface ICSharpCallInterface
    {
        int testInt { get; set; }
        bool testBool { get; set; }
        float testFloat { get; set; }
        string testString { get; set; }
        UnityAction testFun { get; set; }
        float testFloat222 { get; set; }
    }

    void Start()
    {
        ICSharpCallInterface obj = LuaMgr.GetInstance().Global.Get<ICSharpCallInterface>("testClas");

        //接口拷贝 是引用拷贝 改了值 lua表中的值也变了
        obj.testInt = 10000;
        ICSharpCallInterface obj2 = LuaMgr.GetInstance().Global.Get<ICSharpCallInterface>("testClas");
        Debug.Log(obj2.testInt); //输出 1000
    }
    ```
- 接口拷贝时为引用拷贝, 也就是拷贝时只是将指针指向了同一内存空间, 因此当一对象的内容发生改变, 另一对象就会跟着改变
- 而以上除接口外的拷贝都是值拷贝, 也就是拷贝不会影响另一个对象的内存空间
</br>

- 嵌套接口
```CSharp
[CSharpCallLua]
public interface ICSharpCallInterface 
{
    IOtherInterface testInInterface { get; set; }
}

[CSharpCallLua]
public interface IOtherInterface
{
    string testInString { get; set; } 
}

void Start()
{
    LuaMgr.Instance.Init();
    LuaMgr.Instance.DoLuaFile("Main");
    
    ICSharpCallInterface obj = LuaMgr.Instance.Global.Get<ICSharpCallInterface>("testClas");

    print(obj.testInInterface.testInString); //输出 扶离
}
```

***
## 2.9 LuaTable映射table
不建议使用LuaTable和LuaFunction, 因为效率低

- 涉及的lua脚本和上面一致
- 拷贝方式为引用拷贝
```CSharp
void Start()
{
    LuaTable table = LuaMgr.GetInstance().Global.Get<LuaTable>("testClas");
    Debug.Log(table.Get<int>("testInt"));
    //调用函数
    table.Get<LuaFunction>("testFun").Call();


    //改
    table.Set("testInt", 55);
    table.Dispose();
}
```
- table用完后一定要记得销毁

***
## 2.10 CSharpCallLua特性的使用场景
1. 自定义委托
2. 接口

***
# 三. Lua调用C#
C#没有办法直接访问C# 一定是先从C# 调用Lua脚本后
才把核心逻辑交给Lua编写

## 3.1 类
lua中使用C#的类 固定套路
`CS.命名空间.类名`
Unity的类 比如 GameObject Transform等等 —— CS.UnityEngine.类名
```lua
-- 通过C#中的类 实例化一个对象 lua中没有new 
-- 所以我们直接 类名括号就是实例化对象
-- 默认调用的 相当于就是无参构造
local obj1 = CS.UnityEngine.GameObject()
```

不过为了方便, 我们都会这么写
```lua
GameObject = CS.UnityEngine.GameObject

-- 调用
local obj1 = GameObject("fuli")
```

### 3.1.1 注意点

1. 类的静态对象, 直接用 `.` 来调用
    ```lua
    local obj2 = GameObject.Find("fuli")
    ```

2. 得到对象中的成员变量, 直接 `对象.成员变量` 即可
    ```lua
    obj2.transform.position
    ```
3. 如果使用对象中的 成员方法 一定要用`:`
    ```lua
    obj2.transform:Translate(Vector3.right)
    ```
4. 对于自定义的类, 在使用时注意命名空间就行
    ```CSharp
    public class Test
    {
        public void Speak(string str)
        {
            Debug.Log("Test: " + str);
        }
    }

    namespace Fuli
    {
        public class Test2
        {
            public void Speak(string str) 
            {
                Debug.Log("Test2: " + str);
            }
        }
    }
    ```
    在lua中调用为
    ```lua
    local t = CS.Test()
    t:Speak("说话1")

    local t2 = CS.Fuli.Test2()
    t2:Speak("说话2")
    ```
5. 继承了Mono的类, 它们是不能直接new的
    - 平时我们在C#脚本中为一个gameobject添加脚本是
        ```CSharp
        gameObject.AddComponent<Rigidbody>();
        ```
    - 但是Lua不支持这种无参的泛型函数
    - 所以我们只能用另一种重载
        ```CSharp
        gameObject.AddComponent(typeof(Rigidbody));
        ```
    - 对应到lua中就是
        ```lua
        --这里的typeof是xlua提供的用于获取类型的API
        obj2:AddComponent(typeof(CS.UnityEngine.Rigidbody))
        ```
***
## 3.2 枚举
枚举的调用规则 和 类的调用规则是一样的
`CS.命名空间.枚举名.枚举成员`

```lua
PrimitiveType = CS.UnityEngine.PrimitiveType
GameObject = CS.UnityEngine.GameObject

--创建一个枚举类型中的Cube
local obj = GameObject.CreatePrimitive(PrimitiveType.Cube)
```

**`自定义枚举`**
和类一样, 在使用时注意命名空间就行
```CSharp
enum E_MyEnum{
    Idle,Move,Atk
}
```
lua中调用
```lua
E_MyEnum =  CS.E_MyEnum
```
**`枚举转换`**
```lua
--数值转枚举
local a = E_MyEnum.__CastFrom(1)
print(a) -- 输出 Move: 1
--字符串转枚举 
local b = E_MyEnum.__CastFrom("Atk")
print(b) -- 输出 Atk: 2
```
***
## 3.3 数组 List 字典
涉及的C#代码
```CSharp
public class container
{
    public int[] array = new int[5] { 1, 2, 3, 4, 5 };
    public List<int> list = new List<int>();
    public Dictionary<int, string> dict = new Dictionary<int, string>();
}
```
### 3.3.1 XLua 里的 userdata 是什么？
简单说：
- 当你在 Lua 中获取一个 C# 对象（比如 CS.container 实例、C# 数组、List、Dictionary）时，Lua 本身不认识这些 C# 类型，所以 XLua 会把它们包装成一种叫「userdata」的 Lua 类型。
- 你可以把 userdata 理解为「C# 对象在 Lua 里的 “代理”」—— 你在 Lua 中操作这个 userdata，本质就是通过 XLua 桥接，操作背后的 C# 原对象。

```lua
local obj = CS.container() -- 创建 C# container 实例
print(type(obj)) -- 输出：userdata（说明 obj 是包裹 C# container 的 userdata）
print(type(obj.array)) -- 输出：userdata（array 是 C# int[]，也被包装成 userdata）
print(type(obj.list)) -- 输出：userdata（List<int> 同样是 userdata）
```

Lua 本身只有 8 种基础类型（nil、boolean、number、string、table、function、thread、userdata），根本没有「C# 数组」「C# List」「C# Dictionary」这些类型。

当你在 Lua 中写 local csharpArray = obj.array 时：
- 背后的 obj.array 是 C# 里的 int[] 数组（真实存在于 C# 内存中）；
- XLua 不能把 C# 数组直接变成 Lua 原生的 table（两者底层结构完全不同），所以只能做一个「代理壳子」—— 这就是 userdata；
- 你在 Lua 中操作 userdata，本质是通过 XLua 告诉 C#：“帮我调用一下这个 C# 数组的 Length 属性”“帮我修改这个 C# List 的第 0 个元素”。

简单说：你操作的不是 Lua 原生对象，而是 C# 对象的「远程控制端」，所以必须按 C# 的规则发指令。

**结论**：C# 的引用类型（类实例、数组、集合等）在 Lua 中都会变成 userdata，值类型（int、float、Vector3 等）会转成 Lua 原生类型（number、table）。你操作 userdata 时，就按 XLua 约定的规则调用 C# 的属性 / 方法即可。

### 3.3.2 数组
- 会涉及到的CSharp代码
    ```CSharp
    //Array的源码
    public static System.Array CreateInstance(System.Type elementType, int length) { throw null; }
    ```
- 长度
    ```lua
    local obj = CS.container()

    print(obj.array,Length) -- 5
    ```
- 元素
    ```lua
    print(obj.array[0]) -- 1
    ```
- 遍历
    ```lua
    for i=0,obj.array.Length-1 do
        print(obj.array[i])
    end
    ```
- 创建数组
    我们可以使用C#中的Array类中的静态函数CreateInstance来创建数组
    ```lua
    --创建一个int类型长度为10的数组
    local arr = CS.System.Array.CreateInstance(typeof(CS.System.Int32), 10)
    ```
### 3.3.3 List
- 需要注意的是成员方法需要用 `:` 
</br>

- 添加元素
    ```lua
    obj.list:Add(1)
    ```
- 长度
    ```lua
    obj.list.Count
    ```
- 遍历
    ```lua
    for i=0,obj.list.Count - 1 do
        print(obj.list[i])
    end
    ```
- 创建List对象
    ```lua
    --老版本的创建方式不同哦

    --这里只是得到一个List<string>泛型类
    local List_String = CS.System.Collections.Generic.List(CS.System.String)
    --这样才是创建了一个List对象
    local list = List_String()
    ```
### 3.3.4 dictionary
- 增加元素
    ```lua
    obj.dic:Add(1, "123")
    ```
- 遍历
    ```lua
    for k,v in pairs(obj.dic) do
        print(k,v)
    end
    ```
- 创建字典
    ```lua
    -- 和上面的List一样, 先创建出一个Dictionary<string, Vector3>泛型类
    local Dic_String_Vector3 = CS.System.Collections.Generic.Dictionary(CS.System.String, CS.UnityEngine.Vector3)
    -- 再实例化
    local dic2 = Dic_String_Vector3()
    dic2:Add("123", CS.UnityEngine.Vector3.right)
    for i,v in pairs(dic2) do
        print(i,v)
    end

    print(dic2["123"])
    ```
    运行上面代码, 你会发现, 最后一句输出的nil
    正确的写法如下
    ```lua
    -- 固定写法
    -- 通过键获取值
    print(dic2:get_Item("123"))
    -- 输出  (1.00, 0.00, 0.00): 1065353216 

    -- 设置键值对
    dic2:set_Item("123", nil)
    -- 输出 (0.00, 0.00, 0.00): 0

    --也可以用字典中的方法
    print(dic2:TryGetValue("123"))
    --输出 true	(1.00, 0.00, 0.00): 1065353216
    ```
***
## 3.4 拓展方法
[拓展方法相关见CSharp核心的2.12](/CSharp/知识点/核心.md)
- 涉及的 C# 代码
    ```CSharp
    //想要调用 C# 中某个类的拓展方法 那一定要在拓展方法的静态类前面加上LuaCallCSharp特性
    [LuaCallCSharp]
    public static class Tools
    {
        public static void Move(this Person obj)
        {
            Debug.Log(obj.name + "移动");
        }
    }

    public class Person
    {
        public string name = "Fuli";

        public void Speak(string str){ Debug.Log(str); }

        public static void Eat() { Debug.Log("吃东西"); }
    }
    ```

- 使用静态方法
    `CS.命名空间.类名.静态方法名()`
    ```lua
    Person.Eat()
    ```
- 使用成员方法
    ```lua
    obj:Speak("六百六十六")
    ```
- 使用拓展方法
    ```lua
    obj:Move()
    ```

建议 Lua 中要使用的类, 都加上该特性, 可以提升性能
如果不加该特性, 除了拓展方法对应的类, 其他类都不会报错
但是Lua是通过反射的机制去调用CSharp的类, 效率较低