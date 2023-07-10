# 编辑器菜单MenuItem  

> CRE：MenuItem即编辑器菜单栏自定义，可以使用自定义快捷键。    


## 快捷键  

|符号|按下|
|-|-|
|`^`或`%`|Ctrl|
|`#`|Shift|
|`&`|Alt|


## 示例    

```C#  
[MenuItem("自定义/Test %A")]
public static void Test()
{
    //...
}
```    

(END)  