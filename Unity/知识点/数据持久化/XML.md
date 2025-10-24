[toc]
***
# 一. 概述
全称:可拓展标记语言(EXtensible Markup Language)
XML是国际通用的
它是被设计来用于传输和存储数据的一种文本特殊格式
文件后缀一般为`.xml`
***
# 二. XML基础语法
## 2.1 注释
```xml
<!--注释-->
```
## 2.2 固定语法

```XML
<?xml version="1.0" encoding="UTF-8"?>
```

## 2.3 基本语法
xml的基本语法就是 `<元素标签> </元素标签>` 配对出现

必须要有一个根节点

## 2.4 基本规则
1. 每个元素都必须有关闭标签
2. 元素命名规则基本遵照 C# 中变量名命名规则
3. XML 标签对大小写敏感
4. XML 文档必须有根元素
5. 特殊的符号应该用实体引用

|符号|实体|解释|
|----|---|---|
|&lt|<|小于|
|&gt|>|大于|
|&amp|&|和|
|&apos|'|单引号|
|&quot|"|双引号|

***
# 三. 属性

## 3.1 语法
属性就是在元素标签后面空格 添加的内容
注意:属性必须用引号包裹 单引号双引号都可以


```xml
<!--属性 通过空格隔开 属性名=引号包裹的内容-->
<Friend name="小明"age='8'>我的朋友</Friend>
```
如果使用属性记录信息不想使用元素记录可以如下这样写
```xml
<Fater name = "爸爸" age ="50"/>
```
***
# 四. xml文件存放位置
- 只读不写的XML文件可以放在Resources或者StreamingAssets文件夹下
- 动态存储的XML文件放在Application.persistentDataPath路径下

***
# 五. CSharp读取XML文件
C#读取XML的方法有以下几种 
1. XmlDocument    (把数据加载到内存中，方便读取)
2. XmlTextReader  (以流形式加载，内存占用更少，但是是单向只读，使用不是特别方便，除非有特殊需求，否则不会使用)
3. Linq           

## 5.1 读取xml文件信息
通过XmlDocument读取xml文件 有两个API
1. 直接根据xml字符串内容 来加载xml文件
存放在Resorces文件夹下的xml文件加载处理
```CSharp
XmlDocument xml = new XmlDocument();
TextAsset asset = Resources.Load<TextAsset>("TestXml");
//通过这个方法 就能够翻译字符串为xml对象
xml.LoadXml(asset.text);
```

2. 通过xml文件的路径去进行加载
```CSharp
xml.Load(Application.streamingAssetsPath + "/TestXml.xml");
```

## 5.2 读取元素和属性信息
节点信息类
- XmlNode 单个节点信息类

节点列表信息
- XmlNodeList 多个节点信息类

### 5.2.1 获取单个子节点
```CSharp
//获取xml当中的根节点
XmlNode root = xml.SelectSingleNode("Root");
//再通过根节点 去获取下面的子节点
XmlNode nodeName = root.SelectSingleNode("name");
//如果想要获取节点包裹的元素信息 直接 .InnerText
print(nodeName.InnerText);
```

### 5.2.2 获取属性
```CSharp
XmlNode nodeItem = root.SelectSingleNode("Item");
//第一种方式 直接 中括号获取信息
print(nodeItem.Attributes["id"].Value);
print(nodeItem.Attributes["num"].Value);
//第二种方式 
print(nodeItem.Attributes.GetNamedItem("id").Value);
print(nodeItem.Attributes.GetNamedItem("num").Value);
```

### 5.2.3 获取一个节点下的同名节点
```CSharp
XmlNodeList friendList = root.SelectNodes("Friend");

//遍历方式一：迭代器遍历
foreach (XmlNode item in friendList)
{
    print(item.SelectSingleNode("name").InnerText);
    print(item.SelectSingleNode("age").InnerText);
}
```

