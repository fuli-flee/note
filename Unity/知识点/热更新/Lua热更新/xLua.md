[toc]
***
# 一. 什么是热更新
游戏或软件更新时, 无需重新下载客户端进行安装
而是在应用程序启动的情况下, 在内部进行的资源或者代码的更新

***

# 二. C#调用Lua

## 2.1 Lua解析器
- Lua解析器 能够让我们在Unity中执行Lua
一般情况下 保持它的唯一性
    ```CSharp
    LuaEnv env = new LuaEnv();
    ```
- 执行Lua语言
    ```CSharp
    env.DoString("print('Lua代码')"); //输出 LUA: Lua代码
    ```
- 执行一个Lua脚本 Lua知识点 ：多脚本执行 require
    默认寻找脚本的路径 是在 Resources下 并且 因为在这里
    大概率是通过 Resources.Load 去加载Lua脚本  所以只支持 txt bytes等等
    所以Lua脚本 后缀要加一个txt
    ```CSharp
    env.DoString("require('Main')");
    ```
    - 在resource下的lua文件名为 
        ```txt
        Main.lua.txt
        ```
- 帮助我们清楚Lua中我们没有手动释放的对象 垃圾回收
    帧更新中定时执行 或者 切场景时执行
    ```CSharp
    env.Tick();
    ```
- 销毁Lua解析器 
    ```CSharp
    env.Dispose();
    ```