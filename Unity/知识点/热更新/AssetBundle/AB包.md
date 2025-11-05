[toc]
***
# 一. 概念

特定于平台的资产压缩包，有点类似压缩文件
资产包括：模型、贴图、预设体、音效、材质球等等

## 1.1 作用
相对Resources下的资源, AB包更好管理资源
<center>

![alt text](/Unity/图片/热更新/AssetBundle/AssetBundle11-04_21-39-16.jpg)

</center>

1. 减小包体大小
    - 压缩资源
    - 减少初始包大小
2. 热更新
   - 资源热更新
   - 脚本热更新

<center>

![alt text](/Unity/图片/热更新/AssetBundle/AssetBundle11-04_21-40-54.jpg)

</center>


# 二. 生成AB包资源文件
生成AB包的两种方法
1. Unity编辑器开发，自定义打包工具
2. 官方提供好的打包工具：Asset Bundle Browser

## 2.1 AB包生成的文件

以下例子基于我只打了一个名为`Model`的包, 且路径为`AssetBundle/PC`的前提下

AB包生成的文件可以分为以下种类:
- AB包文件
- manifest文件
  - AB包文件信息
  - 当加载时，提供了关键信息: 资源信息，依赖关系，版本信息等等
- 关键AB包（和目录名一样的包）
  - 主包
  - AB包依赖关键信息(manifest文件)

<center>

![alt text](/Unity/图片/热更新/AssetBundle/AssetBundle11-05_10-14-48.jpg)

</center>

## 2.2 AssetBundleBrowser参数相关

**`build（构建页签）`**
<center>

![alt text](/Unity/图片/热更新/AssetBundle/AssetBundle11-05_10-20-11.jpg)

</center>

- BuildTarget:目标平台
- Output Path:目标输出路径
- Clear Folders：是否清空文件夹 重新打包
- Copy To StreamingAssets：是否拷贝到StreamingAssets
- Compression: 压缩方式
  - NoCompression: 不压缩，解压快，包较大 不推荐
  - LZMA: 压缩最小，解压慢; 缺点：用一个资源 要解压所有
  - LZ4: 压缩，相对LZMA大一点点; 建议使用，用什么解压什么，内存占用低
- Exclude Type Information: 在资源包中 不包含资源的类型信息
- Force Rebuild: 重新打包时需要重新构建包; 和Clear Folders不同，它不会删除不再存在的包
- Ignore Type Tree Changes: 增量构建检查时，忽略类型数的更改
- Append Hash: 将文件哈希值附加到资源包名上
- Strict Mode: 严格模式，如果打包时报错了，则打包直接失败无法成功
- Dry Run Build: 运行时构建

***
# 三. AB包资源加载

## 3.1 同步加载
1. 加载AB包
   注意: 同一AB包不能重复加载, 不然报错
```CSharp
AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "AB包名");
```
2. 加载AB包资源
```CSharp
//这里的两种加载方式都行, 看情况使用
//学第二种方式主要是因为Lua不支持泛型, 到时候热更的时候会用
GameObject obj = ab.LoadAsset<GameObject>("资源名");
GameObject obj = ab.LoadAsset("资源名",typeof(GameObject)) as GameObject;
```

## 3.2 异步加载
```CSharp
public GameObject obj;

void Start()
{
    StartCoroutine(LoadABRes("model", "Cube"));
}

IEnumerator LoadABRes(string ABName, string resName)
{
    AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
    yield return abcr;
    AssetBundleRequest abq = abcr.assetBundle.LoadAssetAsync(resName, typeof(GameObject));
    yield return abq;
    obj = Instantiate(abq.asset as GameObject);
}
```

## 3.3 卸载AB包

1. 卸载单个ab包
```CSharp
AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");

ab.Unload(true);
```

2. 卸载所有ab包
```CSharp
AssetBundle.UnloadAllAssetBundles(false);
```

那上面的参数中的bool值是什么意思呢?
- true就代表将场景中正在使用的ab包资源一起卸载了
- false就代表不会卸载场景中被引用的资源, 只会将内存中的ab包卸载了

以下面的代码为例
```CSharp
void Start()
{
    AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");

    GameObject obj = ab.LoadAsset<GameObject>("Cube");
    Instantiate(obj);
    
    ab.Unload(true);
}
```
- 这是true的结果
<center>

![alt text](/Unity/图片/热更新/AssetBundle/AssetBundle11-05_11-56-13.jpg)

</center>


- 这是false的结果
<center>

![alt text](/Unity/图片/热更新/AssetBundle/AssetBundle11-05_11-55-06.jpg)

</center>

***
# 四. AB依赖
当一个资源用到了别的AB包中的资源, 这时如果只加载自己的AB包
通过它创建对象, 会出现资源丢失的情况
这时必须把依赖包一起加载了才能正常

举个例子, 下面Cube是一个包(包名model), Red和Sphere是另一个包(包名Sphere), 但是Cube的材质用的是Red
<center>

![alt text](/Unity/图片/热更新/AssetBundle/AssetBundle11-05_18-00-19.jpg)

</center>
如果只加载model包, 并实例化Cube的话, 就会导致材质丢失

## 4.1 获取依赖信息
1. 加载主包
```CSharp
AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
```
2. 加载主包中的固定文件
```CSharp
//这个是固定写法, 甚至这个传入的字符串
AssetBundleManifest abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
```
3. 从固定文件中得到依赖信息
```CSharp
string[] strs = abManifest.GetAllDependencies("model");
```
4. 加载所有依赖包
```CSharp
for (int i = 0; i < strs.Length; ++i)
{
    AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + strs[i]);
}
```