```CSharp
//遍历方式二：通过for循环遍历
//通过XmlNodeList中的 成员变量 Count可以得到 节点数量
for (int i = 0; i < friendList.Count; i++)
{
    print(friendList[i].SelectSingleNode("name").InnerText);
    print(friendList[i].SelectSingleNode("age").InnerText);
}
```
</br>

**总结**
1. 读取XML文件
    ```CSharp
    XmlDocument xml = new XmlDocument();
    ```
    - 读取文本方式2-xml.Load(传入路径)
    - 读取文本方式1-xml.LoadXml(传入xml文本字符串)
</br>

2. 读取元素和属性
   - 获取单个节点 : XmlNode node = xml.SelectSingleNode(节点名)
   - 获取多个节点 : XmlNodeList nodeList = xml.SelectNodes(节点名)
</br>

3. 获取节点元素内容：node.InnerText
</br>

4. 获取节点元素属性：
    - item.Attributes["属性名"].Value
    - item.Attributes.GetNamedItem("属性名").Value
</br>

5. 通过迭代器遍历或者循环遍历XmlNodeList对象 可以获取到各单个元素节点

***
# 六. 存储修改Xml文件
## 6.1 决定存储在哪个文件夹下
注意： 存储xml文件 在Unity中一定是使用各平台都可读可写可找到的路径
 1. Resources 可读 不可写 打包后找不到  ×
 2. Application.streamingAssetsPath 可读 PC端可写 找得到  ×
 3. Application.dataPath 打包后找不到  ×
 4. Application.persistentDataPath 可读可写找得到   √

## 6.2 存储xml文件
- 关键类 XmlDocument 用于创建节点 存储文件
- 关键类 XmlDeclaration 用于添加版本信息
- 关键类 XmlElement 节点类

存储有5步
1. 创建文本对象
```CSharp
XmlDocument xml = new XmlDocument();
```

2. 添加固定版本信息
   - 这一句代码 相当于就是创建`<?xml version="1.0" encoding="UTF-8"?>`这句内容
    ```CSharp
    XmlDeclaration xmlDec = xml.CreateXmlDeclaration("1.0", "UTF-8", "");
    ```
   - 创建完成过后 要添加进入 文本对象中
    ```CSharp
    xml.AppendChild(xmlDec);
    ```

3. 添加根节点
```CSharp
XmlElement root = xml.CreateElement("Root");
xml.AppendChild(root);
```

4. 为根节点添加子节点
    加了一个 name子节点
```CSharp
XmlElement name = xml.CreateElement("name");
name.InnerText = "扶离";
root.AppendChild(name);

XmlElement listInt = xml.CreateElement("listInt");
for (int i = 1; i <= 3; i++)
{
    XmlElement childNode = xml.CreateElement("int");
    childNode.InnerText = i.ToString();
    listInt.AppendChild(childNode);
}
root.AppendChild(listInt);

XmlElement itemList = xml.CreateElement("itemList");
for (int i = 1; i <= 3; i++)
{
    XmlElement childNode = xml.CreateElement("Item");
    //添加属性
    childNode.SetAttribute("id", i.ToString());
    childNode.SetAttribute("num", (i * 10).ToString());
    itemList.AppendChild(childNode);
}
root.AppendChild(itemList);
```

5.保存
```CSharp
xml.Save(path);
```

## 6.3 修改xml文件
```CSharp
//1.先判断是否存在文件
if( File.Exists(path) )
{
    //2.加载后 直接添加节点 移除节点即可
    XmlDocument newXml = new XmlDocument();
    newXml.Load(path);

    //修改就是在原有文件基础上 去移除 或者添加
    
    //移除
    XmlNode node;
    //这种是一种简便写法 通过/来区分父子关系
    node = newXml.SelectSingleNode("Root/atk");
    //得到自己的父节点
    XmlNode root2 = newXml.SelectSingleNode("Root");
    //移除子节点方法
    root2.RemoveChild(node);

    //添加节点
    XmlElement speed = newXml.CreateElement("moveSpeed");
    speed.InnerText = "20";
    root2.AppendChild(speed);

    //改了记得存
    newXml.Save(path);
}
```

