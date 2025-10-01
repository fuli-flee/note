[toc]
***
# 一. 概念及工作原理

## 1. 概念

全称 即时模式游戏用户交互界面（IMGUI）
在Unity中一般简称为GUI
它是一个代码驱动的UI系统

## 2. 主要作用
1. 作为程序员的调试工具，创建游戏内调试工具
2. 为脚本组件创建自定义检视面板
3. 创建新的编辑器窗口和工具以拓展Unity本身（一般用作内置游戏工具）

注意：不要用它为玩家制作UI功能

## 3. GUI的工作原理

在继承MonoBehaviour的脚本中的特殊函数里, 调用GUI提供的方法

```CSharp
//类似生命周期函数 
private void OnGUI()
{
    //在其中书写 GUI相关代码 即可显示GUI内容
}
```
注意：
1. 它每帧执行 相当于是用于专门绘制GUI界面的函数
2. 一般只在其中执行GUI相关界面绘制和操作逻辑
3. 该函数 在 OnDisable之前  LateUpdate之后执行
4. 只要是继承Mono的脚本 都可以在OnGUI中绘制GUI

***
# 二. 基本控件

## 1. 文本和按钮控件

### 1.1 GUI 控件绘制的共同点
1. 他们都是GUI公共类中提供的静态函数 直接调用即可
</br>

2. 他们的参数都大同小异
     - 位置参数：Rect参数  x y位置 w h尺寸
     - 显示文本：string参数
     - 图片信息：Texture参数
     - 综合信息：GUIContent参数
     - 自定义样式：GUIStyle参数
</br>

3. 每一种控件都有多种重载，都是各个参数的排列组合
</br>

必备的参数内容 是 位置信息和显示信息

### 1.2 文本控件

**`GUI.Label`**

> 再次提示: 一定是要写到 OnGUI() 里

GUI的原点是屏幕左上角

- 基本使用
  - **GUI会保持原材质的宽高比, 不会进行拉伸**
```CSharp
public Texture tex;
public Rect rect;

private void OnGUI()
{
    //字符串
    GUI.Label(new Rect(0,0,100,30),"Hello World");
    //图片
    GUI.Label(rect,tex);
}
```
</br>

- 综合使用(图片和文字一起)
```CSharp
public Rect rect1;

GUI.Label(rect1, content);
```
</br>

- 可以获取当前鼠标或者键盘选中的GUI控件 对应的 tooltip信息(比较鸡肋)
```CSharp
Debug.Log(GUI.tooltip);
```
</br>

- 自定义样式
```CSharp
public GUIStyle style;

GUI.Label(new Rect(0,0,100,30),"Hello World");
```
[官方文档](https://docs.unity.cn/cn/2020.3/Manual/class-GUIStyle.html)

### 1.3 按钮控件

**`GUI.Button`**

在按钮范围内点击和松开就会打印, 这个过程中只要离开按钮范围就会无效
```CSharp
if (GUI.Button(btnRect, btnText,style))
{
    print("单点");
}
```

1. 按下不松且在范围内→每帧持续打印；
2. 离开范围→停止；
3. 回范围→恢复；
4. 在范围内松开→额外多打印一次

场景 1、3 的 “持续打印”，是 Unity 事件系统 “每帧主动调用” 按钮回调；
而场景4的 “松开时的打印”，是 GUI.RepeatButton 的特殊机制 —— 松开鼠标时会强制再触发一次回调，相当于 “执行 if 语句本身”，而非系统持续调用。
```CSharp
if (GUI.RepeatButton(btnRect, btnTex,style))
{
    print("长按");
}
```
***
## 2. 多选框和单选框

### 2.1 多选框
```CSharp
private bool isSel;

isSel = GUI.Toggle(new Rect(0, 0, 100, 30), isSel, "效果开关");
```
自定义样式 显示问题
- 修改固定宽高 fixedWidth和fixedHeight
- 修改从GUIStyle边缘到内容起始处的空间 padding

### 2.2 单选框
```CSharp
private int nowSelIndex = 1;
    
private void OnGUI()
{
    if(GUI.Toggle(new Rect(0, 100, 100, 30), nowSelIndex == 1, "选项一"))
    {
        nowSelIndex = 1;
    }
    if(GUI.Toggle(new Rect(0, 140, 100, 30), nowSelIndex == 2, "选项二"))
    {
        nowSelIndex = 2;
    }
    if(GUI.Toggle(new Rect(0, 180, 100, 30), nowSelIndex == 3, "选项三"))
    {
        nowSelIndex = 3;
    }
}
```
