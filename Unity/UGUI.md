[toc]

***
# 一. 六大基础组件

[参数不懂直接去看官方文档](https://docs.unity.cn/cn/2020.3/Manual/UICanvas.html)

在创建UGUI相关对象时, 会自动创建另外两个对象: ==Canvas== 和 ==EventSystem==

这两个对象是UGUI必不可少的, 其自动挂载的脚本便是六大基础组件:
- Canvas: ==Rect Transform== , ==Canvas== , ==Canvas Scaler== , ==Graphic Raycaster==
- EventSystem: ==EventSystem== , ==Standalone Input Module==

## 1. 概述
**Rect Transform**: UI对象位置锚点控制组件, 主要用于控制模拟位置和对其方式
**Canvas**: 画布组件, 主要用于渲染UI控件
**Canvas Scaler**: 画布分辨率自适应组件, 主要用于分辨率自适应
**Graphic Raycaster**: 射线事件交互组件, 主要用于控制射线响应相关
**EventSystem和Standalone Input Module**: 玩家输入事件相应系统和独立输入模块组件, 主要用于监听玩家操作

***
## 2. Canvas
Canvas就是画布, 它是UGUI中所有UI元素能够被显示的根本
它主要负责渲染自己的所有UI子对象

### 2.1. Canvas渲染模式的控制

如果UI控件对象不是Canvas的子对象, 那么控件将不能被渲染, 我们可以同故宫修改Canvas组件上的参数修改渲染方式


如果没有特殊需求, 一般场景上有一个Canvas即可

### 2.2 Canvas的三种渲染方式
<center>

![alt text](/Unity/图片/UGUI/UGUI10-05_20-27-35.jpg)

</center>

- Screen Space - Overlay: 屏幕空间, 覆盖模式, UI始终在前
- Screen Space - Camera: 屏幕空间, 摄像机模式, 3D物体可以显示在UI之前
- World Space: 世界空间, 3D模式 

#### 2.2.1 Screen Space - Overlay
<center>

![alt text](/Unity/图片/UGUI/UGUI10-05_20-23-20.jpg)

</center>

- Pixel Perfect: 是否开启无锯齿精确渲染 (用性能换效果)
- SortOrder: 排序层编号 (用于控制多个Canvas时的渲染先后顺序)

#### 2.2.2 Screen Space - Camera
虽然我这里的图片是设置的主摄像机, 只是方便截图, 但是实际开发不推荐
<center>

![alt text](/Unity/图片/UGUI/UGUI10-05_20-41-38.jpg)

</center>

- RenderCamera: 用于渲染 UI 的摄像机 (如果不设置将类似于覆盖模式)
- Plane Distance: UI 平面在摄像机前方的距离，类似整体 Z 轴的的感觉
- Sorting Layer: 所在排序层
- Order in Layer: 排序层的序号

#### 2.2.3 World Space
<center>

![alt text](/Unity/图片/UGUI/UGUI10-05_21-55-57.jpg)

</center>
Event Camera : 用于处理 UI 事件的摄像机 (如果不设置，不能正常注册 UI 事件 )

***
## 3. Canvas Scaler

### 3.1 概念

画布缩放控制器, 用于分辨率自适应的组件

它主要负责在不同分辨率下UI控件大小自适应, 它并不负责位置, 位置由之后的RectTransform组件负责

它主要提供了三种用于分辨率自适应的模式

### 3.2 必备知识
- 屏幕分辨率:
Game 窗口中的 Stats 统计数据窗口
看到的当前 "屏幕" 分辨率
**会参与分辨率自适应的计算**
</br>

- 画布大小和缩放系数:
选中 Canvas 对象后在 RectTransform 组件中看到的宽高和缩放
**宽高 * 缩放系数 = 屏幕分辨率**
</br>

- Reference Resolution:
参考分辨率, 在Canvas Scaler中
在缩放模式的宽高模式中出现的参数，**参与分辨率自适应的计算**
</br>

分辨率大小自适应主要就是通过不同的算法计算出一个缩放系系数, 用该系数去缩放所有 UI 控件
让其在不同分辨率下达到一个较为理想的显示效果

### 3.3 Canvas Scaler的三种适配模式

<center>

![alt text](/Unity/图片/UGUI/UGUI10-06_10-09-49.jpg)

</center>

- Constant Pixel Size (恒定像素模式): 无论屏幕大小如何，UI 始终保持相同像素大小
- Scale With Screen Size (缩放模式): 根据屏幕尺寸进行缩放，随着屏幕尺寸放大缩小
- Constant Physical Size (恒定物理模式): 无论屏幕大小和分辨率如何，UI 元素始终保持相同物理大小

#### 3.3.1 Constant Pixel Size  恒定像素模式

<center>

![alt text](/Unity/图片/UGUI/UGUI10-06_18-38-51.jpg)

</center>

**相关参数**
- Scale Factor: 缩放系数,按此系数缩放画布中的所有UI元素
- Reference Pixels Per Unit: 单位参考像素,多少像素对应Unity中的一个单位(默认一个单位为100像素)

图片设置中的Pixels Per Unit设置,会和该参数一起参与计算

**恒定像素模式计算公式**
UI原始尺寸 = 图片大小(像素) / (Pixels Per Unit / Reference Pixels Per Unit )

<center>

![alt text](/Unity/图片/UGUI/UGUI10-06_18-37-49.jpg)

</center>

**总结**
它不会让UI控件进行分辨率大小自适应, 会让UI控件始终保持设置的尺寸大小显示
一般在进行游戏开发极少使用这种模式, 除非通过代码计算来设置缩放系数


#### 3.3.2 Scale With Screen Size  缩放模式

<center>

![alt text](/Unity/图片/UGUI/UGUI10-06_18-46-18.jpg)

</center>

**(1) 相关参数**

- Reference Resolution: 参考分辨率(美术出图的标准分辨率)。
    缩放模式下的所有匹配模式都会基于参考分辨率进行自适应计算
- Screen Match Mode: 屏幕匹配模式,当前屏幕分辨率宽高比不适应参考分辨率时,用于分辨率大小自适应的匹配模式

**(2) 屏幕匹配模式**

<center>

![alt text](/Unity/图片/UGUI/UGUI10-06_19-09-54.jpg)

</center>

- Expand: 水平或垂直**拓展画布**区域,会根据宽高比的变化来放大缩小画布,可能有黑边
- Shrink: 水平或垂直**裁剪画布**区域,会根据宽高比的变化来放大缩小画布,可能会裁剪
- Match Width Or Height: 以**宽高或者二者的平均值作为参考**来缩放画布区域
</br>

    **① Expend**
    拓展匹配

    - 将Canvas Size进行宽或高扩大,让他高于参考分辨率
    - 计算公式:
        - 缩放系数=Mathf.Min(屏幕宽/参考分辨率宽,屏幕高/参考分辨率高);
        - 画布尺寸=屏幕尺寸/缩放系数 
    - 表现效果: 最大程度的缩小UI元素, 保留UI控件所有细节, 可能会留黑边 
</br>

    **② Shrink**
    收缩匹配
    - 将Canvas Size进行宽或高收缩,让他低于参考分辨率
    - 计算公式:
    - 缩放系数=Mathf.Max(屏幕宽/参考分辨率宽,屏幕高/参考分辨率高);
    - 画布尺寸=屏幕尺寸/缩放系数
    - 表现效果: 最大程度的方法UI元素, 让UI元素能够填满画面, 可能会出现裁剪
</br>

    **③ March Width Or Height**
    宽高匹配

    - 以宽高或者二者的某种平均值作为参考来缩放画布
    - Match: 确定用于计算的宽高匹配值
    - 表现效果: 主要用于只有横屏模式或者竖屏模式的游戏
    </br>

    - 竖屏游戏: Match=0
        将画布宽度设置为参考分辨率的宽度
        并保持比例不变,屏幕越高可能会有黑边
    </br>

    - 横屏游戏: Match=1
        将画布高度设置为参考分辨率的高度
        并保持比例不变,屏幕越长可能会有黑边

    ***
#### 3.3.3 Constant Physical Size  恒定物理模式


<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_10-06-09.jpg)