**总结**
1. 路径选取
在运行过程中存储 只能往可写且能找到的文件夹存储
故 选择了Application.persistentDataPath
</br>

2. 存储xml关键类
   - XmlDocument  文件  
      - 创建节点 CreateElement
      - 创建固定内容方法 CreateXmlDeclaration
      - 添加节点 AppendChild
      - 保存 Save
   - XmlDeclaration 版本
   - XmlElement 元素节点  
      设置属性方法SetAttribute
</br>

3. 修改
- RemoveChild移除节点
可以通过 /的形式 来表示 子节点的子节点 
</br>

***
# 七. CSharp中XML序列化

## 7.1 什么是序列化和反序列化
序列化：把对象转化为可传输的字节序列过程称为序列化
反序列化：把字节序列还原为对象的过程称为反序列化

说人话：
序列化就是把想要**存储的内容转换为字节序列**用于存储或传递
反序列化就是把**存储或收到的字节序列信息解析读取出来使用**

## 7.2 xml序列化
1. 第一步准备一个数据结构类
```CSharp
Lesson1Test lt = new Lesson1Test();
```
2. 进行序列化
   - 关键知识点
     - XmlSerializer 用于序列化对象为xml的关键类
     - StreamWriter 用于存储文件  
     - using 用于方便流对象释放和销毁

```CSharp
//第一步：确定存储路径
string path = Application.persistentDataPath + "/Lesson1Test.xml";

//第二步：结合 using知识点 和 StreamWriter这个流对象 来写入文件
// 括号内的代码：写入一个文件流 如果有该文件 直接打开并修改 如果没有该文件 直接新建一个文件
// using 的新用法 括号当中包裹的声明的对象 会在 大括号语句块结束后 自动释放掉 
// 当语句块结束 会自动帮助我们调用 对象的 Dispose这个方法 让其进行销毁
// using一般都是配合 内存占用比较大 或者 有读写操作时  进行使用的 
using ( StreamWriter stream = new StreamWriter(path) )
{

    //第三步：进行xml文件序列化
    XmlSerializer s = new XmlSerializer(typeof(Lesson1Test));
    //这句代码的含义 就是通过序列化对象 对我们类对象进行翻译 将其翻译成我们的xml文件 写入到对应的文件中
    //第一个参数 ： 文件流对象
    //第二个参数: 想要备翻译 的对象
    //注意 ：翻译机器的类型 一定要和传入的对象是一致的 不然会报错
    s.Serialize(stream, lt);
}
```

**总结**
- 先写两个类用来测试
```CSharp
public class Test1
{
    public int testPublic;
    private int testPrivate;
    protected int testProtected;
    internal int testInternal;

    public string testPUblicStr = "fuli";

    public int testPro { get; set; }

    public Test2 testClass = new Test2();

    public int[] arrayInt = new int[3]{1,2,3};
    public List<int> listInt = new List<int>{1,2,3,4};
    public List<Test2> listItem = new List<Test2>{new Test2(), new Test2()};
}

public class Test2
{
    [XmlAttribute()]
    public int test1 = 1;
    [XmlAttribute()]
    public float test2 = 1.1f;
    [XmlAttribute()]
    public bool test3 = true;
}
```

