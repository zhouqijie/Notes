# 自定义Window    

## 示例    

```C#  
public class WindowTest : EditorWindow
{
    public static WindowTest window;

    [MenuItem("自定义/窗口/WindowTest")]
    public static void OpenWindow()
    {
        window = EditorWindow.GetWindow<WindowTest>(false, "WindowTest", true); //Instantiate
        window.Show(); //Show
    }

    public void OnGUI()
    {
        //...
    }
}
```  


(END)  