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

1. 强制更新文本网格,运行时动态更改文本时
```CSharp
tmpUIText.ForceMeshUpdate();
```

1. 获取文本中字符数
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
***

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

***

# 四. 字体资源相关

## 4.1 字体资源创建基本流程

方式一：基于字体文件进行字体资源创建（常用）
  1. 导入字体文件（后缀为 `.ttf` 或 `.otf` 格式的文件）
    一般放在文件夹 TextMesh Pro —> Fonts 中

  2. 选中该字体 右键 —> Create —> TextMeshPro —> Font Asset
    创建好后 一般放在文件夹 TextMesh Pro —> Resources —> nts & Materials 中


方式二：直接打开创建字体资源窗口创建
  工具栏 —> Window —> TextMeshPro —> Font Asset Creator
***

## 4.2 字体配置信息
由于这个知识点太长了, 我建议先把下面这个图片截图后贴在旁边对照着看

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_10-23-06.jpg)

</center>

- Update Atlas Texture:更新纹理图集
点击该按钮后，可以打开一个新窗口
用来配置字体的图集纹理信息
主要用于生成静态字体资源

    <center>

    ![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_10-22-31.jpg)

    </center>

    字体资源创建器
    主要作用：
    将Unity字体资源转换为TMP字体资源
    当生成了TMP字体资源后，是可以将用作源的Unity字体资源删除掉的
    但是删除前要确定不再需要使用它来重新生成TMP字体了

    - Source Font File：选择用于生成TMP字体资源的字体
    </br>
    - Sampling Point Size：采样点大小
    设置用于生成字体纹理的字体大小（以磅为单位）
        Auto Sizing：使用尽可能大的大小，常用设置
        Custom Size：自定义大小
    </br>
    - Padding：填充
    指定字体纹理中字符之间的间距（以像素为单位）
    填充越大，过渡越平滑。
    </br>
    - Packing Method：包装方式
    如何在字体纹理中调整字体布局
        - Fast：快优先
        字符打包速度快，但是字体的大小可能更小
        测试字体时可以使用它

        - Optimum：最佳优先
        尽可能合理利用纹理中的空间保持字符最大可能的字体大小
        正式要确定字体时，使用该方式
    </br>
    - Atlas Resolution：图集分辨率
    如果只有ASCII字符，用512*512即可。
    中文字体时，需要更大更多的图集。（比如1024*1024）
    </br>
    - Character Set：字符集
    确定想要生成字体纹理的字符集
        - ASCII：标准ASCII字符集（字母，数字，标点符号）
        - Extended ASCII：拓展ASCII字符集（增加了许多特殊字符和符号）
        - ASCII Lowercase：仅包含小写字母字符（a~z）
        - ASCII Uppercase：仅包含大写字母字符（A-Z）
        - Numbers + Symbols：包含数字（0-9）和常见的符号（如 !, @, #, $, % 等）
        - Custom Range：允许用户定义一个自定义字符范围，用户可以指定要包含的 Unicode 范围，例如选择某些特定的字符。输入十进制值或范围，比如：32-126,160,8230
        - Unicode Range（Hex）：允许用户输入 Unicode 范围的十六进制值，便于选择特定语言或字符集。输入十六进制值或范围，比如：4E00-9FA5
        - Custom Characters：用户可以手动输入要包含的具体字符，可以是任何字符（如字母、符号等）
        - Characters from File：从文本文件中读取字符，允许用户根据文件内容自动生成字符集，适合大量字符的处理
    </br>
    - Select Font Asset：选择对应的字体资源
    </br>
    - Character Sequence：当选择需要自己输入字体的的相关模式时，会出现
    </br>
    - Render Mode：渲染模式(SDF > Smooth > Color)
        - SMOOTH_HINTED:抗锯齿位图，字符像素和纹理像素对齐，以获得更清晰的结果，通常用于小字体或需要精确对齐的情况
        - SMOOTH:采用抗锯齿效果，提供平滑的边缘，适合大多数情况下的文本显示，能够在清晰度和性能之间取得良好平衡
        - COLOR_HINTED:基于颜色的抗锯齿效果，强调颜色的呈现，适合需要突出颜色的文本
        - COLOR:不使用抗锯齿，直接渲染文本的颜色。适合在高对比度或需要清晰边缘的场景中使用
        - RASTER_HINTED:位图渲染，通常用于较小字体，能够提供较高的清晰度，但在放大时可能出现模糊
        - RASTER:位图渲染，不进行抗锯齿处理，提供快速渲染，但在边缘可能出现锯齿现象
        - SDF:使用带符号的距离场技术渲染，能够在缩放时保持清晰度。适合需要大范围缩放的文本
        - SDF8:使用 8 位 SDF 渲染，支持更小的纹理，适合资源受限的环境
        - SDF16:使用 16 位 SDF 渲染，提供更高的清晰度和更细腻的边缘，适合高质量显示
        - SDF32:使用 32 位 SDF 渲染，具有最高的细节和清晰度，适合需要极高质量文本的情况
        - SDFAA_HINTED:使用抗锯齿的 SDF 渲染，提供平滑的边缘和清晰的字符显示
        - SDFAA:使用 SDF 渲染，但不进行抗锯齿处理，快速渲染，但可能出现锯齿
        - SDF 是 Signed Distance Field 的缩写，意为“带符号距离场”。在字体渲染中，SDF 是一种技术，用于表示文本的形状和边缘信息。
    </br>
    - Get Kerning Pairs：用于在字体资源中获取字偶间距调整（Kerning）对。字偶间距调整是指根据相邻字符的组合来调整它们之间的间距，以实现更好的视觉效果和可读性
    </br>
    - Generate Font Atlas：生成字体图集
    </br>
    - Save：保存图集
    </br>
    - Save as...：另存为
    </br>

