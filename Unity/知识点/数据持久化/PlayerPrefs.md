[toc]
***
# 一. 概述
数据持久化就是将内存中的数据模型转换为存储模型,以及将存储模型转换为内存中的数据模型的统称

说人话:将游戏数据存储到硬盘,硬盘中数据读取到游戏中,也就是传统意义上的存档

## 1.1 PlayerPrefs是什么
Unity提供的可以用于存储读取玩家数据的公共类

***
# 二. 基本方法
## 2.1 存储相关
PlayerPrefs的数据存储 类似于键值对存储 一个键对应一个值
提供了存储3种数据的方法 `int` `float` `string`
键: string类型 
值：int float string 对应3种API

```CSharp
PlayerPrefs.SetInt("myAge", 18);
PlayerPrefs.SetFloat("myHeight", 180.5f);
PlayerPrefs.SetString("myName", "西蒙");
```
直接调用Set相关方法 只会把数据存到内存里
当游戏结束时 Unity会自动把数据存到硬盘中
如果游戏不是正常结束的 而是崩溃 数据是不会存到硬盘中的
只要调用该方法 就会马上存储到硬盘中
```CSharp
PlayerPrefs.Save();
```

PlayerPrefs是有局限性的 它只能存3种类型的数据
如果你想要存储别的类型的数据 只能降低精度 或者上升精度来进行存储
```CSharp
bool sex = true;
PlayerPrefs.SetInt("sex", sex ? 1 : 0);
```

如果不同类型用同一键名进行存储 会进行覆盖
```CSharp
PlayerPrefs.SetFloat("myAge", 20.2f);
```
## 2.2 读取相关
注意 运行时 只要你Set了对应键值对
即使你没有马上存储Save在本地
也能够读取出信息

- `int`
```CSharp
int age = PlayerPrefs.GetInt("myAge");
print(age);//输出0, 因为myAge这个键被覆盖了, 默认值为0
//前提是 如果找不到myAge对应的值 就会返回函数的第二个参数: 默认值
age = PlayerPrefs.GetInt("myAge", 100);
print(age);
```

- `float`
```CSharp
float height = PlayerPrefs.GetFloat("myHeight", 1000f);
```

- `string`

```CSharp
string name = PlayerPrefs.GetString("myName");
```

第二个参数 默认值 对于我们的作用
就是 在得到没有的数据的时候 就可以用它来进行基础数据的初始化

- 判断数据是否存在
```CSharp
if( PlayerPrefs.HasKey("myName") )
{
    print("存在myName对应的键值对数据");
}
```

## 2.3 删除数据
- 删除指定键值对
```Csharp
PlayerPrefs.DeleteKey("myAge");
```
- 删除所有存储的信息
```CSharp
PlayerPrefs.DeleteAll();
```

***
# 三. 不同平台的存储位置

## 3.1 PlayerPrefs存储的数据存在哪里？
不同平台存储位置不一样

**`Windows`**
PlayerPrefs 存储在 
`HKCU\Software\[公司名称]\[产品名称]` 项下的注册表中
其中公司和产品名称是 在“Project Settings”中设置的名称。

> Win+R => regedit => HKEY_CURRENT_USER => SOFTWARE => Unity => UnityEditor => 公司名称 => 产品名称


**`Android`**
`/data/data/包名/shared_prefs/pkg-name.xml `

**`IOS`**
`/Library/Preferences/[应用ID].plist`

## 3.2 PlayerPrefs数据唯一性
PlayerPrefs中不同数据的唯一性是由key决定的，不同的key决定了不同的数据
同一项目中 如果不同数据key相同 会造成数据丢失
要保证数据不丢失就要建立一个保证key唯一的规则

***
# 四. 补充
[反射相关知识在CSharp高阶笔记中的第9条](/CSharp/知识点/高阶.md)

## 4.1 判断一个类型的对象是否可以让另一个类型为自己分配空间
**`IsAssignableFrom`**

```CSharp
class Father { }

class Son : Father { }

public class Reflection : MonoBehaviour
{
    private void Start()
    {
        Type f = typeof(Father);
        Type s = typeof(Son);
        
        //调用者通过该方法进行判断, 判断是否可以通过传入的类型为自己分配空间 
        if (f.IsAssignableFrom(s))
        {
            Father father = Activator.CreateInstance(s) as Father;
        }
    }
}
```

## 4.2 通过反射获取泛型类型
**`GetGenericArguments`**

```CSharp
void Start()
{
    Dictionary<string, int> dic = new Dictionary<string, int>();
    Type dicType = dic.GetType();
    Type[] types = dicType.GetGenericArguments();
    print(types[0]);//输出 System.String
    print(types[1]);//输出 System.Int32
}

```

## 4.3 导出资源包
> Project窗口 => 右键 => Export Package..