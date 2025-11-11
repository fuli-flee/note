--深拷贝
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