- Family Name：字体名称
无法手动修改 创建字体资源时默认设置
</br>
- Style Name：样式名称
无法手动修改 创建字体资源时默认设置
</br>
- Point Size：字体大小（以磅Point，缩写pt为单位）
1磅等于1/72英寸（约0.353毫米）
在字体设计中，磅数用来表示字体的高度。
这个单位是一种用于测量字体大小和印刷文本的单位
</br>
- Scale：缩放
对字体进行缩放，1为原始大小，不缩放
</br>

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_10-12-14.jpg)

</center>

- Line Height：行高
表示每行文本的高度，通常是基于最大字形的高度计算的。决定了两行文本之间的距离
若 Line Height > Ascent Line + Descent Line ，
则会在线条之间创建间隙，不同行上的字符可能会重叠
</br>
- Ascent Line: 上升线
字形中最高的点到基线的垂直距离。表示子母中最高部分（大写字母或某字符的上部）的位置
控制字形可以延伸到基线上方的最大距离。
它对应了线条的顶部
</br>
- Cap Line:大写字母线
表示所有大写字母的顶部所处的水平线，通常略低于上升线
控制基线与大写字形顶部之间的距离
</br>
- Mean Line:中线
表示小写字母的顶部所处的水平线，表示小写字母的标准高度
圆角字形的顶部有时会略高于中线
</br>
- BaseLine:基线
所有字母字符所处的水平线，大多数字符都在这条线的上方，除了下延字符（g、y、q等）
控制基线的高度
基线是字符所在的水平线
</br>
- Descent Line:下行线
字形中最低的点到基线的垂直距离，表示子母中最低部分的水平线（比如字母 g 的底部）
控制字形可以延伸到基线以下的最大距离
</br>
- Underline Offset:下划线偏移
下划线相对基线的位置

- Underline Thickness:下划线粗细
控制下划线的粗细
</br>
- Strikethrough Offset:删除线偏移
控制删除线相对于基线的位置
</br>
- Superscript Offset:上标偏移量
使上标文本相对基线偏移
</br>
- Superscript Size:上标大小
相对于正常字体大小缩放上标文本
</br>
- Subscript Offset:下表偏移量
是下表文本相对基线偏移
</br>
- Subscript Size:下表大小
相对于正常字体大小缩放下表文本
</br>
- Tab Width:指定TAB字符的宽度