</center>

**(1) 相关参数**
- DPI: (Dots Per Inch,每英寸点数)图像每英寸长度内的像素点数
- Physical Unit: 物理单位,使用的物理单位种类
- Fallback Screen DPI: 备用DPI,当找不到设备DPI时,使用此值
- Default Sprite DPI: 默认图片DPI

**Physical Unit**

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_09-21-19.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-07_09-22-30.jpg)

</center>

- 计算公式
    - 根据DPI算出新的Reference Pixels Per Unit(单位参考像素)
        - 新单位参考像素 = 单位参考像素 * Physical Unit / Default Sprite DPI
    - 再使用模式一:恒定像素模式的公式进行计算
        - 原始尺寸 = 图片大小(像素) / (Pixels Per Unit/新单位参考像素)

### 3.4 恒定像素模式和恒定物理模式区别
- 相同点:他们都不会进行缩放,图片有多大显示多大,使用他们不会进行分辨率大小自适应
- 不同点:相同尺寸不同DPI设备像素点区别,像素点越多细节越多, 同样为5像素,DPI较低的设备上看起来的尺寸可能会大于DP1较高的设备

### 3.5 总结
恒定物理模式
- 它不会让UI控件进行分辨率大小自适应
- 会让UI控件始终保持设置的尺寸大小显示
- 而且会根据设备DPI进行计算,让在不同设备上的显示大小夏更加准确

一般在进行游戏开发极少使用这种模式
***
### 3.6 3D模式
当Canvas的渲染模式设置为: 世界空间3D渲染模式时
这时Canvas Scaler的缩放模式会强制变为: World 3D世界模式

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_09-33-17.jpg)

</center>

- Dynamic Pixels Per Unit: UI中动态创建的位图(例如文本)中,单位像素数(类似密度)
- Reference Pixels Per Unit: 单位参考像素,多少像素对应Unity中的一个单位(默认一个单位为100像素)

***
## 4. Graphic Raycaster

图形射线投射器, 用于检测UI输入事件的射线发射器
它主要负责通过射线检测玩家和UI元素的交互, 判断是否点击到了UI元素

