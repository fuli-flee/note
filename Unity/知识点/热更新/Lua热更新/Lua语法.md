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