***
## 4.3 生成设置和图集纹理
生成设置：
主要用来设置字体资源使用的纹理是动态生成的还是静态生成的

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_11-10-51.jpg)

</center>

- Source Font File: 字体文件
</br>
- Atlas Population Mode: 纹理图集生成方式
    - Static：静态模式
    字体集在创建时需要一个固定的纹理图集
    适合字符已知且不需要动态变化的情况。
    如果你的游戏不存在中文，只有类似英文这种少量字符组成的文字时可以使用

    - Dynamic：动态模式
    字体纹理图集会在运行时根据需要动态生成，可以显示更多的字符
    适合中文项目，或者多语言项目
</br> 
- Sampling Point Size：采样点大小
设置用于生成字体纹理的字体大小（以磅为单位）
    - Auto Sizing：使用尽可能大的大小，常用设置
    - Custom Size：自定义大小
</br>
- Padding：填充
指定字体纹理中字符之间的间距（以像素为单位）
填充越大，过渡越平滑。
</br>
- Atlas Width:图集宽
</br>
- Atlas Height:图集高
</br>
- Multi Atlas Textures:是否采用多图纹理
允许在字体资源中生成多个纹理图集（Atlas），而不仅仅是一个。这样可以更灵活地管理不同字符集或不同字体样式
提高性能和内存使用效率，尤其是在需要渲染大量不同字符的情况下建议使用
</br>
- Clear Dynamic Data On Build: 在构建时清除动态生成的数据
这意味着如果你选择了动态字体模式，构建时会移除不再需要的动态字符数据，以节省内存
可以减少最终构建包的大小，确保只包含必要的字符数据
</br>
- Font Atlas:生成字符资源时创建的字体纹理图集
</br>
- Font Material:生成字体资源时创建的字体材质
***

## 4.4 字体粗细
字体粗细：主要用于控制字体粗体、斜体等情况下的文本外观
可以通关关联对应的字体资源进行设置不同情况下使用的内容

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_11-22-50.jpg)

</center>

- Regular Typeface: 常规字体
- ltalic Typeface: 斜体字体
- 100 - thin: 细
- 200 - Extra - Light: 超轻
- 300 - Light：轻
- 400 - Regular: 常规（仅限斜体）
- 500 - Medium: 中
- 600 - Semi - Bold: 半粗
- 700 - Bold: 粗体
- 800 - Heavy: 粗
- 900 - Black: 黑体
- Normal Weight:正常粗细, 没有字体资源用时使用的常规字体粗细
- Bold Weight:粗体粗细, 没有字体资源用时使用的粗体字体粗细
- Spacing Offset:间距偏移, 使用普通文本样式时，在字符之间的间隙偏移
- Bold Spacing:粗体间隙, 没有指定粗体资源时，粗体文本字符之间的间距偏移
- ltalic Style:斜体样式, 400 - Regular > Italic Style 变体指定字体资源没有设置时
    - TMP将使用常规样式字体资源中的字符倾斜该字段中的量来达到斜体效果
- Tab Multiple:多制表符, 该值乘以字体空格符的宽度，用来表示制表符的大小
***
## 4.5 其他相关设置
备用字体资源：
每个字体资源都包含有限数量的字符。当您使用当前字体资源不包含的字符时，
TMP会搜索回退字体列表，直到找到包含该字体资源的字体资源。
然后，text 对象使用该字体来呈现字符

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_11-45-36.jpg)

</center>

- Fallback Font Assets: 备用字体资源
    - Fallback List:
    列出所有备用字体资产。
    当主字体中缺少某些字符时，系统会按顺序尝试使用这些备用字体进行查找
</br>

- Character Table: 字符表
    - Character Search:字符查找
</br>