所以它和NGUI不一样, 它是基于图形来检测的, 不是基于碰撞器来检测的
<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_10-04-31.jpg)

</center>


### 4.1 相关参数
- Ignore Reversed Graphics: 是否忽略反转图形
- Blocking Objects: 射线被哪些类型的碰撞器阻挡 (在覆盖渲染模式下无效)
- Blocking Mask: 射线被哪些层级的碰撞器阻挡(在覆盖渲染模式下无效)
***
## 5. EventSystem 
事件系统
它是用于管理玩家的输入事件并分发给各UI控件
它是事件逻辑处理模块
所有的UI事件都通过EventSystem组件中轮询检测并做相应的执行
它类似一个中转站,和许多模块一起共同协作

如果没有它,所有点击、拖曳等等行为都不会被响应

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_10-34-26.jpg)

</center>

### 5.1 相关参数
- First Selected: 首先选择的游戏对象,可以设置游戏一开始的默认选择
- Send Navigation Events: 是否允许导航事件(移动/按下/取消)
- DragThreshold: 拖拽操作的阈值(移动多少像素算拖拽)

## 6. Standalone Input Module

独立输入模块
它主要针对处理鼠标/键盘/控制器/触屏(新版Unity)的输入
输入的事件通过EventSystem进行分发
它依赖于EventSystem组件,他们两缺一不可

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_10-34-46.jpg)

</center>

### 6.1 相关参数
- Horizontal Axis: 水平轴按钮对应的热键名(该名字对应Input管理器
- Vertical Axis: 垂直轴按钮对应的热键名字对应Input管理器)
- Submit Button: 提交(确定)按钮对应的热建名(该名字对应Input管理器)
- Cancel Button: 取消按钮对应的热建名(该名字对应Input管理器)
- Input Actions Per Second: 每秒允许键盘/控制器输入的数量
- Repeat Delay: 每秒输入操作重复率生效前的延迟时间
- ForceModuleActive: 是否强制模块处于激活状态

## 7. RectTransform

RectTransform意思是矩形变换, 它继承于Transform
是专门用于处理UI元素位置大小相关的组件

Transform组件只处理位置、角度、缩放
RectTransform在此基础上加入了矩形相关,将UI元素当做一个个矩形来处理, 加入了中心点、锚点、长宽等属性
其目的是更加方便的控制其大小以及分辨率自适应中的位置适,应

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_10-50-08.jpg)

</center>

### 7.1 相关参数
- Pivot: 轴心(中心)点,取值范围0~1
- Anchors(相对父矩形锚点):
  - Min是矩形锚点范围X和Y的最小值
  - Max是矩形锚点范围X和Y的最大值
  - 取值范围都是0~1
- Pos(X,Y,Z): 轴心点(中心点)相对锚点的位置
- Width/Height: 矩形的宽高
- Left/Top/Right/Bottom: 矩形边缘相对于锚点的位置;当锚点点分离时会出现这些内容
- Rotation: 围绕轴心点旋转的角度
- Scale: 缩放大小
- ![alt text](/Unity/图片/UGUI/UGUI10-07_11-21-44.jpg) :Blueprint Mode(蓝图模式),启用后,编辑旋转和缩放不会影响矩形,只会影响显示内容
- ![alt text](/Unity/图片/UGUI/UGUI10-07_11-23-35.jpg): RawEditMode(原始编辑模式),启用后,改变轴心和锚点值不会改变矩形位置
</br>

- 锚点中心点快捷设置面板
    <center>

    ![alt text](/Unity/图片/UGUI/UGUI10-07_11-27-58.jpg)

    </center>

    - 鼠标左键点击其中的选项, 可以快捷设置锚点(9宫格布局)
    - 按住Shift点击鼠标左键可以同时设置轴心点(相对自身矩形)
    - 按住Alt点击鼠标左键可以同时设置位置

### 7.2 代码获取
由于 RectTransform 是继承于 Transform 的, 所以通过里氏替换原则就能获取其参数, 这里以轴心点为例子

```CSharp
(this.transform as RectTransform).pivot;
```
***
# 二. 三大基础组件

## 8. Image
图像组件
是UGUI中用于显示精灵图片的关键组件
除了背景图等大图，一般都使用Image来显示UI中的图片元素

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_16-54-57.jpg)

</center>

### 8.1 相关参数
<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_16-51-06.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-07_17-08-23.jpg)

</center>

### 8.2 代码控制
```CSharp
using UnityEngine.UI;

Image img = this.GetComponent<Image>();
img.sprite = Resources.Load<Sprite>("图片名");

//修改尺寸, 只与Rect Transform相关
(transform as RectTransform).sizeDelta = new Vector2(200, 200);
img.raycastTarget = false;
```
***
## 9. Text

文本组件, UGUI中用于显示文本的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_17-46-28.jpg)

</center>

### 9.1 相关参数


<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_17-48-55.jpg)

</center>

### 9.2 富文本

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_17-59-41.jpg)

</center>

### 9.3 边缘线和阴影
**(1) 边缘线组件**
Outline

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_18-58-51.jpg)

