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