- Glyph Table: 字形表
    - Glyph Search:字形搜索
    - X、Y、W、H：字符在纹理中对应的位置范围
    - W、H：字形的宽高
    - BX、BY：相对基线的左上角位置
    - AD：和下一个字符间隔的宽
    - Scale：缩放大小
    - Atlas Index：图集索引在第几个图集中（开启了多图集时）
</br>

- Glyph Adjustment Table：字形调整表
字形调整表控制特定字符对之间的间距
    - Adjustment Pair Search:调整对搜索
    - Char 左和右:想要查找的字符
    - ID 左和右:唯一标识符，一般显示左右字符的ASCII十进制编码
    - OX 左和右:字符原点坐标X
    - OY 左和右:字符原点坐标Y
    - AX 左和右:字符的宽度
    - Add New Glyph Adjustment Record: 添加新的字形调整记录

***
# 五. 富文本标签
[富文本API](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/RichTextSupportedTags.html)

富文本标签是在很多文本处理系统中使用的标记语言
允许通过特定的标签来格式化文本内容。
这些标签可以控制文本的样式、颜色、大小和其他视觉效果
从而增强文本的表现力

它的写法类似HTML或XML标签

```xml
<标签名>文本内容</标签>
```
或
```xml
<标签名="值">文本内容</标签>
```

不同的标签会为文本带来不同的表现效果

## 5.1 主要作用
可以让我们在一个TMP文本控件中让一段文字呈现出各种不同的表现效果
让文本表现效果更具吸引力和生动性
常常用于游戏的 UI文本、聊天窗口、说明文本 等等文本显示相关的系统中

## 5.2 常用富文本标签
1. 换行
```xml
<br>
```

2. 文本加粗
```xml
<b></b>
```

3. 文本斜体
```xml
<i></i>
```

4. 加下划线
```xml
<u></u>
```

5. 改变大小
```xml
<size=数值></size>
```

6. 改变颜色
```xml
<color=#RGBA 16进制></color>
```

7. 对齐方式
```xml
<align=left、center、right、justified、flush></align>
```

8. 背景高亮
```xml
<mark=#RGBA 16进制></mark>
```

9.  透明度
```xml
<alpha=#A 16进制>
```

10. 全部大写
```xml
<allcaps></allcaps>
```

11. 改字体和材质（可选）
```xml
<font="字体名" material="材质名"></font>
```

12.加上标(平方)
```xml
<sup></sup>
```

13. 加下标(化学式)
```xml
<sub></sub>
```

14. 超链接
```xml
<link="链接"></link>
```
***
# 六. 样式表
样式表是TMP提供的一个和 富文本标签 配合使用的功能
我们可以利用 样式表 自定义一种文本样式
然后在富文本中使用
`<style="样式表名"></style>` 标签包裹想要应用该样式的文字
相当于可以重复利用样式，避免每次书写相同、冗余的富文本样式编码

样式表的本质是富文本标签，相当于是对富文本标签的复用

## 6.1 修改创建样式表
1. 修改默认样式表
    > Project窗口 => TextMesh Pro => Resources => Style Sheets => Default Style Sheet

    选中默认样式表配置后 可以在Inspector窗口修改 本质也是个ScriptableObject表
</br>

2. 创建样式表
    >Project窗口右键 => TextMeshPro => Style Sheet

    创建后，选中样式表文件 可以在Inspector窗口修改

## 6.2 样式表配置
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_18-47-24.jpg)

</center>

- Name: 样式表名
- HashCode：不可修改的唯一编码
- Opening Tags:富文本标签开头
- Closing Tags:富文本标签结尾
</br>

- Up、Down：上下选择当前样式
- +、-：增加删除新的样式
- Previous、Next：样式比较多时，用来翻页

## 6.3 样式表设置
每一个TMP控件中的额外设置中, 都可以关联对应的样式表资源
如果没有设置，会使用默认样式表