</center>

**(2) 阴影**
Shadow

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_19-00-45.jpg)

</center>

### 9.4 代码控制
```CSharp
Text txt = this.GetComponent<Text>();
txt.text = "Niko is a cat";
```
### 9.5 拓展
不管是控件中的Label还是Text组件, 默认的字都很糊

- 解决方法: 缩小RectTransform的同时放大Front size, 放大Front size要注意放大程度要在RectTransform范围内, 不然字会消失

## 10. RawImage
RawImage是原始图像组件
是UGUI中用于显示任何纹理图片的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_19-07-24.jpg)

</center>

它和Image的区别是 一般RawImage用于显示大图(背景图，不需要打入图集的图片，网络下载的图等等)


### 10.1 相关参数
<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_19-08-35.jpg)

</center>

### 10.2 代码控制

```CSharp
RawImage raw = this.GetComponent<RawImage>();
//这里加载任何格式的图片都是可以的, 只是我这里用了Texture举例
raw.texture = Resources.Load<Texture>("材质名");
raw.uvRect = new Rect(0, 0, 1, 1);
```
***
# 三. 组合控件

## 11. Button

按钮组件, 是UGUI中用于处理玩家按钮相关交互的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_19-26-42.jpg)

</center>

默认创建的Button由2个对象组成
- 父对象——Button组件依附对象 同时挂载了一个Image组件 作为按钮背景图
- 子对象——按钮文本 (可选)

### 11.1 相关参数
<center>

![alt text](/Unity/图片/UGUI/UGUI10-07_19-28-04.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-07_19-32-22.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-07_19-38-43.jpg)

</center>

### 11.2 代码控制

```CSharp
Button btn = this.GetComponent<Button>();
btn.interactable = true;
btn.transition = Selectable.Transition.None;
```

### 11.3 监听点击事件的两种方式
点击事件 是 在按钮区域抬起按下一次 就算一次点击

1. 拖脚本

2. 代码添加

```CShrap
btn.onClick.AddListener(ClickBtn);
btn.onClick.AddListener(() => {
    print("123123123");
});

btn.onClick.RemoveListener(ClickBtn2);
btn.onClick.RemoveAllListeners();

public void ClickBtn()
{
    print("按钮点击");
}
```

## 12. Toggle

开关组件, 是UGUI中用于处理玩家单选框多选框相关交互的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_09-39-06.jpg)

</center>

默认是多选框, 可以通过配合ToggleGroup组件制作为单选框

默认创建的Toggle由4个对象组成
- 父对象——Toggle组件依附
- 子对象——背景图（必备）、选中图（必备）、说明文字（可选）

### 12.1 相关参数
<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_09-37-58.jpg)

</center>

### 12.2 代码控制
```CSharp
Toggle tog = this.GetComponent<Toggle>();
```

- 得到激活的Toggle
```CSharp
ToggleGroup togGroup = this.GetComponent<ToggleGroup>();

//可以遍历提供的迭代器 得到当前处于选中状态的 Toggle
foreach (Toggle item in togGroup.ActiveToggles())
{
    print(item.name + " " + item.isOn);
}
```

### 12.3 监听事件
```CSharp
tog.onValueChanged.AddListener(ChangeValue);
tog.onValueChanged.AddListener((b) =>
{
    print("代码监听 状态改变" + b);
});

private void ChangeValue(bool v)
{
    print("代码监听 状态改变" + v);
}
```

## 13. InputField
输入字段组件, 是UGUI中用于处理玩家文本输入相关交互的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_10-47-22.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-08_10-48-05.jpg)

</center>

默认创建的InputField由3个对象组成
- 父对象——InputField组件依附对象 以及 同时在其上挂载了一个Image作为背景图
- 子对象——文本显示组件（必备）、默认显示文本组件（必备）

### 13.1 相关参数
- TextComponent: 用于关联显示输入内容的文本组件
- Text: 输入框的起始默认值
- Character Limit: 可以输入字符长度的最大值
- Content Type: 输入的字符类型限制
  - Standard: 标准模式;可以输入任何字符
  - Autocorrected: 自动更正模式;跟踪未知单词,向用户建议合适的替换候选词
  - IntegerNumber: 整数模式;用户只能输入整数
  - Decimal Number: 十进制数模式;用于只能输入数组包括小数
  - Alphanumeric: 字母数字模式;只能输入字母和数字
  - Name: 名字模式;自动将每个单子首字母大写
  - Email Address: 邮箱地址模式;允许最多输入一个@符号组成的字符和数字字符串
  - Password: 密码模式;用星号隐藏输入的字符,允许使用符号
  - Pin: 别针模式;用星号隐藏输入的字符,只允许输入整数
  - Custom: 自定义模式;允许自定义行类型,输入类型,键盘类型和字符验证 
- LineType: 行类型,定义文本格式
    - Single Line: 只允许单行显示
    - Multi Line Submit: 允许使用多行,仅在需要时使用新的一行
    - Multi Line NewLine: 允许使用多行,用户可以按回车键空行
