[toc]
***
# 一. 概述
全称:JavaScript对象简谱(JavaScript Object Notation)
Json是国际通用的一种轻量级的数据交换格式
主要在网络通讯中用于传输数据,或本地数据存储和读取
易于人阅读和编写,同时也易于机器解析和生成,并有效地提升网络传输效率

***
# 二. JSON基础语法

## 2.1 基础语法
JSON是一种键值对结构

### 2.1.1 注释

和CSharp一样

### 2.1.2 语法规则
|名称|符号|含义|
|---|---|---|
|大括号|{}|对象|
|中括号|[]|数组|
|冒号|:|键值对对应关系|
|逗号|,|数据分割|
|双引号|""|键名/字符串|

## 2.2 注意事项
1. 如果数据表示对象那么最外层有大括号
2. 一定是键值对形式
3. 键一定是字符串格式
4. 键值对用逗号分开
5. 数组用[]包裹
6. 对象用{}包裹

***
# 三. JsonUtility
JsonUtlity 是Unity自带的用于解析Json的公共类

它可以
1. 将内存中对象序列化为Json格式的字符串
2. 将Json字符串反序列化为类对象

## 3.1 在文件中存读字符串
1. 存储字符串到指定路径文件中
```CSharp
string path = Application.persistentDataPath + "/example.json";
//第一个参数 存储的路径
//第二个参数 存储的字符串内容
//注意：第一个参数 必须是存在的文件路径 如果没有对应文件夹 会报错
File.WriteAllText(path, "这是一个JSON示例文件");
```

2. 在指定路径文件中读取字符串
```CSharp
string str = File.ReadAllText(path);
```

## 3.2 使用JsonUtlity进行序列化
方法：`JsonUtility.ToJson(对象)`

```CSharp
Test1 t = new Test1();

//Jsonutility提供了现成的方法 可以把类对象 序列化为 json字符串
string jsonStr = JsonUtility.ToJson(t);
File.WriteAllText(Application.persistentDataPath + "/Test.json", jsonStr);
```

注意 ：
1. float序列化时看起来会有一些误差
2. **自定义类**需要加上序列化特性`[System.Serializable]`
3. 想要**序列化私有变量** 需要加上特性`[SerializeField]`
4. JsonUtility**不支持字典**
5. JsonUtlity存储null对象不会是null 而是默认值的数据

## 3.3 使用JsonUtlity进行反序列化
`方法：JsonUtility.FromJson(字符串)`

```CSharp
//读取文件中的 Json字符串
string jsonStr = File.ReadAllText(path);
//使用Json字符串内容 转换成类对象
//下面两种方法都行, 用第二种
Test1 t2 = JsonUtility.FromJson(jsonStr,typeof(Test1)) as Test1;
Test1 t3 = JsonUtility.FromJson<Test1>(jsonStr);

//注意：
//如果Json中数据少了，读取到内存中类对象中时不会报错
```

## 3.4 注意事项
- 这里先准备一个json文件如下:
 ```json
 [
     {"hp":1,"speed":10,"volume":100,"scale":15},
     {"hp":2,"speed":20,"volume":200,"scale":25},
     {"hp":3,"speed":30,"volume":300,"scale":35}
 ]
 ```
- 再写一个相应的类
 ```CSharp
 public class ExampleInfo
 {
     public int hp;
     public int speed;
     public int volume;
     public int scale;
 }
 ```
1. JsonUtlity无法直接读取数据集合
```CSharp
jsonStr = File.ReadAllText(Application.persistentDataPath + "/exampleInfo.json");
List<ExampleInfo> exampleInfoList = JsonUtility.FromJson<List<ExampleInfo>>(jsonStr);
```
以上代码会直接报错: 
> ArgumentException: JSON must represent an object type.

问题在于Json文件的格式, Json文件必须严格按照键值对的方式进行编写, 将Json文件改为以下就行了
```json
{
    "list":[
        {"hp":1,"speed":10,"volume":100,"scale":15},
        {"hp":2,"speed":20,"volume":200,"scale":25},
        {"hp":3,"speed":30,"volume":300,"scale":35}
    ]
}
```
那既然json文件改了, 那对于接收`FromJson`这个API的结果就应该发生变化
既然json里相当与用一个对象去包裹了一个数组, 那相应的接收的类也应该要修改
```CSharp
public class ExampleInfoList
{
    public List<ExampleInfo> list;
}

[System.Serializable]
public class ExampleInfo
{
    public int hp;
    public int speed;
    public int volume;
    public int scale;
}
```
此时在稍微修改以下代码, 就可以获取到对应数据了
```CSharp
jsonStr = File.ReadAllText(Application.persistentDataPath + "/exampleInfo.json");
ExampleInfoList exampleInfoList = JsonUtility.FromJson<ExampleInfoList>(jsonStr);
```

2. 文本编码格式需要时UTF-8 不然无法加载