- 上面的知识点相关的注释太乱, 其实要用到的代码就6行
```CSharp
Test1 test1 = new Test1();//你要序列化的类
string path = Application.persistentDataPath + "/Test_Serialize.xml";//你要存储的路径

using ( StreamWriter stream = new StreamWriter(path) )
{
    XmlSerializer s = new XmlSerializer(typeof(Test1));
    s.Serialize(stream, test1);
}
```
- 通过以上代码就可以得到对应路径的xml文件,然后你对照上面的两个测试类看看就懂了
- [这里附一个截图工具](https://zh.snipaste.com/)

```xml
<?xml version="1.0" encoding="utf-8"?>
<Test1 xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <testPublic>0</testPublic>
  <testPUblicStr>fuli</testPUblicStr>
  <testClass test1="1" test2="1.1" test3="true" />
  <arrayInt>
    <int>1</int>
    <int>2</int>
    <int>3</int>
  </arrayInt>
  <listInt>
    <int>1</int>
    <int>2</int>
    <int>3</int>
    <int>4</int>
  </listInt>
  <listItem>
    <Test2 test1="1" test2="1.1" test3="true" />
    <Test2 test1="1" test2="1.1" test3="true" />
  </listItem>
  <testPro>0</testPro>
</Test1>
```


序列化流程
1. 有一个想要保存的类对象
2. 使用XmlSerializer 序列化该对象
3. 通过StreamWriter 配合 using将数据存储 写入文件
</br>

注意 ：
1. 只能序列化公共成员
2. 不支持字典序列化
3. 可以通过特性修改节点信息 或者设置属性信息
4. Stream相关要配合using使用

***
# 八. CSharp中的Xml反序列化

关键知识
 1. using 和 StreamReader
 2. XmlSerializer 的 Deserialize反序列化方法
```CSharp

string path = Application.persistentDataPath + "/Test_Serialize.xml";
if( File.Exists(path) )
{
    //读取文件
    using (StreamReader reader = new StreamReader(path))
    {
        //产生了一个 序列化反序列化的翻译机器
        XmlSerializer s = new XmlSerializer(typeof(Test1));
        Test1 lt = s.Deserialize(reader) as Test1;
    }
}
```

**总结**
1. 判断文件是否存在 File.Exists
2. 文件流获取 StreamReader reader = new StreamReader(path)
3. 根据文件流 XmlSerializer通过Deserialize反序列化 出对象

注意：List对象 如果有默认值 反序列化时 不会清空 会往后面添加, 那么就会导致重复数据

***
# 九. IXmlSerializable接口
C# 的XmlSerializer 提供了可拓展内容 
可以让一些不能被序列化和反序列化的特殊类能被处理
让特殊类继承 IXmlSerializable 接口 实现其中的方法即可

## 9.1 实现成员

该接口需实现的三个成员方法

```CSharp
public XmlSchema GetSchema() { }

public void ReadXml(XmlReader reader) { } 

public void WriteXml(XmlWriter writer) { }
```
- 返回结构

```CSharp
public XmlSchema GetSchema()
{
    //这个先不进行学习, 返回空就行了
    return null;
}
```

### 9.1.1 方法介绍
只要继承了该接口, 就只会执行以下两个方法中的逻辑
- 反序列化时 会自动调用的方法 `public void ReadXml(XmlReader reader)`
- 序列化时 会自动调用的方法 `public void WriteXml(XmlWriter writer)`

以下为测试类, 结合下面的知识点进行讲解
```CSharp
public class Test : IXmlSerializable
{
    public int testInt;
    public string testStr;

    public void ReadXml(XmlReader reader) 
    {

    } 

    public void WriteXml(XmlWriter writer) 
    {

    }
}
```

### 9.1.2 读写属性
- 读属性
```CSharp
public void ReadXml(XmlReader reader)
{
    testInt = int.Parse(reader["testInt"]);
    testStr = reader["testStr"];
} 
```

- 写属性
```CSharp
public void WriteXml(XmlWriter writer) 
{
    writer.WriteAttributeString("testInt",testInt.ToString());
    writer.WriteAttributeString("testStr",testStr);
}
```

### 9.1.3 读写节点
执行下面写节点的代码后, xml文件里的内容如下:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Test3>
  <testInt>0</testInt>
  <testStr>123</testStr>
</Test3>
```

- 读节点
  - 方法一
    ```CSharp
    public void WriteXml(XmlWriter writer) 
    {
        reader.Read();
        reader.Read();
        testInt = int.Parse(reader.Value);
        reader.Read();
        reader.Read();
        reader.Read();
        testStr = reader.Value;
        reader.Read();
    }
    ```
    其实挺简单的, 一张图就能看懂, 读到对应的地方去获取相应的值就行了

    <center>
    
    ![alt text](/Unity/图片/XML/XML10-23_17-34-34.jpg)

    </center>

  - 方法二
    ```CSharp
    public void WriteXml(XmlWriter writer) 
    {
        while(reader.Read())
        {
            //读到节点时就暂停
            if( reader.NodeType == XmlNodeType.Element )
            {
                switch (reader.Name)
                {
                    case "testInt":
                        reader.Read();
                        this.testInt = int.Parse(reader.Value);
                        break;
                    case "testStr":
                        reader.Read();
                        this.testStr = reader.Value;
                        break;
                }
            }
        }
    }
    ```

- 写节点
```CSharp
public void WriteXml(XmlWriter writer) 
{
    writer.WriteElementString("testInt",testInt.ToString());
    writer.WriteElementString("testStr",testStr);
}
```

### 9.1.4 读写包裹节点
执行下面写节点的代码后, xml文件内容如下:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Test3>
  <testInt>
    <int>0</int>
  </testInt>
  <testStr>
    <string>123</string>
  </testStr>
</Test3>
```

- 读包裹节点
```CSharp
public void ReadXml(XmlReader reader)
{
    //reader默认为根节点, 执行下面代码后就到了testInt节点上
    reader.Read();
    
    XmlSerializer s = new XmlSerializer(typeof(int));
    reader.ReadStartElement("testInt");
    testInt = (int)s.Deserialize(reader);
    reader.ReadEndElement();

    XmlSerializer s2 = new XmlSerializer(typeof(string));
    reader.ReadStartElement("testStr");
    testStr = s2.Deserialize(reader).ToString();
    reader.ReadEndElement();
}
```

- 写包裹节点
```CSharp
public void WriteXml(XmlWriter writer) 
{
    XmlSerializer s = new XmlSerializer(typeof(int));
    writer.WriteStartElement("testInt");
    s.Serialize(writer,testInt);
    writer.WriteEndElement();
    
    XmlSerializer s2 = new XmlSerializer(typeof(string));
    writer.WriteStartElement("testStr");
    s2.Serialize(writer,testStr);
    writer.WriteEndElement();
}
```

***
# 十. 让Dictionary支持xml序列和反序列化
1. 我们没办法修改C#自带的类
2. 那我们可以重写一个类 继承Dictionary 然后让这个类继承序列化拓展接口IXmlSerializable
3. 实现里面的序列化和反序列化方法即可

## 10.1 具体实现
[具体的可以直接导入我写好的](/Unity/小项目/数据持久化/XML/XML_SerizlizerDictionary.unitypackage)

下面是源码

```CSharp
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class SerizlizerDictionary<TKey, TValue> : Dictionary<TKey, TValue> ,IXmlSerializable
{
    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        reader.Read();
        
        XmlSerializer keyReader = new XmlSerializer(typeof(TKey));
        XmlSerializer valueReader = new XmlSerializer(typeof(TValue));

        TKey key;
        TValue value;
        
        while (reader.NodeType != XmlNodeType.EndElement)
        {
            key = (TKey)keyReader.Deserialize(reader);
            value = (TValue)valueReader.Deserialize(reader);
            
            this.Add(key,value);
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        XmlSerializer keySer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSer = new XmlSerializer(typeof(TValue));

        foreach (var kv in this)
        {
            keySer.Serialize(writer,kv.Key);
            valueSer.Serialize(writer,kv.Value);
        }
    }
}
```