- Placeholder: 关联用于显示初始内容文本控件
- Caret Blink Rate: 光标闪烁速率
- Caret Width: 光标宽
- CustomCaret Color: 自定义光标颜色
- Selection Color: 批量选中的背景颜色
- HideMobile Input: 隐藏移动设备屏幕上键盘,仅适用于IOS
- Read Only: 只读,不能改
- Character.Limit: 可以输入字符长度的最大值

### 13.2 代码控制
```CSharp
InputField input = this.GetComponent<InputField>();
input.text = "123123123123";
```

### 13.3 监听事件
```CSharp
input.onValueChanged.AddListener((str) =>
{
    print("代码监听 改变" + str);
});

input.onEndEdit.AddListener((str) =>
{
    print("代码监听 结束输入" + str);
});
```
==注意==:
- Unity 从 2020.1 版本开始新增了 ==InputField.onSubmit== 事件 
- 它和 InputField.onEndEdit 的区别在于:
  - InputField.onEndEdit（结束编辑）
    - 触发时机：当输入框失去焦点时触发（无论输入内容是否变化）。
  - InputField.onSubmit（提交输入）
    - 触发时机：当用户主动提交输入内容时触发。

## 14. Slider
滑动条组件, 是UGUI中用于处理滑动条相关交互的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_18-38-13.jpg)

</center>

默认创建的Slider由4组对象组成
- 父对象——Slider组件依附的对象
- 子对象——背景图、进度图、滑动块三组对象
### 14.1相关参数

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_18-40-32.jpg)

</center>

### 14.2 代码控制
```CSharp
Slider s = this.GetComponent<Slider>();
print(s.value);
```

### 14.3 监听事件
```CSharp
s.onValueChanged.AddListener((v) =>
{
    print("代码添加的监听" + v);
});
```

## 15. Scrollbar
滚动条组件, 是UGUI中用于处理滚动条相关交互的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_19-19-40.jpg)

</center>

默认创建的Scrollbar由2组对象组成
- 父对象——Scrollbar组件依附的对象
- 子对象——滚动块对象

一般情况下我们不会单独使用滚动条, 都是配合ScrollView滚动视图来使用

### 15.1 相关参数

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_19-28-16.jpg)

</center>

### 15.2 代码控制
```CSharp
Scrollbar sb = this.GetComponent<Scrollbar>();
print(sb.value);
print(sb.size);
```

### 15.3 监听事件
```CSharp
sb.onValueChanged.AddListener((v) => {
    print("代码监听的函数" + v);
});
```


## 16. ScrollView
滚动视图组件, 是UGUI中用于处理滚动视图相关交互的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_20-11-03.jpg)

</center>

默认创建的ScrollRect由4组对象组成
- 父对象——ScrollRect组件依附的对象 还有一个Image组件 最为背景图
- 子对象
    - Viewport控制滚动视图可视范围和内容显示
    - Scrollbar Horizontal 水平滚动条
    - Scrollbar Vertical 垂直滚动条

### 16.1 相关参数

<center>

![alt text](/Unity/图片/UGUI/UGUI10-08_20-13-52.jpg)

</center>

### 16.2 代码控制 

```CSharp
ScrollRect sr = this.GetComponent<ScrollRect>();

//设置位置的两种方法(常用第二种):
//改变内容的大小 具体可以拖动多少 都是根据它的尺寸来的
sr.content.sizeDelta = new Vector2(200, 200);

sr.normalizedPosition = new Vector2(0, 0.5f);
```

### 16.3 监听事件
```CSharp
sr.onValueChanged.AddListener((vec) =>
{
    print(vec);
});
```
## 17. DropDown

下拉列表（下拉选单）组件, 是UGUI中用于处理下拉列表相关交互的关键组件

<center>

![alt text](/Unity/图片/UGUI/UGUI10-09_08-40-58.jpg)

</center>

默认创建的DropDown由4组对象组成
- 父对象
  - DropDown组件依附的对象 还有一个Image组件 作为背景图

- 子对象
    - Label是当前选项描述
    - Arrow右侧小箭头
    - Template下拉列表选单

### 17.1 相关参数
<center>

![alt text](/Unity/图片/UGUI/UGUI10-09_08-48-02.jpg)

</center>

### 17.2 代码控制
```CSharp
Dropdown dd = this.GetComponent<Dropdown>();

print(dd.value);

print(dd.options[dd.value].text);

dd.options.Add(new Dropdown.OptionData("123123123"));
```

### 17.3 监听事件

```CSharp
dd.onValueChanged.AddListener((index) => {
    print(index);
});
```
***

# 四. 图集
UGUI和NGUI使用上最大的不同是 NGUI使用前就要打图集, UGUI可以再之后再打图集
打图集的目的就是减少DrawCall 提高性能

UGUI中渲染顺序与Hierarchy窗口的排列顺序有关, 排序越靠前越后渲染(这句话也有歧义, 你理解为 "排序越靠后越后渲染" 也行, 理解的方向不同罢了, 反正看下面的例子就懂了)

<center>

![alt text](/Unity/图片/UGUI/UGUI10-09_10-14-19.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-09_10-13-46.jpg)

</center>

## 18. 图集制作

