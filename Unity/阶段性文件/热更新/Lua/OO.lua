--面向对象
--只需要注意在子类重写父类方法时, 如果想保留父类实现要用
-- self.base.方法名(self)

--所有类的基类: Object

Object = {}

function Object:new()
	local obj = {}
	self.__index = self
	setmetatable(obj, self)
	return obj
end

function Object:subClass(className)
	_G[className] = {}
	local obj = _G[className]
	self.__index = self
	obj.base = self
	setmetatable(obj,self)
	return obj
end