以下两种方法都可以改变为对应样式
- 为Text Input中的输出添加`<style="样式表名"></style>`标签
- 在Text Style中选择对应样式
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_18-54-05.jpg)

</center>

***
# 七. 颜色渐变预设

让文本对象重复使用相同的颜色渐变
避免每次对文本单独进行设置，提高开发效率

## 7.1 创建和使用
- 创建：

    > Project窗口右键 => TextMeshPro => Color Gradient

    创建好后，选中文件，在Inspector窗口即可开始编辑
</br>

- 使用：
在TMP文本中开启颜色渐变
拖入创建好的颜色渐变配置
注意：文本中颜色模式和配置文件中模式一致
***

# 八. 精灵图片资源(图文混排)
精灵图片资源是配合富文本标签使用的资源
它可以让我们在TMP文本中显示图片，达到图文混排的目的

我们只需要创建并配置好精灵图片资源
便可以利用富文本标签在文本中显示图片
## 8.1 创建

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_20-01-38.jpg)

</center>


1. 根据自己的需求建立一个图集纹理
    图集的
    - Texture Type 为 Sprite
    - Sprite Mode 为 Multiple

    并且我们需要在Sptie Editor中将图集中的图片划分为单独的Sprite(需要导入2D Sprite包)
</br>

2. 准备好图集文件后
    > 在Project窗口中选中图集后 => 右键 => Text Mesh Pro => Sprite Asset

    创建后，一般我们需要为每个图片进行
    - 名字设置
    - 位置宽度相关设置
  
    最好配合着使用来配置, 其中的参数和字体参数类似
    关键参数：
    - BX、BY：相对于基线的原点的左上角
    - AD：放置下一个内容时往右前进的位置

    为了方便，可以通过最下方的全局偏移和缩放进行设置

## 8.2 使用
直接在TMP文本控件中输入sprite相关的富文本标签便可以使用

一般不改以下内容
- 一个决定了它在图集中的采样范围和位置
- 一个决定了图片的宽高
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_20-10-43.jpg)

</center>

默认资源中获取图片
- `<sprite index=图片ID color=#RGBA的16进制(可选)>`
- `<sprite name="图片名" color=#RGBA的16进制(可选)>`
- `<sprite=图片ID color=#RGBA的16进制(可选)>`

指定资源中获取图片
- `<sprite="资源名" index=图片ID color=#RGBA的16进制(可选)>`
- `<sprite="资源名" name="图片名" color=#RGBA的16进制(可选)>`

也可以将精灵图片资源直接关联给对应的TMP文本
若不关联，将使用默认资源
***

# 九. TMP基础设置
> Project Settings => TextMeshPro => Settings

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_19-13-46.jpg)

</center>

- Default Font Asset：默认字体设置
    - Path：字体资源的存储位置

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_19-14-33.jpg)

</center>

- Fallback Font Assets：默认备用字体资源

- Fallback Material Settings: 备用材质设置
    - Match Material Presets：
    匹配材质预设
    启用后备用字体中的字形与主字体的样式匹配
    让主字体和后备字体看起来类似

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_19-15-19.jpg)

</center>

- Dynamic Font System Setting：动态字体系统设置
    - Get Font Features at Runtime：运行时获取字体功能
    - Missing Character Unicode：当找不到字符时使用的替代字符, 默认0表示是一个正方形的轮廓
    - Disable warnings：启用后可避免Unity为每个缺失字形记录的警告

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_19-16-12.jpg)

</center>

- Text Container Default Setting：文本容器默认设置
    - TextMeshPro：3D文本默认大小
    - TextMeshPro UI：UI文本默认大小
    - Enable Raycast Target：是否默认为 文本对象启用摄像目标检测
    - Auto Size Text Container：启用后会自动调整文本容器大小
    - Is Object Scale Static：是否是静态缩放
</br>