### 18.1 在Unity中打开自带的打图集功能
> Edit => Project Setting => Editor => Sprite Packer
> (精灵包装器，可以通过Unity自带图集工具生成图集)

<center>

![alt text](/Unity/图片/UGUI/UGUI10-09_09-38-09.jpg)

</center>

- Disabled：默认设置，不会打包图集
- Enabled For Build：Unity进在构建时打包图集，在编辑器模式下不会打包
- Always Enabled：Unity在构建时打包图集，在编辑模式下运行前会打包图集
</br>

==注意: 以下这个参数只在unity2020以下版本才会有, 在2020版本后Legacy Sprite Packer被移除, 切换到了 Sprite Atlas V1==
- Padding Power:选择打包算法在计算打包的精灵之间以及精灵与生成的图集边缘之间的间隔距离, 这里的数字代表2的n次方

### 18.2 相关参数
==注意: 第一种创建方式为unity2020版本以下创建精灵图集的方法, 第二种为2021及以上的方法==
> 右键 => Create => Sprite Atlas
> 右键 => Create => 2D => Sprite Atlas

//TODO: 这里的参数讲解暂存, 因为unity核心还没写完, 到时候再来补充

### 18.3 DrawCall
> Game窗口 => Stats

这里先将Batches认为是DrawCall
没打图集前的DrawCall是5, 打完图集后为3

<center>

![alt text](/Unity/图片/UGUI/UGUI10-09_10-24-19.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-09_10-26-42.jpg)

</center>

- Text和Image组件也占一个DrawCall

<center>

![alt text](/Unity/图片/UGUI/UGUI10-09_10-30-08.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-09_10-28-57.jpg)

</center>

- 当Text处于一个图集的两个精灵图片之间时会打断图集的合并, 增加一次DrawCall

- 下面是三张截图, 不是5张
<center>

![alt text](/Unity/图片/UGUI/UGUI10-09_10-33-03.jpg)

![alt text](/Unity/图片/UGUI/UGUI10-09_10-32-36.jpg)

![alt text](/Unity/图片/UGUI/UGUI10-09_10-34-10.jpg)

</center>

### 18.4 代码加载
```CSharp
using UnityEngine.U2D;

//加载图集
SpriteAtlas sa = Resources.Load<SpriteAtlas>("MyAlas");
//从图集中加载指定名字的小图
sa.GetSprite("bk");
```
# 五. UI事件监听接口
目前所有的控件都只提供了常用的事件监听列表
如果想做一些类似长按，双击，拖拽等功能是无法制作的
或者想让Image和Text，RawImage三大基础控件能够响应玩家输入也是无法制作的

而事件接口就是用来处理类似问题
让所有控件都能够添加更多的事件监听来处理对应的逻辑
## 19. 事件接口

### 19.1 常用事件接口
- IPointerEnterHandler - OnPointerEnter - 当指针进入对象时调用 （鼠标进入）
- IPointerExitHandler - OnPointerExit - 当指针退出对象时调用 （鼠标离开）
- IPointerDownHandler - OnPointerDown - 在对象上按下指针时调用 （按下）
- IPointerUpHandler - OnPointerUp - 松开指针时调用（在指针正在点击的游戏对象上调用）（抬起）
- IPointerClickHandler - OnPointerClick - 在同一对象上按下再松开指针时调用 （点击）
</br>


- IBeginDragHandler - OnBeginDrag - 即将开始拖动时在拖动对象上调用 （开始拖拽）
- IDragHandler - OnDrag - 发生拖动时在拖动对象上调用 （拖拽中）
- IEndDragHandler - OnEndDrag - 拖动完成时在拖动对象上调用 （结束拖拽）

### 19.2 不常用事件接口
- IInitializePotentialDragHandler - OnInitializePotentialDrag - 在找到拖动目标时调用，可用于初始化值
- IDropHandler - OnDrop - 在拖动目标对象上调用
- IScrollHandler - OnScroll - 当鼠标滚轮滚动时调用
- IUpdateSelectedHandler - OnUpdateSelected - 每次勾选时在选定对象上调用
</br>

- ISelectHandler - OnSelect - 当对象成为选定对象时调用
- IDeselectHandler - OnDeselect - 取消选择选定对象时调用
</br>

- 导航相关
  - IMoveHandler - OnMove - 发生移动事件（上、下、左、右等）时调用
  - ISubmitHandler - OnSubmit - 按下 Submit 按钮时调用
  - ICancelHandler - OnCancel - 按下 Cancel 按钮时调用

### 19.3 使用事件接口
1. 继承MonoBehavior的脚本继承对应的事件接口，引用命名空间
```CSharp
using UnityEngine.EventSystems;
```

2. 实现接口中的内容
3. 将该脚本挂载到想要监听自定义事件的UI控件上

### 19.4 PointerEventData参数的关键内容
- 父类：BaseEventData

    - pointerId： 鼠标左右中键点击鼠标的ID 通过它可以判断右键点击
    - position：当前指针位置（屏幕坐标系）
    - pressPosition：按下的时候指针的位置
    - delta：指针移动增量
    - clickCount：连击次数
    - clickTime：点击时间
    </br>

    - pressEventCamera：最后一个OnPointerPress按下事件关联的摄像机
    - enterEvetnCamera：最后一个OnPointerEnter进入事件关联的摄像机

