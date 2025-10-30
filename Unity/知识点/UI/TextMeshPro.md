[toc]
***
# 一. 概述
[官方文档](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/index.html)
TextMeshPro(TMP)是Unity2020之后主要使用的2D、3D文本渲染工具,比UGUI中Text表现更好

1. TMP的渲染效果更好,可以提供更好的文本质量, 文本的清晰度更高、抗锯齿更强、字形更准确
2. TMP提供了更强大的富文本支持, 在文本中添加样式、颜色、链接、图片等更灵活
3. TMP可以使用自定义字体和样式
4. TMP提供了更灵活的文本布局控制
5. TMP虽然内存占用更多,但是性能表现更好,可以提高渲染效率
等等

***
# 二. UI文本
创建 TMP UI 文本对象

两种方式：
1. Hierarchy ——> UI ——> TextMeshPro相关控件
2. GameObject ——> UI ——> TextMeshPro相关控件

Canvas Renderer 中有一个 Cull Transparent Mesh(剔除透明网格):对于文本来说建议勾选上
***
## 2.1 参数相关

### 2.1.1 文本输入相关
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-29-46.jpg)

</center>

- Text Input：
文本输入框
输入的文本会在控件中显示
</br>

- Enable RTL Editor：
启用RTL编辑器
开启该选项后，可以从右向左显示文本
并且会在下方看到一个额外的输入框RTL Text Input
可以在其中查看反转的文本并直接对它进行编辑
</br>

- Text Style：
文本样式
可以设置文本控件的样式
    - H1、H2、H3：代表标题级别，数字越大，表示级别越低
    - C1、C2、C3：代表颜色级别，用于定义不同文本的颜色，通常用于区  信息
    - Quote：代表引用文本样式，一般表示引用别人的内容
    - Link：超链接文本格式样式
    - Title：标题样式
    - Normal：普通正文文本样式
</br>

### 2.1.2 字体相关
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-37-43.jpg)

</center>

- Font Asset：
字体资源
可以选择默认的字体资源
TMP中的字体可以自定义
</br>
- Material Preset：
材质预设
TMP字体的显示，都是基于材质球
我们可以切换材质球来达到不同的渲染效果
本质就是材质球上使用的Shader着色器不同
</br>
- Font Style：
字体的样式
  - B：加粗
  - I：斜体
  - U：下划线
  - S：删除线
  - ab：小写文本
  - AB：大写文本
  - SC：大写文本，但是以实际输入的字母大小显示
</br>
- Auto Size：
自动调节大小
消耗较大，尽量少用
勾选后出现更多设置
    - Min：字体最小大小
    - Max：字体最大大小
    - WD%：水平挤压字符，使它们更高，可以在一行中显示更多内容
    - Line：减少行间距，只能为负数
</br>

### 2.1.3 颜色相关
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-48-14.jpg)

</center>

- Vertex Color：
顶点颜色
材质和纹理的颜色会乘以该颜色
决定字体颜色
</br>

- Color Gradient：
颜色渐变
勾选后会出现新的参数
    - Color Preset：颜色预设

    - Color Mode：颜色模式
        - Single：和顶点颜色叠加
        - Horizontal Gradient：双色水平渐变
        - Vertical Gradient：双色垂直渐变
        - Four Corners Gradient：四色四角渐变
    - Colors: 根据颜色模式决定有几个颜色设置
</br>

- Override Tags：
启用此选项可忽略任何富文本标记更改文本颜色
一般不勾选

### 2.1.4 间距相关
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-48-56.jpg)

</center>

- Character：字符间距
- Word：单词间距
- Line：行间距（自动换行的）
- Paragraph：段落间距（段落由显式换行符定义）


### 2.1.5 对齐相关
第一行：水平对齐选项
- ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-49-36.jpg)左对齐、居中对其、右对齐
- ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-50-01.jpg)
  - 左对齐：
  增加单词和字符之间的间距填满每一行
  最后一行不会拉伸

  - 齐平：
  同上，最后一行会拉伸
- ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-50-32.jpg)
根据网格而不是文本指标使文本居中
差异和居中不是很明显
在某些情况下比常规的居中效果更好


第二行：垂直对齐选项

- ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-51-09.jpg)顶部对齐、中部对齐、底部对齐
- ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-51-38.jpg)
    基线对齐
    调整文本的位置，使第一行的基线（文本底部）与显示区域的中间对齐。
    这在处理单行文本时很有用
- ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-51-58.jpg)
    中线对齐
    类似中部对其
    此选项使用文本网格的边界确定垂直放置，在狭小的空间中很有用
- ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_20-52-16.jpg)
    卡普赖恩对齐
    调整文本的位置，使第一行的中间与显示区域的中间对齐


### 2.1.6 包裹或溢出相关
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_21-14-05.jpg)

</center>

- Wrapping
包裹
    Disabled：禁用后，不会因为控件大小的变化而自动换行了
    Enabled：启用后，当控件大小变化时，会自动换行去适应它（比如自动换行）
</br>

- Overflow
溢出
当文本不适合显示区域时
应该如何处理
    - Overflow：溢出
    扩展到显示区域外（但是如果启用了包裹，会自动换行）
    - Ellipsis：省略
    阶段文本可视范围外内容用省略号代替
    - Masking：遮罩
    和overflow类似，但是会隐藏显示区域外的所有内容
    - Truncate：截断
    超出范围的内容不再显示
    - Scroll Rect：滚动矩形
    此选项仅用于与较旧的 TextMesh Pro 项目兼容。对于新项目，请改用遮罩模式
    - Page：分页
    将文本剪切成多个页面，每个页面都适合显示区域
    您可以选择要显示的页面
    - Linked：联系
    将文本扩展到您选择的另一个 TextMesh Pro 游戏对象
    该Text显示不完，在另一个联系的Text对象中显示

### 2.1.7 UV映射相关
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_21-14-24.jpg)

</center>

- Horizontal Mapping: 水平映射
    - Character：
    在每个文字上水平拉伸纹理
    - Line：
    每条线的整个宽度上水平拉伸纹理
    - Paragraph：
    整个文本中水平拉升纹理
    - Match Aspect：
    水平缩放纹理，保持纵横比，不变形
</br>

- Vertical Mapping: 垂直映射
    - Character：
    在每个文字上垂直拉伸纹理
    - Line：
    每条线的整个宽度上垂直拉伸纹理
    - Paragraph：
    整个文本中垂直拉升纹理
    - Match Aspect：
    垂直缩放纹理，保持纵横比，不变形
</br>

- Line Offset：
设置为 线段、段落、匹配模式时，会出现该参数
用于控制偏移位置

### 2.1.8 额外设置相关
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-30_21-25-26.jpg)

</center>

- Margins：边缘设置
可以设置文本和文本容器之间的间距
负值可以让文本超出边框
也可以直接在Scene窗口中直接操作黄色边框
</br>
- Geometry Sorting：几何排序
决定TMP重合时如何进行排序
    - Normal：按照显示顺序排序
    当四边形重合时，靠摄像机近的显示在前面

    - Reverse：按相反的顺序绘制四边形
    重合时，离摄像机远的显示在前面
</br>
- Is Scale Static：告诉TMP文本系统该文本不会发生缩放相关变化
TMP会跳过与缩放相关的计算，从而减少CPU和GPU的负担，提升性能

    适用场景：适用于在场景中缩放比例固定的文本对象，可以开启该选项