- Text Component Default Setting：文本组件默认设置
    - Default Font Size：默认字体大小
    - Text Auto Size Ratios：文本自动调整大小比例
    - Word Wrapping：是否启用自动换行
    - Kerning：是否启用字偶间距调整
    - Extra Padding：是否额外进行填充，可以降低字符在sprite的边界处被截断的几率
    - Tint All Sprites：为所有精灵图片着色
    - Parse Escape Sequence：是否解析转义字符

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_19-16-39.jpg)

</center>

- Default Sprite Asset：默认精灵图片资源
    - Missing Sprite Unicode：精灵缺失时采用的替代付，默认为方形轮廓
    - IOS Emoji Support：是否支持IOS表情符号
    - Path：精灵图片资源存储位置
</br>

- Default Style Sheet：默认样式表
    - Path：样式表资源存储位置
</br>

- Color Gradient Presets：默认颜色渐变预设
    - Path：颜色渐变预设存储位置
</br>

- Line Breaking for asian languages：用于处理亚洲语言的换行规则
    - Leading Characters：前导符号
    一般不允许这些符号出现在开头 
    遇到这些字符触发自动换行时，不触发换行，让其显示在尾部
    - Following Characters：尾随符号
    一般不允许这些符号出现在行的结尾，如果这些符号在行的结尾出现自动换行了
    会将这些符号放倒下一行
</br>

- Korean Language Options：韩语相关设置
    - Use Modern Line Breaking：使用现代换行，启用后将采用更符合韩语规则的换行规则
</br>
***

# 十. 材质球相关

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-24-23.jpg)

</center>


## 10.1 概念
SDF 是 有符号距离场 (Signed Distance Field) 的缩写
- 有符号(Signed)：指的是距离可以为正或负，表示一个点位于边界的内部（负值）还是外部（正值）
- 距离(Distance)：表示每个像素点到字符边缘的距离
- 场(Field)：指的是整个字体或图形周围的距离值的分布

是一种用于高质量文本和图形渲染的技术
尤其适用于缩放或在低分辨率下保持边缘平滑的情况

它的本质就是在一个Shader（着色器）中利用SDF相关算法规则来渲染文字
SDF 技术生成的字体纹理并不是普通的位图，而是基于每个像素到字体边缘的距离值。
这些距离值存储在纹理的灰度通道中，代表每个像素到字符边缘的距离信息。
然后在渲染时，着色器根据这些距离值动态计算字体的边缘，最终渲染出平滑的字符轮廓。

主要在TMP中用于生成和渲染文本，能让字体在任意大小或距离下保持清晰和锐利的效果

## 10.2 SDF材质球指什么
我们创建的字体资源使用的材质球
本质上就是一个使用了SDF相关Shader的材质球
利用该Shader渲染出来的字体效果会更好
并且该Shader提供了很多可以被配置的参数
我们可以在对应字体的材质球中修改这些参数
从而让我们的字体实现一些更复杂的美术表现效果


## 10.3 SDF材质球和基础设置(Face)
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-23-45.jpg)

</center>

- Color:  文本表面颜色，该颜色会和TMP组件中设置的颜色相乘进行叠加

- Texture：可以为文本添加贴图
    - Tiling—平铺（缩放） 
    - Offset—偏移  
    - Speed—移动速度（可以实现滚动效果 可以配合UV配置使用）

- Softness：边缘柔和度，可以让文字边缘产生模糊感

- Dilate：拓展，类似改变粗细的效果

***
## 10.4 边缘线(Outline)和阴影(Underlay)
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-34-27.jpg)

</center>

- Color：边缘线颜色
- Texture：边缘线纹理，会用纹理颜色和轮廓颜色相乘叠加
- Thickness：轮廓粗细，值越大越粗

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-34-54.jpg)

</center>

- Underlay Type：底图阴影类型
    - None：无阴影
    - Normal：正常标准的底图样式
    - Inner：反转底图，用原始文本遮罩它
</br>

- Color：底图阴影的颜色
    - Offset X：X偏移
    - Offset Y：Y偏移
    - Dilate：拓展，相当于粗细
    - Softness：边缘柔和度，让阴影边界产生模糊感