***
# 六. 事件触发器

## 20. EventTrigger
事件触发器是EventTrigger组件, 它是一个集成了所有事件接口的脚本
它可以让我们更方便的为控件添加事件监听

<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_08-43-11.jpg)

</center>

### 20.1 使用
上面都没讲过拖脚本的方式来监听事件, 这里讲一下, 该方法适用于所有组合控件

1. 拖脚本

<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_08-54-14.jpg)

</center>

```CSharp
public class Test : MonoBehaviour
{
    public void TestPointEnter(BaseEventData data)
    {
        //通过里氏替换原则就能使用该类的数据了, 比如eventData.position
        PointerEventData eventData = data as PointerEventData;

        print("鼠标进入");
    }
}
```
将要处理的逻辑挂载到Canvas下, 在Image下挂载EventTrigger组件, 然后让EventTrigger组件关联相应逻辑就行了

<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_08-58-08.jpg)

</center>

2. 代码控制 

```CSharp
public EventTrigger et;

void Start()
{
    //声明一个希望监听的事件对象
    EventTrigger.Entry entry = new EventTrigger.Entry();
    //声明 事件的类型
    entry.eventID = EventTriggerType.PointUp;
    //监听函数关联
    entry.callback.AddListener((data) =>
    {
        print("抬起");
    });
    //把声明好的 事件对象 加入到 EventTrigger当中
    et.triggers.Add(entry);
}
```

类中类Entry的源码
```CSharp
public class Entry
{
    public EventTriggerType eventID = EventTriggerType.PointerClick;
    public TriggerEvent callback = new TriggerEvent();
}
```
***
# 七. 屏幕坐标转UI相对坐标

## 21. RectTransformUtility类
RectTransformUtility 公共类是一个RectTransform的辅助类
主要用于进行一些 坐标的转换等等操作
其中对于我们目前来说 最重要的函数是 将屏幕空间上的点，转换成UI本地坐标下的点

### 21.1 将屏幕坐标转换为UI本地坐标系下的点
方法：
```CSharp
public RectTransform parent;

public void OnDrag(PointerEventData eventData)
{
    //参数一：相对父对象
    //参数二：屏幕点
    //参数三：摄像机
    //参数四：最终得到的点
    //一般配合拖拽事件使用
    Vector2 nowPos;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parent,
                eventData.position,
                eventData.enterEventCamera,
                out nowPos );

    this.transform.localPosition = nowPos;

    //this.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
}

```
***
# 八. 遮罩
在不改变图片的情况下
让图片在游戏中只显示其中的一部分
## 22. Mask
通过在父对象上添加Mask组件即可遮罩其子对象

<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_11-10-02.jpg)

</center>

注意：
1. 想要被遮罩的Image需要勾选Maskable
2. 只要父对象添加了Mask组件，那么所有的UI子对象都会被遮罩
3. 遮罩父对象图片的制作，不透明的地方显示，透明的地方被遮罩

***
# 九. 模型和粒子显示在UI之前

## 23. 模型显示在UI之前

### 23.1直接用摄像机渲染3D物体
- Canvas的渲染模式需为覆盖模式以外的模式
- 摄像机模式 和 世界(3D)模式 都可以让模型显示在UI之前（Z轴在UI元素之前即可）

- 注意：
    1. 摄像机模式时建议用专门的摄像机渲染UI相关
    2. 面板上的3D物体建议也用UI摄像机进行渲染

### 23.2 将3D物体渲染在图片上，通过图片显示
- 专门使用一个摄像机渲染3D模型，将其渲染内容输出到Render Texture上
- 类似小地图的制作方式
- 再将渲染的图显示在UI上

- 该方式 不管Canvas的渲染模式是哪种都可以使用

## 24.  粒子特效显示在UI之前

- 粒子特效的显示和3D物体类似

- 注意点：
  - 在摄像机模式下时, 可以在粒子组件的Renderer相关参数中改变排序层 让粒子特效始终显示在其之前不受Z轴影响
***
# 十. 异形按钮
图片形状不是传统矩形的按钮

## 25. 如何让异形按钮能够准确点击

### 25.1 通过添加子对象的形式
按钮之所以能够响应点击，主要是根据图片矩形范围进行判断的
它的范围判断是自下而上的，意思是如果有子对象图片，子对象图片的范围也会算为可点击范围
那么我们就可以用多个透明图拼凑不规则图形作为按钮子对象用于进行射线检测

### 25.2 通过代码改变图片的透明度响应阈值
第一步：修改图片参数 开启 `Read/Write Enabled` 开关
第二步：通过代码修改图片的响应阈值

该参数含义：指定一个像素必须具有的最小alpha值，以变能够认为射线命中了图片
说人话：当像素点alpha值小于了 该值 就不会被射线检测了
```CSharp
public Image img;
void Start()
{
    img.alphaHitTestMinimumThreshold = 0.1f;
}
```

*** 
# 十一. 自动布局组件

