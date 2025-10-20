[toc]
***
# 一. 三大基础组件

***
## 1. Root

> Project窗口 => 右键 => NGUI => Create 2D UI

用于分辨率自适应的根对象, 可以设置基本分辨率, 相当于设置UI显示区域
并且管理所有UI控件的分辨率自适应

### 1.1 相关参数

- Flexible 灵活模式
  - Minimum Height 屏幕小于该值时开始按比例缩放
  - Maximum Height 屏幕大于该值时开始按比例缩放
  - Shrink Portrait 竖屏时, 按宽度来适配
  - Adjust by DPI 使用dpi适配计算, 建议勾选
</br>

- Constrained 适用于移动设备
  因为移动设备都是全屏应用 不会频繁改变分辨率 只用适配不同分辨率的设备
  横屏勾选 高 fit  竖屏 勾选 宽 fit 一般就可以比较好的进行分辨率适应了 
  需要注意的是 背景图 一定要考虑 极限 宽高比来出 最大宽高比  19.9:9
</br>

- Constrained On Mobiles 是上面两者的综合体 适用于多平台发布的游戏和应用

***
## 2. Panel

### 2.1 相关参数
- Alpha 控制所有子UI元素的透明度
- Depth 控制该panel的层级, 层级高的后渲染会把层级低的先渲染的遮挡住
- Clipping 裁剪
- Sorting Layer 排序层

<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_08-50-12.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_08-51-21.jpg)

</center>

1. 没有Panel父对象, UI控件看不到
2. Panel一般用于管理面板, 控制层级
3. Panel可以有多个, 一般一个Panel管理一个面板

## 3. Event System(UICamera)

输入事件监听的基础

主要作用是让摄像机渲染出来的物体
能够接收到NGUI的输入事件
大部分设置不需要我们去修改

有了它我们通过鼠标 触碰 键盘 控制器 操作UI 响应玩家的输入


<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_09-01-33.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_09-09-24.jpg)

</center>

### 3.1 相关参数
1. EventSystem很重要，如果没有它，我们没有办法监听玩家输入
2. 创建UI时的 2DUI 和3DUI 主要就是摄像机的模式不一样
  EventSystem的2D和3D主要是 采用2D碰撞器 还是3D碰撞器 不能直接改变摄像机模式
***

# 二. 图集制作
***
## 4. 图集

> Project窗口 => 右键 => NGUI => Open Atlas Maker

新建一个图集后, 会有三个文件:
1. 图集文件
2. 图集材质
3. 图集图片
 
<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_09-43-09.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_09-47-44.jpg)


</center>

- NGUI中的最小图片控件Sprite要使用图集中的图片进行显示
- 图集 就是把很多单独的小图 合并为 一张大图 合并后的大图就是图集
- 目的：提高渲染性能

***
# 三. 三大基础控件
***
## 5. Sprite
<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_10-21-52.jpg)

</center>

### 5.1 代码控制图片

**(1)改变为当前图集中选择的图片**
```CSharp
public UISprite sprite;

sprite.spriteName = "图片名字";
```

**(2)改变为其它图集中的图片**
```CSharp
//先加载图集
NGUIAtlas atlas = Resources.Load<NGUIAtlas>("图集路径");
sprite.atlas = atlas;
//再设置图片
sprite.spriteName = "图集中图片名字";
```
***
## 6. Label 
<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_10-39-50.jpg)

</center>

### 6.1 参数相关
<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_10-38-53.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_10-41-10.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_10-50-38.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_10-56-23.jpg)

</center>

### 6.2 代码控制
```CSharp
public UILabel label;

label.text = "123123123123";
```
***
## 7. Texture

### 7.1 概念
Sprite只能显示图集中图片 一般用于显示中小图片, 如果使用大尺寸图片 没有必要打图集
直接使用Texture组件进行大图片显示

<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_11-09-04.jpg)

</center>

### 7.2 代码控制
```CSharp
public UITexture tex;

//加载图片 
Texture texture = Resources.Load<Texture>("图片名");
//改变图片
if (texture != null)
    tex.mainTexture = texture;
```

***
# 四. 组合控件
***
## 8. Button

### 8.1 所有组合控件的共同特点
1. 在3个基础组件对象 ==(Sprite, Label, Texture)== 上添加对应组件
2. 如果希望响应点击等事件 需要添加碰撞器