***
## 10.5 照明(Lighting)
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-37-06.jpg)

</center>

- Bevel：斜面设置
- Local Lighting：本地照明设置
- Bump Map：凹凸贴图设置
- Environment Map：环境设置

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-37-50.jpg)

</center>

- Outer Bevel：外斜面, 让字体产生带有倾斜侧面的凸起效果, 相当于向中间凸起来
- Inner Bevel：内斜面, 轮廓凸起的文本, 相当于向中间凹进去
- Amount：陡峭程度
- Offset：位置偏移
- Width：斜面大小
- Roundness：让斜面更加平滑，产生更多角度区域
- Clamp：限制斜面最大高度

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-38-08.jpg)

</center>

- Light Angle: 光照角度，模拟局部光的角度
- Specular Color：镜面反射颜色
- Specular Power：镜面发射强度
- Reflectivity Power：反射强度，值越大越能反应周围环境的颜色
- Diffuse Shadow：漫反射阴影，调整整体阴影的等级，值越高，阴影越强
- Amblent Shadow：环境阴影，调整环境光照水平

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-38-30.jpg)

</center>

- Texture：凹凸贴图
- Face：凹凸影响程度
- Outline：凹凸贴图对文本轮廓的影响程度

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-39-01.jpg)

</center>

- Face Color：立方体贴图对文本的颜色影响，会和文本进行颜色叠加
- Outline Color：立方体贴图对文本的轮廓颜色影响，会和轮廓进行颜色叠加
- Texture：环境的立方体贴图
- Rotation：旋转环境贴图

***
## 10.6 发光(Glow) 和 调试设置(Debug Settings)
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-45-07.jpg)

</center>

- Color：发光颜色
- Offset：发光效果中心偏移值
- Inner：发光效果向内延伸距离
- Outer：发光效果向外延伸距离
- Power：发光强度

<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro10-31_21-45-28.jpg)

</center>

- Font Atlas：字体图集
- Gradient Scale：渐变比例
- Texture Width/Height：纹理宽高
- Scale X\Y：SDF比例乘数，越大越清晰，越小越模糊
- Sharpness：清晰度
- Perspective Filter：透视过滤器, 使用透视摄像机时，调整该设置，可以- 让文本看起来更柔和
- Offset X、Y：每个字符顶点位置的偏移量
- Mask：蒙版
    - Mask off: 关闭蒙版
    - Mask Hard:较硬蒙版
    - Mask Soft:柔和蒙版
- Clip Rect：剪辑矩形，用于设置遮罩矩阵范围
- Stencil ID：模板ID
- Stencil Comp：模板成分
- Uset Ratios：使用率
- Cull Mode：裁剪模式

***

# 十一. 工具类相关

## 11.1 TMP_TextEventHandler类
<center>

![alt text](/Unity/图片/TextMeshPro/TextMeshPro11-01_10-30-05.jpg)

</center>

它是 TextMeshPro 中提供的一个交互工具类
主要用于处理用户和TMP文本之间的交互事件
主要作用是监听并响应 TMP 文本中的特定区域或标签（如链接 <link> 和特定字符）
的点击和鼠标悬停事件
这个类可以让你对文本的某些部分进行交互式操作，适用于创建如超链接、工具提示、弹出信息等效果

### 11.1.1 使用
- 链接：`onLinkSelection` —— 当用户悬停超链接时触发
- 字符：`onCharacterSelection` —— 当用户悬停某个字符时触发
- 单词：`onWordSelection` —— 当用户悬停某个单词时触发
- 行：`onLineSelection` —— 当用户悬停某一行文本时触发
- 精灵图片：`onSpriteSelection` —— 当用户悬停某一精灵图片时触发

### 11.1.2 代码控制
**各个监听函数的参数含义：**
1. On Character Selection (Char, Int32)
    - Char: 被选中的字符
    - Int32: 该字符在文本字符串中的索引位置
</br>

