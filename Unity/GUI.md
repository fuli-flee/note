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

## 补充
OnGUI方法的代码执行顺序是从上到下执行, 所以越想绘制层次在后的图片就越应该将其代码写在OnGUI的靠前部分

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

***
## 3. 输入框

### 3.1 普通输入

```CSharp
private string inputStr;
//输入框 一个是显示内容
// 一个是最大输入长度 
inputStr = GUI.TextField(new Rect(0,0,100,30), inputStr, 5);
```

### 3.2 密码输入
值得注意的是, 第三个参数是用于显示替代的字符
```CSharp
private string inputPw;
inputPw = GUI.PassworldField(new Rect(0,0,100,30), inputPw, '*');
```

*** 
## 4. 拖动条
### 4.1 水平拖动条
```CSharp
private float nowValue;
//拖动条最大值为1,最小值为0
nowValue = GUI.HorizontalSlider(new Rect(0,0,100,30), nowValue, 0, 1);
```

### 4.2 竖直拖动条
```CSharp
nowValue = GUI.VerticalSlider(new Rect(0,0,100,30), nowValue, 0, 1);
```
***
## 5. 图片绘制和框
### 5.1 图片绘制
```CSharp
public Rect texPos;
public Texture tex;
public ScaleMode mode = ScaleMode.StretchToFill;
public bool alpha = true;
public float wh = 0;

GUI.DrawTexture(texPos, tex, mode, alpha, wh);
```

讲解一下没有见过的参数重载
- ScaleMode(缩放模式)
  - Stretch To Fill (会通过宽高比来计算图片 但是 会进行裁剪)
  - Scale And Crop (会自动根据宽高比进行计算 不会拉变形 会一直保持图片完全显示的状态)
  - Scale And Fit (始终填充满你传入的 Rect范围)
</br>

- alpha 是用来 控制 图片是否开启透明通道的
</br>

- imageAspect ： 自定义宽高比  如果不填 默认为0 就会使用 图片原始宽高  


### 5.2 框绘制
没啥用, 就多个黑色的背景框
```CSharp
GUI.Box();
```

***
# 三. 复合控件

***
## 6. 工具栏和选择网络

### 6.1 工具栏
```CSharp
private int toolbarIndex = 0;
private string[] toolbarInfos = new string[] { "强化", "进阶", "幻化" };

toolbarIndex = GUI.Toolbar(new Rect(0, 0, 200, 30), toolbarIndex, toolbarInfos);
//工具栏可以帮助我们根据不同的返回索引 来处理不同的逻辑
switch (toolbarIndex)
{
    case 0:
        break;
    case 1:
        break;
    case 2:
        break;
}
```

### 6.2 选择网络
```CSharp
//相对toolbar多了一个参数 xCount 代表 水平方向最多显示的按钮数量
selGridIndex = GUI.SelectionGrid(new Rect(0, 50, 200, 60), selGridIndex, toolbarInfos, 1);
```

***
## 7. 滚动视图和分组

### 7.1 分组

用于统一管理多个组件, 达到分组管理的目的
```CSharp
public Rect groupPos;

GUI.BeginGroup(groupPos);//一般只用这个重载
//下面声明的控件位置都是相对于这个Group来说的
GUI.Button(new Rect(0, 0, 100, 50), "测试按钮");
GUI.Label(new Rect(0, 60, 100, 20), "Label信息");

GUI.EndGroup();
```

### 7.2 滚动视图
```CSharp
// 参数一: 滚动视图在屏幕上的位置
// 参数二: 显示视图在x,y上的移动距离
// 参数三: 视图显示范围
nowPos = GUI.BeginScrollView(scPos, nowPos, showPos);

GUI.Toolbar(new Rect(0, 0, 300, 50), 0, strs);
GUI.Toolbar(new Rect(0, 60, 300, 50), 0, strs);
GUI.Toolbar(new Rect(0, 120, 300, 50), 0, strs);
GUI.Toolbar(new Rect(0, 180, 300, 50), 0, strs);

GUI.EndScrollView();
```
***
## 8. 窗口