### 8.2 制作Button
1. 一个Sprite（需要文字再加一个Label子对象）
2. 为Sprite添加Button脚本
3. 添加碰撞器

<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_11-47-10.jpg)

</center> 

### 8.3 监听事件的两种方式
1.拖脚本
2.代码获取按钮对象监听
```CSharp
btn.onClick.Add(new EventDelegate(ClickDo));

btn.onClick.Add(new EventDelegate(() => {
    print("lambda表达式添加的 点击事件处理");
}));

public void ClickDo()
{
    print("按钮点击");
}
```
***

## 9. Toggle

单选框
<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_17-17-24.jpg)

</center>



### 9.1 Toggle制作
1. 2个Sprite 1父1子
2. 为父对象添加Toggle脚本
3. 添加碰撞器

### 9.2 相关参数

<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_16-47-37.jpg)

</center>

### 9.3 代码控制

监听事件的两种方式
1. 拖代码
2. 代码进行监听添加

```CSharp
public UIToggle tog;

void Start()
{
  tog.onChange.Add(new EventDelegate(Change));
}

private void Change()
{
    print("代码监听");
}
```
***
## 10.Input
<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_19-56-45.jpg)

</center>

### 10.1 制作Input
1. 1个Sprite做背景 1个Label显示文字
2. 为Sprite添加Input Field脚本
3. 添加碰撞器

### 10.2 相关参数

<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_20-05-11.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_20-08-30.jpg)

</center>

### 10.3 代码控制

```CSharp
public UIInput input;

input.onSubmit.Add(new EventDelegate(() =>
{
    print("完成输入 通关代码添加的监听函数");
}));

input.onChange.Add(new EventDelegate(() =>
{
    print("输入变化 通关代码添加的监听函数");
}));
```

***
## 11. PopupList

### 11.1 制作Popuplist
1. 一个sprite做背景 一个lable做显示内容
2. 添加PopupList脚本在Sprite
3. 添加碰撞器在Sprite
4. 关联lable做信息更新，选择Label中的SetCurrentSelection函数

### 11.2 相关代码

<center>

![alt text](/Unity/图片/NGUI/NGUI10-03_22-07-45.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_22-09-01.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-03_22-18-47.jpg)

</center>

### 11.3 代码控制
```CSharp
public UIPopupList list;

list.items.Add("新加选项");

list.onChange.Add(new EventDelegate(() => {

    print("代码添加的监听" + list.value);
}));
```


***

## 12. Slider

<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_10-17-43.jpg)

</center>

### 12.1 制作Slider

1. 3个sprite 1个做根对象为背景  2个子对象 1个进度 1个滑动块 
2. 设置层级
3. 为根背景添加Slider脚本
4. 添加碰撞器（父对象或者滑块）
5. 关联3个对象

### 12.2 代码控制

```CSharp


slider.onChange.Add(new EventDelegate(() => {
    print("通过代码监听" + slider.value);
}));

//这里是第一个学习NGUI控件以来直接用委托调用的
slider.onDragFinished += () => {
    print("拖曳结束" + slider.value);
}; 
```
***

## 13. Scrollbar和Progressbar

1. ScrollBar滚动条, 一般不单独使用, 都是配合滚动视图使用

<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_11-05-03.jpg)

</center>

2. ProgressBar进度条,  一般不咋使用, 一般直接用Sprite的Filed填充模式即可

<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_11-05-35.jpg)

</center>

### 13.1 制作Scrollbar
1. 两个Sprite 1个背景 1个滚动条
2. 背景父对象添加脚本
3. 添加碰撞器
4. 关联对象


### 13.2 制作Progressbar
1. 两个Sprite 1个背景 1个进度条
2. 背景父对象添加脚本
3. 关联对象

***
## 14. ScrollView

滚动视图

<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_15-02-24.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-04_15-08-39.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-04_15-51-03.jpg)

</center>

### 14.1 制作ScrollView
1. 直接工具栏创建即可 NGUI——Create——ScrollView
2. 若需要ScrollBar 自行添加水平和竖直
3. 添加子对象 为子对象添加Drag Scroll View和碰撞器
添加了Drag Scroll View脚本后不用去手动关联对应的父对象ScrollView, 脚本会自动关联

### 14.2 相关参数
<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_15-10-52.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-04_15-17-17.jpg)

</center>

### 14.3 自动对齐脚本Grid 参数相关


<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_15-53-34.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-04_15-54-22.jpg)

</center>


### 14.3 代码控制