2. On Sprite Selection (Char, Int32)
    - Char: 被选中的精灵字符（TMP 中的内嵌精灵）
    - Int32: 该精灵字符在文本字符串中的索引位置
</br>

3. On Word Selection (String, Int32, Int32)
    - String: 被选中的完整单词
    - 第一个 Int32: 单词的第一个字符在文本中的索引位置
    - 第二个 Int32: 单词的长度（字符数）
</br>

4. On Line Selection (String, Int32, Int32)
    - String: 被选中的整行文本
    - 第一个 Int32: 该行第一个字符在文本中的索引位置
    - 第二个 Int32: 该行的长度（字符数）
</br>

5. On Link Selection (String, String, Int32)
    - 第一个 String: URL
    - 第二个 String: 链接的显示文本
    - Int32: 链接在文本中的索引位置
</br>

在对应的监听事件里添加相应的函数就行了
```CSharp
private void Start()
{
    TMP_TextEventHandler tmpHandler = gameObject.GetComponent<TMP_TextEventHandler>();
    tmpHandler.onLinkSelection.AddListener(Link);
    tmpHandler.onCharacterSelection.AddListener(Character);
    tmpHandler.onSpriteSelection.AddListener(SpriteTest);
    tmpHandler.onWordSelection.AddListener(Word);
    tmpHandler.onLineSelection.AddListener(Line);
}

public void Character(char ch, int index)
{
    print("----------------------字符------------------------");
    print(ch);
    print(index);
}

public void SpriteTest(char ch, int index)
{
    print("-----------------------图片-----------------------");
    print(ch);
    print(index);
}

public void Word(string str, int i1, int i2)
{
    print("----------------------单词------------------------");
    print(str);
    print(i1);
    print(i2);
}

public void Line(string str, int i1, int i2)
{
    print("-----------------------行-----------------------");
    print(str);
    print(i1);
    print(i2);
}

public void Link(string linkInfo, string linkText, int index)
{
    print("------------------------链接----------------------");
    print(linkInfo);
    print(linkText);
    print(index);
}
```

***
## 11.2 TMP_TextUtilities
它是 TextMeshPro 中提供的一个实用工具类
包含多个常用方法，主要用于获取指定位置的文本信息
我们主要在点击文本时，利用该类来获取点击到的具体内容

### 11.2.1 常用API
下面的方法返回的都是索引值, **如果没有获取到信息，返回的索引为-1**
利用获取到的索引可以在TMP文本控件中的textInfo属性中的
- linkInfo
- wordInfo
- characterInfo
- lineInfo

来获取信息

1. 获取给定位置文本中的具体信息
   - 获取链接索引：int FindIntersectingLink(TMP_Text text, Vector3 position, Camera camera)
   - 获取单词索引：int FindIntersectingWord(TMP_Text text, Vector3 position, Camera camera)
   - 获取单字符索引：int FindIntersectingCharacter(TMP_Text text, Vector3 position, Camera camera)
   - 获取行索引：int FindIntersectingLine(TMP_Text text, Vector3 position, Camera camera)
</br>

2. 获取离给定位置最新的文本中的具体信息
   - 获取链接索引：int FindNearestLink(TMP_Text text, Vector3 position, Camera camera)
   - 获取单词索引：int FindNearestWord(TMP_Text text, Vector3 position, Camera camera)
   - 获取单字符索引：int FindNearestCharacterOnLine (TMP_Text text, Vector3 position, Camera camera)
   - 获取行索引：int FindNearestLine(TMP_Text text, Vector3 position, Camera camera)

### 11.2.2 代码控制
这里以获取链接ID为例

- 先让类继承想要功能的接口, [相关知识在UGUI的第五章](/Unity/知识点/UI/UGUI.md)
- 然后去实现相关成员方法

```CSharp
public class HandleTest : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI tmpUIText;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpUIText, eventData.position, null);
        if (linkIndex != -1)
        {
            print(tmpUIText.textInfo.linkInfo[linkIndex].GetLinkID());
        }
    }
}
```