</br>
- Rich Text：是否开启富文本，默认开启
开启后可以识别富文本相关的关键字
</br>
- Raycast Target：摄像检测目标
决定文本是否能被点击、触摸等事件响应
关闭后，触摸和点击会“穿透”
如果希望文本响应点击等事件，需要勾选
</br>
- Maskable：是否能被遮罩裁剪
勾选时，TMP会被Mask组件裁剪
取消勾选，不会被裁剪
</br>
- Parse Escape Characters：是否解析转义字符
如果开启文本会解析转义字符，否则无用
</br>
- Visible Descender：可见下降
使用脚本缓慢显示文本时，启用该选项
启用它可现实底部文本，并在显示新行时向上移动
需要把垂直对齐改为Bottom下部对齐
</br>
- Sprite Asset：Sprite 资源
允许文本中嵌入精灵2D图片，用于处理图文混排时很重要
比如嵌入表情符号等等
</br>
- Style Sheet Asset：管理和应用文本样式，使文本格式更加高效和统一
TMP_Style Sheet文件可以定义多种文本样式，并在多个文本组件中重复使用，确保一致性
</br>
- Kerning：自动调整字符间距，提高文本可读性和美感
</br>
- Extra Padding：额外填充
为文本的边界添加额外填充，避免文本与边界过于靠近，提升视觉效果
</br>

***

## 2.2 脚本相关
[更多API看官方文档](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/api/TMPro.TextMeshProUGUI.html)

### 2.2.1 脚本获取TMP UI组件
1. 需要引用TMP命名空间 `TMPPro`
2. TMP UI组件名为 `TextMeshProUGUI`

### 2.2.2 TMP UI组件常用属性
先声明一个TMP对象
```CSharp
public TextMeshProUGUI tmpUIText;
```

1. 文本内容 `text`
```CSharp
tmpUIText.text
```

2. 字体 `font`
3. 字体大小 `fontSize`
4. 颜色 `color`
5. 对齐方式 `alignment`

```CSharp
tmpUIText.alignment = TextAlignmentOptions.Center;
```

6. 行间距 `lineSpacing`
7. 是否启用富文本 `richText`

### 2.2.3 TMP UI组件常用方法
1. 设置文本内容，支持富文本格式
```CSharp
tmpUIText.SetText("<color=blue>Hello, World!</color>");
```
2. 强制重新构建文本网格，通常在文本内容或样式更改后使用

|展位|调用时机|
|---|---|
|Prelayout|布局前调用|
|Layout|布局时调用|
|PostLayout|布局后调用|
|PreRender|渲染前调用|
|LatePreRender|渲染后调用|
|MaxUpdateValue|最后调用|

```CSharp
tmpUIText.Rebuild(UnityEngine.UI.CanvasUpdate.Prelayout);
```

3. 强制更新文本网格,运行时动态更改文本时
```CSharp
tmpUIText.ForceMeshUpdate();
```

4. 获取文本中字符数
```CSharp
tmpUIText.text.Length;
```

### 2.2.4 UI事件监听
如果想要为TMP UI空间添加交互事件
可以用UGUI中EventTrigger的方式
```CSharp
void Start
{
    public void OnClickTmpUI()
    {
        print("文本被点击了");
    }
}

```

# 三. 3D文本相关

[更多API](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/api/TMPro.TextMeshPro.html)

创建方法
> Hierarchy => 右键 => 3D Object => TMP

## 3.1 3D文本和UI文本的区别
1. 组件不同
  3D: TextMeshPro
  UI: TextMeshProUGUI
</br>

2. 用途不同
  3D: 主要用于在3D场景中显示文字
  UI: 主要用于在UI中显示文字，具备UI相关的一些属性
</br>

3. 渲染方式
  3D: 直接渲染在场景上
  UI: 通过UGUI的Canvas系统渲染
</br>  

4. 交互方式
  3D: 一般通关**添加碰撞器**进行碰撞检测判断交互
  UI: 一般利用UI系统的交互规则，比如EventTrigger
</br>
如何选择？
文本需要与3D场景交互需要在3D场景上显示，选择3D文本 TextMeshPro,把他当成3D物体处理即可
文本需要在UI系统中使用，选择 TextMeshProUGUI, 把它当成UI组件使用即可