- 用Grid自动排序ScrollView里的单元格
    NGUI 的 UIGrid 在 Start() 之后会自动执行一次 Reposition()
    如果 ScrollView 中的单元格堆叠在一起, 想要用 Grid 重新排版
```CSharp
public UIScrollView sv;

private void Start()
{
    for (int i = 0; i < 100; ++i)
    {
        GameObject cell = Instantiate(Resources.Load<GameObject>("Prefabs/Cell"));
        cell.transform.SetParent(sv.transform, false);
    }

    //使用Grid自动排序, 需要挂载Grid脚本
    sv.GetComponent<UIGrid>().Reposition();
}
```

- 更推荐用数学计算的方式排版
```CSharp
private void Start()
{
    closeButton.onClick.Add(new EventDelegate(() => { HideMe(); }));

    for (int i = 0; i < 100; ++i)
    {
        GameObject cell = Instantiate(Resources.Load<GameObject>("Prefabs/Cell"));
        cell.transform.SetParent(sv.transform, false);
        
        //不用Grid组件
        cell.transform.localPosition = new Vector3(120 * (i % 5), 120 * (i / 5), 0);
    }
}
```
为什么更推荐第二种去排版单元格, grid组件只能根据单元格的先后顺序来排版, 那问题来了, 如果我想让这个ScrollView以我自己定的规矩排版呢? 就比如根据等级和稀有度排版的结果都是不一样的,grid组件时做不到的

- 用等级排序
<center>

![alt text](/Unity/图片/NGUI/example1.jpg)

</center>

- 用稀有度排序

<center>

![alt text](/Unity/图片/NGUI/example2.jpg)

</center>

***
# 五. Anchor 锚点组件
***
## 15. Anchor

### 15.1 概念
是用于9宫格布局的锚点
1. 老版本——锚点组件——用于控制对象对齐方式
2. 新版本——3大基础控件自带 锚点内容——用于控制对象相对父对象布局

### 15.2 老版本——锚点组件
主要用于设置面板相对屏幕的9宫格位置
用于控制对象对齐方式
<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_20-40-50.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-04_20-41-25.jpg)

</center>


### 15.3 新版本——基础控件自带锚点信息
用于控制对象相对父对象布局
<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_21-02-15.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-04_20-49-58.jpg)

</center>
***

## 16. EventListener和EventTrigger

### 16.1 控件自带事件的局限性
目前复合控件只提供了一些常用的事件监听方式
比如, Button —— 点击, Toggle —— 值变化, 等等
如果想要制作 按下 抬起 长按等功能 利用现在的知识是无法完成的

### 16.2 NGUI事件 响应函数
- 添加了碰撞器的对象
- NGUI提供了一些利用反射调用的函数
</br>

- 经过 OnHover(bool isOver)
- 按下 OnPress(bool pressed)
- 点击 OnClick()
- 双击 OnDoubleClick()
- 拖曳开始 OnDragStart()
- 拖曳中  OnDrag(Vector2 delta)
- 拖曳结束 OnDragEnd()
- 拖曳经过某对象 OnDragOver(GameObject go)
- 拖曳离开某对象 OnDragOut(GameObject go)
- 等等等等

### 16.2 UIEventListener和UIEventTrigger
他们帮助我们封装了所有 特殊响应函数, 可以通过它进行管理添加

1. UIEventListener 适合代码添加

```CSharp
public UISprite A;

UIEventListener listener = UIEventListener.Get(A.gameObject);
listener.onPress += (obj, isPress) => {
    print(obj.name + "被按下或者抬起了" + isPress);
};
```

2. UIEventTrigger 适合Inspector面板 关联脚本添加

<center>

![alt text](/Unity/图片/NGUI/NGUI10-04_22-42-33.jpg)

</center>

3. UIEventListener和UIEventTrigger区别
   - Listener更适合 代码添加监听 Trigger适合拖曳对象添加监听
   - Listener传入的参数 更具体  Trigger就不会传入参数 我们需要在函数中去判断处理逻辑

***
# 六. NGUI进阶

## 17. DrawCall

### 17.1 概念
CPU(处理器)准备好渲染数据（顶点，纹理，法线，Shader等等）后
告知GPU(图形处理器-显卡)开始渲染（将命令放入命令缓冲区）的命令

### 17.2 如何降低DrawCall数量
在UI层面上, 打图集就是减少DrawCall次数的方法, 小图合大图——>即多个小DrawCall变一次大DrawCall