**总结**
1. 必备知识点 —— File存读字符串的方法 `ReadAllText` 和 `WriteAllText`
2. JsonUtlity提供的序列化反序列化方法 `ToJson` 和 `FromJson`
3. 自定义类需要加上序列化特性`[System.Serializable]`
4. 私有保护成员 需要加上`[SerializeField]`
5. JsonUtlity`不支持字典`
6. JsonUtlity不能直接将数据反序列化为数据集合
7. Json文档编码格式必须是 `UTF-8`

***
# 四. LitJson
[下载地址](https://github.com/LitJSON/litjson)

它是一个第三方库，用于处理Json的序列化和反序列化
LitJson是C#编写的，体积小、速度快、易于使用
它可以很容易的嵌入到我们的代码中
只需要将LitJson代码拷贝到工程中即可

## 4.1 使用LitJson进行序列化
方法：`JsonMapper.ToJson(对象)`

```CSharp
Test2 t = new Test2();

string path = Application.persistentDataPath + "/example2.json";
string jsonStr = JsonMapper.ToJson(t);
File.WriteAllText(path,jsonStr);
```

转换出的json文件中字符串会变成以下这样, 这是正常的, 目前我只看到了中文字符串会变成这样
>"name":"\u5C0F\u660E"

注意 ：
1. 相对JsonUtlity**不需要加特性**
2. **不能序列化私有变量**
3. **支持字典类型**,字典的键 建议都是字符串 因为 Json的特点 Json中的键会加上双引号
4. 需要引用LitJson命名空间
5. LitJson可以准确的**保存null类型**

## 4.2 使用LitJson反序列化
方法：`JsonMapper.ToObject(字符串)`

```CSharp
string jsonStr = File.ReadAllText(path);
//方法一: 返回一个JsonData, 该类本质上是键值对, 所以可以通过索引去读取值
JsonData data = JsonMapper.ToObject(jsonStr);
print(data["name"]);

//方法二: 通过泛型转换, 更加方便
Test2 t2 = JsonMapper.ToObject<Test2>(jsonStr);
```

注意 ：
1. 类结构需要**无参构造函数**，否则反序列化时报错
    就比如我的Test2和Student类是这样的
    ```CSharp
    public class Test2
    {
        //这两个字典用于讲下面的第二点
        public Dictionary<int, string> dic;
        public Dictionary<string, string> dic2;

        public Student2 s1;
    }

    public class Student2
    {
        public int age;
        public string name;

        public Student2(int age, string name)
        {
            this.age = age;
            this.name = name;
        }
    }
    ```
    而这里面的Student没有无参构造函数, 反序列化就会直接报错
    </br>

2. 虽然支持字典, 但是键在使用为数值时会有问题, 需要使用字符串类型
    - 上面在序列化的时候也提到了, 由于Json的键必须是字符串, 所以对于Dictionary<int,string>这样的数据结构来说是会报错的, 所以要么在使用的时候只用以string为键的字典; 要么去写一个方法自己去处理Json中的键(这种方法肯定很麻烦)

## 4.3 注意事项
将之前我先准备一个示例和一个相应的类
```json
[
    {"hp":1,"speed":10,"volume":100,"scale":15},
    {"hp":2,"speed":20,"volume":200,"scale":25},
    {"hp":3,"speed":30,"volume":300,"scale":35}
]
```
```CSharp
public class ExampleInfo
{
    public int hp;
    public int speed;
    public int volume;
    public int scale;
}
```

1. LitJson可以直接读取数据集合
```CSharp
string jsonStr = File.ReadAllText(Application.persistentDataPath + "/exampleInfo.json");
List<ExampleInfo2> infos2 = JsonMapper.ToObject<List<ExampleInfo2>>(jsonStr);
```

2. 文本编码格式需要是UTF-8 不然无法加载

**总结**
1. LitJson提供的序列化反序列化方法 JsonMapper.ToJson 和 ToObject<>
2. LitJson无需加特性
3. LitJson不支持私有变量
4. LitJson支持字典序列化反序列化
5. LitJson可以直接将数据反序列化为数据集合
6. LitJson反序列化时 自定义类型需要无参构造
7. Json文档编码格式必须是UTF-8

***
# 五. JsonUtility与LitJson

## 5.1 相同点
1. 他们都是用于Json的序列化反序列化
2. Json文档编码格式必须是UTF-8
3. 都是通过静态类进行方法调用

## 5.2 不同点
1. JsonUtlity是Unity自带，LitJson是第三方需要引用命名空间
2. JsonUtlity使用时自定义类需要加特性, LitJson不需要
3. JsonUtlity支持私有变量(加特性), LitJson不支持
4. JsonUtlity不支持字典, LitJson支持(但是键只能是字符串)
5. JsonUtlity不能直接将数据反序列化为数据集合(数组字典), LitJson可以
6. JsonUtlity对自定义类不要求有无参构造，LitJson需要
7. JsonUtlity存储空对象时会存储默认值而不是null，LitJson会存null

## 5.3 如何选择两者
根据实际需求, 建议使用LitJson
原因：LitJson不用加特性，支持字典，支持直接反序列化为数据集合，存储null更准确