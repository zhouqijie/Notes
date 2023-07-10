# 自定义EditorTool   

### 示例    

```C#  
[EditorTool("CustomScaleTool")]
public class CustomScaleTool : EditorTool
{
    public override void OnToolGUI(EditorWindow window)
    {
        base.OnToolGUI(window);

        //(可调用Handles类成员绘制元素)      
        //....  
    }
}
```  


（END）  