后面会学习在模型上减少DrawCall

### 17.3 制作UI时降低DrawCall的技巧
1. 通过NGUI Panel上提供的DrawCall查看工具(Panel => Show Draw Calls)
2. 注意不同图集之间的层级关系
3. 注意Label的层级关系

<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_12-12-28.jpg)

</center>

首先, 这里的层级关系是指Widget里的层级, 当多个UI的图片都是一个图集且这几个UI的层级的中间层级没有其他图集时,他们便会合并为一个DrawCall

- 合并DrawCall
    - 这三个sprite用的一个图集; A,B,C的层级分别为1,2,3; 所以只会有一个DrawCall
<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_12-21-18.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-05_12-22-57.jpg)

</center>

- 中间层级为其他图集
  - 此时我把B的图集改变了, DrawCall变成了3个, 其中第1个和第3个是一个图集

    <center>

    ![alt text](/Unity/图片/NGUI/NGUI10-05_12-26-13.jpg)
    ![alt text](/Unity/图片/NGUI/NGUI10-05_12-26-40.jpg)

    </center>

  - 同理, 中间不是Sprite, 而是Label的话也会打断合并DrawCall

    <center>

    ![alt text](/Unity/图片/NGUI/NGUI10-05_12-29-13.jpg)
    ![alt text](/Unity/图片/NGUI/NGUI10-05_12-31-21.jpg)

    </center>

*** 
## 18. NGUI字体

> NGUI => Open => Font Maker

<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_16-15-44.jpg)

</center>

### 18.1 作用
1. 降低DrawCall
2. 自定义美术字体

### 18.2 制作NGUI字体
NGUI内部提供了字体制作工具
 1. 根据字体文件 生成指定内容文字 达到降低DrawCall的目的
 2. 使用第三方工具BitmapFont生成字体信息和图集
   通过NGUI 字体工具使用第三方工具生成的内容制作字体
   达到自定义美术字体

### 18.3 Unity动态字体和NGUI字体如何选择
1. 文字变化较多用Unity动态字体 变化较少用NGUI字体
2. 想要减少DrawCall用NGUI字体 
3. 美术字用NGUI字体

***
## 19. NGUI缓动
> NGUI => Tween

### 19.1 概念
NGUI缓动 就是让控件交互时 进行缩放变化 透明变化 位置变化 角度变化等等行为
NGUI自带Tween功能来实现这些缓动效果

### 19.2 NGUI缓动的使用
1. 关键组件 Tween缓动相关组件
    此处以Tween中的Scale为例
    (下图中的Animation Curve打错字了, 是"动画曲线" 不是"动画取消")
<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_16-54-10.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-05_16-50-52.jpg)

</center>

2. 关键组件 Play Tween可以通过它让该对象和输入事件关联

> NGUI => Attach => Play Tween Script

<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_17-15-41.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-05_17-22-24.jpg)
![alt text](/Unity/图片/NGUI/NGUI10-05_17-23-06.jpg)

</center>

## 20. 模型和粒子

### 20.1 NGUI中显示3D模型
- 方法一：
使用UI摄像机渲染3D模型
  1. 改变NGUI的整体层级 为 UI层
  2. 改变主摄像机和NGUI摄像机的 渲染层级
  3. 将想要被UI摄像机渲染的对象层级改为 UI层
  4. 调整模型和UI控件的Z轴距离

</br>

- 方法二：
    使用多摄像机渲染 Render Texture

### 20.2 NGUI中显示粒子特效
1. 让Panel和粒子特效处于一个排序层
2. 在粒子特效的 Render参数中 设置自己的层级

***
# 七. 其他

***
## 21. NGUI 事件响应 播放音效
`PlaySound 脚本`

<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_19-15-31.jpg)

</center>

## 22. NGUI控件和键盘按键绑定
`KeyBinding 脚本`

<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_19-18-06.jpg)

</center>

## 23. PC端 tab键快捷切换选中
`KeyNavigation 脚本`

<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_19-21-49.jpg)

</center>

## 24. 语言本地化
`Localization脚本`

<center>

![alt text](/Unity/图片/NGUI/NGUI10-05_19-18-06.jpg)

</center>

1. 在Resources下创建一个txt文件 命名必须为Localization
2. 配置文件
3. 给想要切换文字的Label对象下挂载Localize 关联Key
4. 给用于切换语言的下拉列表下添加脚本LanguageSelection