### 8.1 窗口
- 第一个参数 id 是窗口的唯一ID 不要和别的窗口重复
- 委托参数 是用于 绘制窗口用的函数 传入即可
- id对于我们来说 有一个重要作用 除了区分不同窗口 还可以在一个函数中去处理多个窗口的逻辑
- 通过id去区分他们
```CSharp
GUI.Window(1, new Rect(100, 100, 200, 150), DrawWindow, "测试窗口");
GUI.Window(2, new Rect(100, 350, 200, 150), DrawWindow, "测试窗口2");

private void DrawWindow(int id)
{
    switch (id)
    {
        case 1:
            GUI.Button(new Rect(0, 30, 30, 20), "1");
            break;
        case 2:
            GUI.Button(new Rect(0, 30, 30, 20), "2");
            break;
    }
}
```

### 8.2 模态窗口 
- 可以让该其它控件不再有用,多用于处理错误

```CSharp
GUI.ModalWindow(3, new Rect(300, 100, 200, 150), DrawWindow, "模态窗口");

private void DrawWindow(int id)
{
    switch (id)
    {
        case 3:
            GUI.Button(new Rect(0, 30, 30, 20), "3");
            break;
    }
}

```

### 8.3 拖动窗口

```CSharp
//位置赋值只是前提
dragWinPos = GUI.Window(4, dragWinPos, DrawWindow, "拖动窗口");

private void DrawWindow(int id)
{
    switch (id)
    {
        case 4:
            //该API 写在窗口函数中调用 可以让窗口被拖动
            //传入Rect参数的重载 作用 
            //是决定窗口中哪一部分位置 可以被拖动
            //默认不填 就是无参重载 默认窗口的所有位置都能被拖动
            GUI.DragWindow(new Rect(0,0,1000,20));
            break;
    }
}
```

***
# 四. 自定义整体样式
***
## 9. GUISkin

### 9.1 全局颜色

#### (1)全局的着色颜色 影响背景和文本颜色
```CSharp
GUI.color = Color.red;
```
#### (2)文本着色颜色 会和 全局颜色相乘

```CSharp
GUI.contentColor = Color.yellow;
```

#### (3)背景元素着色颜色 会和 全局颜色相乘
```CSharp
GUI.backgroundColor = Color.red;
```

### 9.2 整体皮肤样式

它可以帮助我们整套的设置 自定义样式 , 相对单个控件设置Style要方便一些

```CSharp
public GUISkin skin;

GUI.skin = skin;
//虽然设置了皮肤 但是绘制时 如果使用GUIStyle参数 优先设置GUIStyle参数
GUI.Button(new Rect(0, 0, 100, 30), "测试按钮");
```
***
## 10. GUILayout 

主要用于进行编辑器开发, 如果用它来做游戏UI不合适
默认垂直排列

### 10.1 自动布局

#### (1) 修改为垂直布局
```CSharp
GUILayout.BeginVertical();

GUILayout.EndVertical();
```

#### (2) 修改为水平布局
```CSharp
GUILayout.BeginHorizontal();

GUILayout.EndHorizontal();
```

#### (3) 设置显示区域
```CSharp
GUI.BeginGroup(new Rect(100, 100, 200, 300)); 

GUI.EndGroup();
```

### 10.2 布局选项

布局的使用
```CSharp
GUILayout.Button("123", GUILayout.Width(200));

GUILayout.Button("235", GUILayout.ExpandWidth(false));
```

#### (1)控件的固定宽高
```CSharp
GUILayout.Width(300);
GUILayout.Height(200);
```

#### (2)允许控件的最小宽高
```CSharp
GUILayout.MinWidth(50);
GUILayout.MinHeight(50);
```

#### (3)允许控件的最大宽高
```CSharp
GUILayout.MaxWidth(100);
GUILayout.MaxHeight(100);
```

#### (4)允许或禁止水平拓展
```CSharp
GUILayout.ExpandWidth(true);//允许
GUILayout.ExpandHeight(false);//禁止
GUILayout.ExpandHeight(true);//允许
GUILayout.ExpandHeight(false);//禁止
```