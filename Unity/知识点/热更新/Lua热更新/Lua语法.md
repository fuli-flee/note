[toc]

***
# 一. 基础语法
[Lua仓库](https://luabinaries.sourceforge.net/download.html)
[Github](https://github.com/rjpcomputing/luaforwindows/releases)

## 1.1 注释
```Lua
-- 单行注释

--[[
多行
注释
]]

--[[
多行
注释
]]--

--[[
多行
注释
--]]
```

## 1.2 变量
Lua中变量都不需要声明变量类型, 它自己会判断
```lua
--这里用number来举例
a = 1
a = 1.2
```

- 简单变量类型: nil, number, string , boolean

注意:
- nil可以理解为null, **但是不同的是nil是变量类型**
- number能代表所有数值, 整形, 浮点型,双精度都能往里装
- lua没有char, 全都是string, 所以字符串用双引号 `""` 或者单引号 `''` 都行
- 用type函数能查看变量类型, 其返回值类型是string
```lua
a = nil
print(type(a)) --输出 nil
print(type(type(a))) -- 输出 string
```
- Lua中使用没有声明过的变量, 不会报错, 默认值是nil
```lua
--b重来没有声明过
print(b) --输出nil
```
***
## 1.3 string

- 获取字符串长度: `#`
    ```lua
    s = "abcd"
    print(#s) -- 4

    s = "abcd以"
    print(#s) -- 7
    ```

- 多行打印
    ```lua
    print("123\n123")
    s = [[
    谁
    有
    多
    余
    资
    金
    ]]
    print(s)
    ```
- 字符串拼接
    ```lua
    s = "123".."456"
    s1 = "123"
    s2 = 111
    print(s) -- 123456
    print(s1..s2) -- 123111

    print(string.format("提丰的DPS是%d"),799) -- 提丰的DPS是799
    ```
|占位符|说明|
|:---:|:---:|
|%s|字符串（所有类型自动转为字符串）|
|%d|十进制整数（四舍五入取整）|
|%i|同 %d（兼容 C 语法，无区别）|
|%f|浮点数（默认保留 6 位小数）|
|%x|十六进制整数（小写字母 a-f）|
|%X|十六进制整数（大写字母 A-F）|
|%o|八进制整数|
|%c|ASCII 码转字符|

- 别的类型转字符串
    ```lua
    a = true
    print(tostring(a))
    ```
- 公共方法
    ```lua
    --小写转大写
    str = "abdsCds"
    print(string.upper(str)) -- ABDSCDS

    --大写转小写
    print(string.lower(str))

    --翻转字符串
    print(string.reverse(str))

    --字符串索引查找
    print(string.find(str, "Cds"))-- 输出 5	7

    --截取字符串
    print(string.sub(str, 3, 4))-- 输出 ds

    --字符串重复
    print(string.rep(str, 2))-- 输出 abdsCdsabdsCds

    --字符串修改
    print(string.gsub(str, "ds", "**")) -- 输出 ab**C**	2

    --字符转 ASCII码
    a = string.byte("Lua", 1)-- 输出 76
    print(a)
    --ASCII码 转字符
    print(string.char(a))--输出 L
    ```

注意:
- 一个中文字符占3位
- lua中支持转义字符
- lua里的索引都是从1开始的

***
## 1.4 运算符
- 算数运算符
    ```lua
    print("123.4" + 1) -- 124.4
    print(1 / 2) -- 0.5
    print("123.4" / 2) -- 61.7
    print("123.4" % 2) -- 1.4

    --幂运算
    print("幂运算" .. 2 ^ 5) -- 32
    print("123.4" ^ 2) -- 15227.56
    ```
</br>

- 条件运算符
    `>` `<` `>=` `<=` `==` `~=`
</br>

- 逻辑运算符
    与: `and`
    或: `or`
    非: `not`
</br>

- 位运算符
lua不支持位运算符, 需要自己实现
</br>

- 三目运算符
lua不支持三目运算符, 需要自己实现

注意:
- lua中没有自增`++`和自减`--`
- 没有复合运算符: `+=`, `-=`, `*=`, `/=`, `%=`
- 字符串 可以进行 算数运算符操作 会自动转成number
- `^` lua中 该符号 是幂运算; 不是异或
- lua中不等于为 `~=`
- lua中也会短路, 也就是`print( false and true)`对于这个条件来说如果满足第一个,后面的条件就不会判断和执行了

***
## 1.5 条件分支语句

```lua
a = 10

if a < 5 then
	print("1234")
elseif a == 6 then
	print("6")
elseif a == 7 then
	print("7")
else
	print("other")
end
```

注意: 
- lua没有`switch`

***
## 1.6 循环

- while : **while 条件 do ..... end**
    ```lua
    num = 0
    
    while num < 5 do
        print(num)
        num = num + 1
    end
    ```
- do while : **repeat ..... until 条件 （注意：条件是结束条件）**
    ```lua
    num = 0

    repeat
        print(num)
        num = num + 1
    until num > 5 --满足条件跳出 结束条件
    ```
- for : **for `变量名=初始值`, `结束值(包含)`, `自增量` do ..... end**
    ```lua
    for i =2,5 do --默认递增 i会默认+1
	    print(i)
    end
    ```

注意:
- `repeat ..... until 条件` 这里的条件是结束条件
- for循环语句, 变量默认每次循环加1
- for每次循环都会判断当前变量是否大于结束值

***
## 1.7 函数
- 基本格式
    ```lua
    function 函数名()
        ...
    end
    ```

- 函数的类型
```lua
F5 = function( )
	print("123")
end
print(type(F5)) --输出为function


-- 有参数的函数, 传入参数不需要声明变量类型
-- 也就意味着你传参想传啥就传啥
function F3(a)
	print(a)
end
F3() -- 输出 nil
F3(1,2,3) -- 直接把2和3丢弃了


-- 有返回值的函数, 可以返回多个值
function F4(a)
	return a, "123", true
end
temp, temp2, temp3, temp4 = F4("1")--可以用多个变量去接收
print(temp)
print(temp2)
print(temp3)
print(temp4) -- 不会报错, 无非就是没有接到返回值, 输出nil
```

- 函数的重载
    lua不支持重载, 默认调用最后一个声明的函数
</br>

- 变长参数
    ```lua
    function F7( ... )
        --变长参数使用 用一个表存起来再用
        arg = {...}
        for i=1,#arg do
            print(arg[i])
        end
    end
    F7(1,"123",true,4,5,6)
    ```
- 函数嵌套
    ```lua
    function F8()
        return function()
            print(123);
        end
    end
    f9 = F8()
    f9()

    --闭包
    function F9(x)
        --改变传入参数的生命周期
        return function(y)
            return x + y
        end
    end

    f10 = F9(10)
    print(f10(5))
    ```

注意: 
- 如果你传入的参数 和函数参数个数不匹配, 不会报错 只会补空nil 或者 丢弃
- 多返回值时 在前面声明多个变量来接取即可
  - 如果变量不够 不影响 值接取对应位置的返回值
  - 如果变量多了也不影响,会直接赋为nil
- lua中 函数不支持重载 ,默认调用最后一个声明的函数
***

## 1.8 table
lua没有复杂类型, 只是用表去表示它们的特征而已
就像栈(数据结构), 你可以用两个栈去倒腾, 能模拟出队列的用法, 是一个道理

### 1.8.1 数组
- 基本结构
    ```lua
    a = {{1,2,3},{4,5,6}} 
    print(a[2][1]) -- 输出4

    -- 遍历输出
    for i=1,#a do
        b = a[i]
        for j=1,#b do
            print(b[j])
        end
    end
    ```
- 自定义索引
    即使其中默认元素索引被自定义索引给断开了, 它也能呢继续默认索引
    ```lua
    a = {[0] = 1,2,3,[-1]=4,5}
    print(a[0]) -- 1
    print(a[-1]) -- 4
    print(#a) -- 3

    print(a[1]) -- 2 
    print(a[2]) -- 3
    print(a[3]) -- 5
    ```
- 自定义索引的坑
    自定义索引断一个数值还能接上, 断两个就不接了
    ```lua
    a = {[1] = 1,[2] = 2,[4] = 4,[5] = 5}
    print(#a) --输出 5

    b = {[1] = 1,[2] = 2,[5] = 5,[6] = 6}
    print(#b) --输出 2

    c = {[1] = 1,[2] = 2,[4] = 4,[6] = 6}
    print(#c) --输出 6
    ```

注意:
- lua中的索引默认从1开始
- lua中所有的复杂类型都是table（表）
- 中间元素为nil, 会断掉长度, 使其获取不准确

***
### 1.8.2 ipairs迭代器遍历
ipairs遍历是 从1开始往后遍历的, 且需要索引连续
只能找到连续索引的 键 如果中间断序了 它也无法遍历出后面的内容
```lua
a = {[0] = 1, 2, [100]=3, 4, 5, [5] = 6}
for i,k in ipairs(a) do
    print("ipairs遍历键值 "..i.."_"..k)
end
```
输出结果为
```txt
ipairs遍历键值 1_2
ipairs遍历键值 2_4
ipairs遍历键值 3_5
```

**ipairs迭代器遍历键**
```lua
for i in ipairs(a) do
	print("ipairs遍历键"..i)
end
```
输出结果为
```txt
ipairs遍历键 1
ipairs遍历键 2
ipairs遍历键 3
```

### 1.8.3 pairs迭代器遍历
```lua
--它能够把所有的键都找到 通过键可以得到值
for i,v in pairs(a) do
	print("pairs遍历键值"..i.."_"..v)
end
```
输出结果为
```txt
pairs遍历键值 1_2
pairs遍历键值 2_4
pairs遍历键值 3_5
pairs遍历键值 0_1
pairs遍历键值 5_6
pairs遍历键值 100_3
```

**pairs迭代器遍历键**
```lua
for i in pairs(a) do
	print("pairs遍历键"..i)
end
```
输出结果为
```txt
pairs遍历键 1
pairs遍历键 2
pairs遍历键 3
pairs遍历键 0
pairs遍历键 5
pairs遍历键 100
```

**补充**
table 的储存分为**数组**部分和**哈希表**部分
- 数组部分，从 1 开始作整数数字索引。这可以提供紧凑且高效的随机访问。
- 哈希表部分，唯一不能做哈希键值的是 nil 

在 Lua 中，使用 pairs 遍历 table 时，遍历顺序是不确定的，Lua 只保证每个键值对都会被访问一次，但不保证顺序。

不过，pairs 的实现通常是：

1. 先遍历 数组部分（连续正整数键 1, 2, 3, …）

2. 再遍历 哈希部分（其他所有键，包括 0、负数、不连续的正整数、字符串等），顺序取决于它们在哈希表中的存储位置，这与插入顺序、哈希算法、表大小等有关。

所以

- 数组部分（从 1 开始的连续正整数索引）按数字顺序遍历。

- 哈希部分（其他键）遍历顺序不保证，可能因 Lua 版本、表大小、插入顺序等变化。

- 不要依赖 pairs 的顺序，如果需要有序遍历，应自己维护键的顺序（例如用一个数组存储键并排序）。

***
### 1.8.4 字典

- 声明
    ```lua
    a = {["name"] = "扶离", ["age"] = 21, ["1"] = 5}

    --访问值
    print(a["name"]) -- 扶离
    print(a.name) -- 扶离
    ```

- 修改
    ```lua
    a["name"] = "STL";
    print(a["name"]) -- STL
    ```
- 新增
    ```lua
    a["sex"] = false
    print(a["sex"]) -- false
    ```
- 删除
    ```lua
    a["sex"] = nil
    print(a["sex"]) -- nil
    ```
- 遍历
    ```lua
    -- 遍历一定用pairs
    -- 以下三种方式都能输出键值对
    for k,v in pairs(a) do
        print(k,v)
    end

    for k in pairs(a) do
        print(k)
        print(a[k])
    end

    --这种不光输出值, 键值都会打印出来
    for _,v in pairs(a) do
        print(_, v)
    end
    ```

注意: 
- 虽然可以通过`.`成员变量的形式得到值 但是不能是数字, 也就是不存在 `a.1` 这种写法去获取值

***

### 1.8.5 类和结构体
Lua中是默认没有面向对象的 需要自己来实现
```lua
Student = { 
	--成员变量
	age = 20, 
	subject = "Math",

	--成员函数
	GrowUp = function()
		--这样写 这个age 和表中的age没有任何关系 它是一个全局变量
		print(age) -- 输出nil 

		--想要在表内部函数中 调用表本身的属性或者方法
		--一定要指定是谁的 所以要使用 表名.属性 或 表名.方法
		print(Student.age)
	end,

	Learn = function(t)
        --想要在函数内部调用自己属性或者方法的 方法
        --把自己作为一个参数传进来 在内部 访问
		print(t.subject)
	end
}

Student.Learn(Student);

-- 声明表过后 可以在表外新增声明
Student.name = "扶离"
Student.Speak = function()
	print("说话")
end
function Student.Speak2()
	print("说话2")
end
```

麻烦, 太麻烦了, 调用自己的属性还要点明是自己的
这就跟你喊你儿子, 本来喊一声"儿子" 就知道是谁了, 现在偏要让你喊 "xxx的儿子", 你儿子才认你一样怪

解决方案:
- 冒号调用方法 会默认把调用者 作为第一个参数传入方法中
- self 表示 默认传入的第一个参数
```lua
--那调用成员函数就变得简单了
Student:Learn();

--那么对于在外部调用也能起效了
function Student:Speak2()
	print(self.."说话2") -- 扶离说话
end
```
注意:
- Lua 里的 self 本身没有特殊含义，它**不是关键字**, 它只是一个约定俗成的变量名和语法糖，只有满足特定条件，才会被自动赋值。
- Lua 中要让 self 自动作为第一个参数传入，必须满足以下两种场景之一：
   1. 用 `:` 定义和调用函数
   2. 用 `.` 定义和调用函数时手动传入 self
- `:` 不能用在赋值语句的左侧,只能用在 `function 表:方法()` 或 `表:方法() （调用）`中
  - 所以不存在`Student:Speak = function ()`它会错误地当成 `Student.Speak(Student)`
  - 所以要么写成上面的标准形式, 要么写成`Student.Speak = function(self)`
- 附一张Lua的关键字表
```txt
and       break     do        else      elseif    end
false     for       function  goto      if        in
local     nil       not       or        repeat    return
then      true      until     while
```
***
### 1.8.6 API
- 插入
    向 table 的 “数组部分” 插入元素（默认插在末尾）
    ```lua
    -- t（目标表）、pos（插入位置，可选，默认 #t+1）、value（插入值）
    table.insert(t, [pos], value)
    ```
- 移除
    删除 table “数组部分” 的元素（默认删末尾），后续元素自动前移
    ```lua
    -- t（目标表）、pos（删除位置，可选，默认 #t）
    table.remove(t, [pos])
    ```
- 排序
    对 table “数组部分” 排序（原地排序，改变原表）
    ```lua
    -- t（目标表）、comp（比较函数，可选，默认升序）
    table.sort(t, [comp])
    ```
    这里举个降序的例子
    ```lua
    table.sort(t, function(a,b)
        if a > b then
            return true
        end
    end)
    ```
- 拼接
    将 table “数组部分” 的元素拼接成字符串（仅支持字符串 / 数字类型元素）
    ```lua
    -- t（目标表）、sep（分隔符，可选，默认空串）、i（起始索引，可选，默认 1）、j（结束索引，可选，默认 #t）
    table.concat(t, [sep], [i], [j])
    ```
    举个例子
    ```lua
    tb = {"123", "456", "789", "10101"}
    str = table.concat(tb, ",")
    print(str) -- 输出 123,456,789,10101
    ```
***
### 1.8.7 补充
在实际运行以下代码时, 不同的版本会出现不同的输出结果
```lua
a = {1,2,nil,4,"1231",true,nil}

print(#a) 
-- LuaForWindows_v5.1.5-52输出的结果是2
-- 而Lua v5.4.2(我用的)的运行结果是6
```

**省流: 版本原因**

但是我依旧推荐看一下以下内容, 我想让你知道的是标准序列和空洞的概念以及#运算符的原理

[**官方文档3.4.7原话**](https://www.lua.org/manual/5.4/manual.html#3.4.7)
When is a sequence, returns its only border, which corresponds to the intuitive notion of the length of the sequence. When is not a sequence, can return any of its borders. (The exact one depends on details of the internal representation of the table, which in turn can depend on how the table was populated and the memory addresses of its non-numeric keys.) t#tt#t

**以下是对官方文档的直译:**

长度运算符由----元前缀运算符 #表示。

字符串的长度是其字节数。（当每个字符为一个字节时，这就是字符串长度的通常含义。）

对表应用长度运算符会返回该表中的一个边界。表中的边界是满足以下条件的任意非负整数：（边界 ==0 或 t [边界]~=nil）并且（t [边界 + 1]==nil 或 边界 ==math.maxinteger）

通俗地说，边界是表中存在的任意正整数索引，且该索引后跟着一个不存在的索引，再加上两种极限情况：当索引 1 不存在时，边界为 0；当最大整数索引存在时，边界为该最大整数。请注意，非正整数的键不会影响边界。

恰好有一个边界的表称为序列。例如，表 {10,20,30,40,50} 是一个序列，因为它只有一个边界（5）。表 {10,20,30,nil,50} 有两个边界（3 和 5），因此它不是一个序列。（索引 4 处的 nil 被称为空洞。）表 {nil,20,30,nil,60,nil,60,nil} 有三个边界（0、3 和 6），所以它也不是一个序列。表 {} 是一个边界为 0 的序列。

当 t 是序列时，#t 返回其唯一的边界，这与序列长度的直观概念相对应。当 t 不是序列时，#t 可以返回其任意一个边界。（具体返回哪一个取决于表的内部表示细节，而这又可能取决于表的填充方式及其非数字键的内存地址。）

**总结**

Lua 不同版本对 # 运算符的核心算法逻辑没有本质差异,但是其内部实现细节可能因版本、虚拟机实现不同而有差异, 差异仅存在于「非序列表返回哪个边界」的具体选择上。

- 若表是标准序列（无空洞、整数索引 1~n 连续），# 的行为在所有 Lua 版本中完全一致，返回序列长度；
- 若表是非序列（有空洞、混合非整数键），# 的返回值可能因版本 / 虚拟机不同而变化，这不是「算法不同」，而是「规范允许的实现差异」；

所以官方文档也明确提醒：不要依赖非序列表的 # 结果，若需精确控制长度，应自己维护长度变量，或使用 ipairs（仅遍历序列部分）、pairs（遍历所有键）手动统计。

***
# 二. 多Lua脚本执行

## 2.1 全局变量和本地变量
- 全局变量
    ```lua
    -- 按照C++或者C#来说, temp是局部变量, 是取不到的
    for i = 1,2 do
        temp = "123"
    end
    print(temp) -- 123
    ```
    这里附一个C++的
    ```cpp
    int main()
    {
        for (int i = 0; i < 2; ++i)
            int temp = i;

        std::cout << temp; // 这里会直接报错
    }
    ```
- 局部变量
    本地（局部）变量的关键字 local
    ```lua
    for i = 1,2 do
        local temp = "123"
    end
    print(temp) -- nil
    ```
***
## 2.2 多脚本执行
- 关键字: `require`
  - 第一个脚本 `01.lua`
    ```lua
    print("落叶捎来讯息")
    require('02')
    print(a)
    -- 输出 落叶捎来讯息
    --      那是群星的时代
    --      123
    ```
  - 第二个脚本 `02.lua`
    ```lua
    a = 123
    print("那是群星的时代")
    ```
- 如果是require加载执行的脚本 加载一次过后不会再被执行
  - 将第一个脚本改为
    ```lua
    print("落叶捎来讯息")

    require('02')
    require('02')
    -- 输出依然和上面一样
    ```

注意:
- 示例的两个脚本在同一文件夹中

**脚本卸载**
- `package.loaded["脚本名"]`
  返回值是boolean 意为 该脚本是否被执行
  - 卸载已经执行过的脚本
    ```lua
    package.loaded["脚本名"] = nil
    ```
***
## 2.3 _G表
_G表是一个总表(table) 他将我们声明的所有全局的变量都存储在其中
- 所有声明的全局变量 都以键值对的形式存在其中
- 局部变量不会存到_G表中
    ```lua
    _G["a"] = 1
    print(a) -- 输出 1
    print(_G.a) -- 输出 1
    print(_G["a"]) -- 输出 1
    ```


- require 执行一个脚本时  可以在脚本最后返回一个外部希望获取的内容
    - 第一个脚本 `01.lua`
    ```lua
    local b = require('02')
    print(b)--输出 123
    ```
  - 第二个脚本 `02.lua`
    ```lua
    local a = 123
    return a
    ```
- 以上示例引出另一个知识, 可以通过`require`绕过_G表去传输局部变量
***

# 三. 特殊用法
## 3.1 多变量赋值
```lua
a,b,c = 1,2,"123"

--多变量赋值 如果后面的值不够 会自动补空
a,b,c = 1,2 
print(c) --nil

--多变量赋值 如果后面的值多了 会自动省略
a,b,c = 1,2,3,4,5,6
```
## 3.2 多返回值
```lua
function Test()
	return 10,20,30,40
end

--多返回值时 你用几个变量接 就有几个值
--如果少了 就少接几个 如果多了 就自动补空
a,b,c,d,e = Test()
print(e)--nil
```

## 3.3 and与or

- `and` 与 `or` 他们不仅可以连接 `boolean` 值 甚至任何东西都可以用来连接
- **在lua中 只有 nil 和 false 才认为是假**
</br>

- "短路"——对于and来说  有假则假  对于or来说 有真则真
- 所以 他们只需要判断 第一个 是否满足 就会停止计算了

```lua
print( 1 and 2 ) -- 2
print( 0 and 1) -- 1
print( nil and 1) -- nil
print( false and 2) -- false
print( true and 3) -- 3

print( true or 1 ) -- true
print( false or 1) -- 1
print( nil or 2) -- 2
```

- lua不支持三目运算符, 所以只能像下面这样写
```lua
x = 1
y = 2
res = (x>y) and x or y
print(res) -- 2
```
***
# 四. 协同程序
## 4.1 协程的创建
- 常用方式 : `coroutine.create()`
    ```lua
    fun = function()
        print(123)
    end

    co = coroutine.create(fun)
    print(co) -- thread: 0000000000f9e5c8(这是地址)
    print(type(co)) -- thread
    ```

- `coroutine.wrap()`
    ```lua
    co2 = coroutine.wrap(fun)
    print(co2) -- function: 0000000000fa94f0(这是地址)
    print(type(co2)) -- function
    ```
注意: 
- 协程的本质是一个线程对象

***
## 4.2 协程的运行
- `coroutine.resume()`
    这种方法对应的的`coroutine.create()`
    resume /rɪˈzuːm/ v. 恢复
    ```lua
    coroutine.resume(co) -- 输出 123
    ```
- `协程名()`
    这种方法对应的`coroutine.wrap()`
    ```lua
    co2() -- 输出 123
    ```
既然第二种方式创建的协程是一个函数, 那`协程名()`这样调用也就说的过去了

***
## 4.3 协程的挂起
- `coroutine.yield()`
解释一下下面的代码
  - 先创建一个协程, 然后用`resume`调用它
  - 当它执行到`coroutine.yield(i)`会被挂起
  - 因为lua执行顺序是从上往下依次执行代码语句
  - 所以又需要`resume`使其恢复运行, 然后又进入循环被挂起
    ```lua
    fun2 = function( )
        local i = 1
        while true do
            print(i)
            i = i + 1
            --协程的挂起函数
            coroutine.yield(i)
        end
    end

    co3 = coroutine.create(fun2)
    coroutine.resume(co3) -- 1
    coroutine.resume(co3) -- 2
    ```
- 同理
    ```lua
    co4 = coroutine.wrap(fun2)
    co4()
    co4()
    ```
### 4.3.1 返回值
以下代码建立在上面的示例基础上
- `coroutine.create`
   其有两个返回值
   - 一为boolean: 是否恢复成功
   - 二为函数返回值
    ```lua
    co3 = coroutine.create(fun2)

    isOK, tempI = coroutine.resume(co3)
    print(isOk,tempI) -- 输出 true	2
    ```
- `coroutine.wrap()`
    其只有一个返回值 : 函数返回值
    ```lua
    co4 = coroutine.wrap(fun2)
    print("返回值"..co4()) -- 输出 返回值2
    ```
***
## 4.4 协程的状态
- `coroutine.status(协程对象)`
  - dead 结束
  - suspended 暂停
  - running 进行中
- `co4` 无法用 `coroutine.status()` 查看状态, 因为它返回的是一个`function`
</br>

- `coroutine.running()`
这个函数可以得到当前正在 运行的协程的线程号

***
# 五. 元表
任何表变量都可以作为另一个表变量的元表
任何表变量都可以有自己的元表（可以看作父类）
当我们子表中进行一些特定操作时
会执行元表中的内容
## 5.1 设置元表
`setmetatable`
- 第一个参数 子表
- 第二个参数 元表
```lua
meta = {}
myTable = {}
setmetatable(myTable, meta)
```
***
## 5.2 特定操作
### 5.2.1 __tostring
当子表要被当做字符串使用时 会默认调用这个元表中的tostring方法
```lua
meta = {
	__tostring = function(t)
		return t.name
	end
}

myTable = {
	name = "扶离"
}

setmetatable(myTable, meta)

print(myTable) -- 扶离
```
### 5.2.2 __call
当子表被当做一个函数来使用时 会默认调用这个__call中的内容
当希望传参数时 一定要记住 默认第一个参数 是调用者自己
```lua
meta = {
    __call = function(a, b)
		print(a)
		print(b)
		print("No Surprises")
	end
}

myTable = {
	name = "Radiohead"
}

setmetatable(myTable, meta)

--把子表当做函数使用 就会调用元表的 __call方法
myTable(1)

-- 输出:
-- table: 00000000010294f0
-- 1
-- No Surprises
```
### 5.2.3 运算符重载

lua没有`大于`和`大于等于`的元方法
只能通过`小于`和`小于等于`来取反

|运算符|元方法|说明|
|:---:|:---:|:---:|
|+|__add|加|
|-|__sub|减|
|*|__mul|乘|
|/|__div|除|
|%|__mod|余|
|^|__pow|幂|
|==|__eq|等|
|<|__lt|小于|
|<=|__le|小于等于|
|..|__concat|拼接|

举个例子
```lua
meta = {
	--运算符+
	__add = function(t1, t2)
		return t1.age + t2.age
	end,
	--运算符==
	__eq = function(t1, t2)
		return true
	end
}

myTable1 = {age = 1}
setmetatable(myTable1, meta)
myTable2 = {age = 2}

print(myTable1 + myTable2) -- 3
print(myTable1 == myTable2) -- true
```

**追加**:
对于以下lua代码
```lua
meta = {
  __eq = function(t1, t2)
     return true
  end
}
myTable1 = {age = 1}
myTable2 = {age = 2}
setmetatable(myTable1, meta)
print(myTable2 == myTable1)
```

Lua 5.1：**只有当两个表都有相同的元表**时，才会调用 __eq 元方法, 所以会返回false
Lua 5.2+：**只要任意一个表**有 __eq 元方法，就会调用, 所以会返回true

这个版本差异对小于和小于等于的元方法也同样适用

## 5.2.4 __index和__newIndex

**`__index`**:
当子表中 找不到某一个属性时 
会到元表中 __index指定的表去找属性
```lua
meta = {
    name = "扶离",
    --只有在__index里的属性才会被子表获取到
    __index = {age = 2}
}

myTable = {}
setmetatable(myTable, meta)

print(myTable.age) -- 2
print(myTable.name) -- nil
```

- **__index的坑**:
    ```lua
    meta = {
        age = 2,
        __index = meta
    }

    myTable = {}
    setmetatable(myTable, meta)

    print(myTable.age) -- 输出 nil
    ```
    但是,你把__index声明到外部
    ```lua
    meta = {
        age = 2
    }
    meta.__index = meta

    myTable = {}
    setmetatable(myTable, meta)

    print(myTable.age) -- 输出 2
    ```

    所以, 建议把__index的初始化都声明在表外部
</br>

- **元表嵌套**
    ```lua
    metaFather = {
        age = 2
    }
    metaFather.__index = metaFather

    meta = {}
    meta.__index = meta
    setmetatable(meta, metaFather)

    myTable = {}
    setmetatable(myTable, meta)

    print(myTable.age) -- 2
    ```

**`__newIndex`**: 
当赋值时，如果赋值一个不存在的索引
那么会把这个值赋值到newIndex所指的表中 不会修改自己
```lua
meta = {}
myTable = {}

meta.__newindex = {}
setmetatable(myTable, meta)

--这个__newindex将属性赋值重定向了, 强制变量赋值为__newindex的值
--所以这里的赋值不会成功
myTable.age = 2

print(myTable.age) -- 输出nil
```
***
## 5.3 其他
- getmetatable
  获取元表
    ```lua
    meta = {}
    myTable = {}
    setmetatable(myTable, meta)

    print(getmetatable(myTable)) -- table: 0000000000fa9630(这里指的就是meta表)
    ```

- rawget
  在搜寻属性时, 只会寻找自身表中的属性, 相当于禁用了__index
    ```lua
    meta = { age = 2 }
    meta.__index = meta

    myTable = {}
    setmetatable(myTable, meta)

    print(rawget(myTable,"age")) -- nil
    ```

- rawset
  忽略__newindex的设置, 只会修改自身的变量
    ```lua
    meta = {}
    myTable = {}

    meta.__newindex = {}
    setmetatable(myTable, meta)
    rawset(myTable, "age" , 2)
    print(myTable.age) -- 2
    ```
***
# 六. 面向对象

## 6.1 封装
封装就是把数据和操作数据的方法 “打包” 在一起，隐藏内部实现细节，只对外暴露有限接口。

类的实现就是封装的表现
lua中 类 都是基于 table来实现
```lua
Object = {}
Object.id = 1

function Object:new()
	local obj = {}
	self.__index = self
	setmetatable(obj,self)
	return obj
end

local myObj = Object:new()

print(myObj.id) -- 输出1
```
再举一个例子, 想想最后一句代码会输出什么?
```lua
Object = {}
Object.id = 1

function Object:Test()
	print(self.id)
end

function Object:new()
	local obj = {}
	self.__index = self
	setmetatable(obj,self)
	return obj
end

local myObj = Object:new()

myObj.id = 2
myObj:Test()
```
来捋一遍, 当调用 Object:new() 声明出一个 myObj 对象时
- 在new方法的内部会创建一个新表obj, 这个表将 Object 设置为元表, 然后将返回值赋值给 myObj 
- 那此时其实就是独立于 Object 的另一个表了, 那在执行 myObj\.id = 2 时, 其实就和 Object\.id 没有关系了
- 所以输出结果为 2

***
## 6.2 继承
```lua
Object = {}
Object.id = 1

function Object:new()
	local obj = {}
	self.__index = self
	setmetatable(obj,self)
	return obj
end

function Object:subClass(className)
	_G[className] = {}
	local obj = _G[className]
	self.__index = self
    --子类 定义一个base属性 代表父类
	obj.base = self
	setmetatable(obj, self)
	return obj
end

Object:subClass("Person")

local p = Person:new()
print(p.id)
```
- 执行 Person:new()
    - Person:new() 等价于 Person.new(Person)，所以 self = Person
    - Person 里没有 new 方法，通过元表链查找：
    - Person 的元表是 Object（在 subClass 中设置）
    - Object.__index = Object（在 subClass 中设置）
    - 所以在 Object 中找到 new 方法
</br>

- 执行 Object:new()
    - 此时 self = Person（从调用传递过来）
    - 关键操作：
    ```lua
    self.__index = self      -- Person.__index = Person（覆盖了之前的 Object）
    setmetatable(obj, self)  -- obj 的元表 = Person
    ```
    - 返回的 obj 现在是一个以 Person 为元表的空表
</br>

- 查找 p\.id
    - p 本身没有 id 属性
    - 查找 p 的元表, 找到它的元表是 Person
    - 查找 Person.__index：Person（在 new 中设置的）
    - Person 本身没有 id 属性
    - 继续查找 Person 的元表：Object（在 subClass 中设置的）
    - 查找 Object.__index：Object（在 subClass 中设置的）
    - 在 Object 中找到 id = 1

***
## 6.3 多态
相同行为 不同表象 就是多态
相同方法 不同执行逻辑 就是多态

回看上面继承的实例代码, 我一直没有提其中的一句
```lua
--子类 定义一个base属性 代表父类
obj.base = self
```
现在就派上用场了

```lua
Object:subClass("GameObject")
GameObject.posX = 0;
GameObject.posY = 0;
function GameObject:Move()
	self.posX = self.posX + 1
	self.posY = self.posY + 1
	print(self.posX)
	print(self.posY)
end
```
我再让 Player 去继承 GameObject
```lua
GameObject:subClass("Player")
function Player:Move()

end
```
那么最基本的多态就实现了: 子类可以对父类中的相同方法进行重写
但是在C#中的相同逻辑中, 是可以用 base.Move() 来调用父类的 Move 方法的

为了实现这个功能, 可以把子类改为
```lua
function Player:Move()
    self.base:Move()
end

local p1 = Player:new()
local p2 = Player:new()
```
这样就完成了--------吗?

认真分析上面的`self.base:Move()`这句代码
- self.base : 指的是GameObject
- self.base:Move() : 等价于 GameObject:Move() , 那这里传进去的 self 是 GameObject, 这还面向对象吗? 
- 也就是说上面代码中的 p1 和 p2 的move方法都是调用的 GameObject:Move() , 它们共享的是一个数据
- 所以问题还是出在传入参数的self, 解决思路就是要让 self 分别指向自己

所以应该改成
```lua
function Player:Move()
    self.base.Move(self)
end
```
***
# 七. 深拷贝

## 7.1 lua中的深拷贝
在Lua中，使用赋值运算符"="进行拷贝的时候，分两种情况：

1. string、number、boolean这些基本类型，会进行复制，会创建一个新对象，拷贝出来的对象和原来的互不影响
    ```lua
    local num1 = 123
    local num2 = num1
    num2 = 456

    print(num1) -- 123
    print(num2) -- 456
    ```
2. table类型，是直接进行的引用，拷贝出来的对象和原来是一个对象，改一处另一处也会变化
    ```lua
    local tb1 = {x = 1,y = 2,z = 3}
    local tb2 = tb1
    tb2.x = 4
    print(tb1.x) -- 4
    print(tb2.x) -- 4
    ```

因此，一般我们提到Lua中的深拷贝，一般都是希望对table类型的变量实现深拷贝。即拷贝后的内容变化，不会影响原来的内容。

而Lua中并没有提供这样的api，因此我们一般会自己封装一个函数。

## 7.2 如何进行深拷贝？
进行table深拷贝整体的封装思路就是递归地遍历表的每一个元素，并且在遇到子表时，对子表也进行深拷贝。这样可以确保拷贝后的新表与原表完全独立，任何对新表的修改都不会影响到原表。

```lua
function clone(object)
    -- 记录已经复制过的表，防止循环引用
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        -- 如果已经复制过该表，则直接返回存储的表
        elseif lookup_table[object] then
            return lookup_table[object]
        end

        local new_table = {}
        -- 牢记table引用是浅拷贝
        -- 所以new_table在这里改变了也就意味着lookup_table[object]也改变了
        -- 所以lookup_table[object]的内容可以保留到后续的递归调用中
        lookup_table[object] = new_table
        for key,value in pairs(object) do
            new_table[_copy(key)] = _copy(value)
        end
        return setmetatable(new_table, getmetatable(object))
    end
    return _copy(object)
end
```
每次递归调用_copy函数时，都会对原表中的键和值进行深拷贝，并将结果插入到新表"new_table"中。这样就确保了新表和原表之间完全独立

***
# 八. 自带库

## 8.1 时间
```lua
-- 系统时间
print(os.time())

-- 自己传入参数 得到时间
print(os.time({year = 1931, month = 9, day = 18})) -- 100800

--获取当前时间具体信息
local nowTime = os.date("*t")
for k,v in pairs(nowTime) do
	print(k,v)
end
```
***
## 8.2 数学运算
`math`
- 绝对值
    ```lua
    math.abs()
    ```
- 弧度转角度
    ```lua
    math.deg(math.pi)
    ```
- 三角函数 传参为弧度
    ```lua
    math.cos(math.pi)
    ```
- 向下向上取整
    ```lua
    print(math.floor(2.6)) -- 2
    print(math.ceil(5.2)) -- 6
    ```
- 最大最小值
    ```lua
    print(math.max(1,2)) -- 2
    print(math.min(4,5)) -- 4
    ```
- 小数分离 分成整数部分和小数部分
    ```lua
    print(math.modf(1.2)) -- 1	0.2
    ```
- 幂运算
    ```lua
    print(math.pow(2, 5)) -- 32.0
    ```
- 随机数
    ```lua
    --先设置随机数种子
    math.randomseed(os.time())
    print(math.random(100))
    ```
- 开方
    ```lua
    print(math.sqrt(4)) -- 2.0
    ```
***
## 8.3 路径
```lua
--lua脚本加载路径
package.path
```
***
# 九. GC
关键字: `collectgarbage`
```lua
-- 获取当前lua占用内存数 以KB为单位 用返回值*1024 就可以得到具体的内存占用字节数
collectgarbage("count")

-- 进行垃圾回收
collectgarbage("collect")
```