## 26. 自动布局
自动布局的工作方式一般是
自动布局控制组件 + 布局元素 = 自动布局

自动布局控制组件：Unity提供了很多用于自动布局的管理性质的组件用于布局
布局元素：具备布局属性的对象们，这里主要是指具备RectTransform的UI组件

### 26.1 布局元素的布局属性
要参与自动布局的布局元素必须包含布局属性, 在Inspector窗口的最下面

<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_20-51-53.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-10_20-53-42.jpg)

</center>

布局属性主要有以下几条
- inmum width：该布局元素应具有的最小宽度
- Minmum height：该布局元素应具有的最小高度
</br>

- Preferred width：在分配额外可用宽度之前，此布局元素应具有的宽度
- Preferred height：在分配额外可用高度之前，此布局元素应具有的高度。
</br>

- Flexible width：此布局元素应相对于其同级而填充的额外可用宽度的相对量
- Flexible height：此布局元素应相对于其同级而填充的额外可用高度的相对量
</br>

在进行自动布局时 都会通过计算布局元素中的这6个属性得到控件的大小位置

在布局时，布局元素大小设置的基本规则是
1. 首先分配最小大小Minmum width和Minmum height
2. 如果父类容器中有足够的可用空间，则分配Preferred width和Preferred height
3. 如果上面两条分配完成后还有额外空间，则分配Flexible width和Flexible height

一般情况下布局元素的这些属性都是0
但是特定的UI组件依附的对象布局属性会被改变，比如Image和Text

一般情况下我们不会去手动修改他们，但是如果你有这些需求
可以手动添加一个LayoutElement组件 可以修改这些布局属性

### 26.2 水平垂直布局组件
水平垂直布局组件
将子对象并排或者竖直的放在一起

组件名：`Horizontal Layout Group` 和 `Vertical Layout Group`
可以配合子类中的`Layout Element`一起使用

<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_21-00-27.jpg)
![alt text](/Unity/图片/UGUI/UGUI10-10_21-09-48.jpg)

</center>

参数相关：
- Padding：左右上下边缘偏移位置
- Spacing:子对象之间的间距
</br>
- ChildAlignment:九宫格对其方式
- Control Child Size：是否控制子对象的宽高
- Use Child Scale：在设置子对象大小和布局时，是否考虑子对象的缩放
- Child Force Expand：是否强制子对象拓展以填充额外可用空间

### 26.3 网格布局组件
网格布局组件
将子对象当成一个个的格子设置他们的大小和位置

组件名：`Grid Layout Group`
<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_21-16-27.jpg)

</center>

参数相关：
- Padding：左右上下边缘偏移位置
- Cell Size：每个格子的大小
- Spacing：格子间隔
- Start Corner:第一个元素所在位置（4个角）
- Start Axis：沿哪个轴放置元素；Horizontal水平放置满换行，Vertical竖直放置满换列
- Child Alignment：格子对其方式（9宫格）
- Constraint：行列约束
  - Flexible：灵活模式，根据容器大小自动适应
  - Fixed Column Count：固定列数
  - Fixed Row Count：固定行数

### 26.4 内容大小适配器
内容大小适配器
它可以自动的调整RectTransform的长宽来让组件自动设置大小
一般在Text上使用 或者 配合其它布局组件一起使用

组件名：`Content Size Fitter`
<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_21-21-36.jpg)

</center>

参数相关
- Horizontal Fit：如何控制宽度
- Vertical Fit:如何控制高度

Unconstrained：不根据布局元素伸展
Min Size：根据布局元素的最小宽高度来伸展
Preferred Size：根据布局元素的偏好宽度来伸展宽度。

### 26.5 宽高比适配器
宽高比适配器
1. 让布局元素按照一定比例来调整自己的大小
2. 使布局元素在父对象内部根据父对象大小进行适配

组件名：`Aspect Ratio Fitter`
<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_21-29-01.jpg)

</center>

参数相关：
- Aspect Mode：适配模式，如果调整矩形大小来实施宽高比
  - None：不让矩形适应宽高比
  - Width Controls Height：根据宽度自动调整高度
  - Height Controls Width：根据高度自动调整宽度
  - Fit In Parent：自动调整宽度、高度、位置和锚点，使矩形适应父项的矩形，同时保持宽高比，会出现“黑边”
  - Envelope Parent：自动调整宽度、高度、位置和锚点，使矩形覆盖父项的整个区域，同时保持宽高比，会出现“裁剪”
- Aspect Ratio：宽高比；宽除以高的比值

***
# 十二. 画布组

## 27. Canvas Group
如果我们想要整体控制一个面板的淡入淡出 或者 整体禁用
使用目前学习的知识点 是无法方便快捷的设置的
为面板父对象添加 `CanvasGroup` 组件 即可整体控制

组件：`Canvas Group`
<center>

![alt text](/Unity/图片/UGUI/UGUI10-10_22-11-07.jpg)

</center>

参数相关：
- Alpha：整体透明度控制
- Interactable:整体启用禁用设置
- Blocks Raycasts：整体射线检测设置
- Ignore Parent Groups：是否忽略父级CanvasGroup的作用 