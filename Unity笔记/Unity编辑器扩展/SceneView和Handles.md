# SceneView

> "Scene"窗口对象。    


## GameObject可视和可选设置  

```C#  
SceneVisibilityManager.instance.EnablePicking();
SceneVisibilityManager.instance.Show();
```

## SceneView的常驻UI绘制      

```C#  
public class PersistantSceneUI 
{
    [InitializeOnLoadMethod]
    static void InitializeOnLoadMethod()
    {
        SceneView.onSceneGUIDelegate = delegate (SceneView sceneView)
        {
            Handles.BeginGUI();

            GUI.Label(new Rect(0f, 0f, 50f, 15f), "标题");
            GUI.Button(new Rect(0f, 20f, 50f, 50f),
                AssetDatabase.LoadAssetAtPath<Texture>("Assets/unity.png"));


            Handles.EndGUI();
        };   
    }
}
```  


## 获取当前事件    

```C#  
Event.current.type == EventType.MouseDown  //鼠标按下事件  
Event.current.type == EventType.MouseDrag  //鼠标拖动事件  
Event.current.type == EventType.MouseUp  //鼠标松开事件  
Event.current.type == EventType.MouseMove  //鼠标移动事件
Event.current.type == EventType.Repaint  //重绘事件  

```  


## OnSceneGUI方法    

CRE：OnSceneGUI用来在"场景"视图中处理编辑器事件。    

- 获取按键输入：  

```C#  
e.alt;
Event.current.button;
```

- 设置脏标记：  

```C#  
EditorUtility.SetDirty();    
```  

- 标记撤销：  

```C#
Undo.RecordObject(target, "...");
```

## 场景相机  

`SceneView.lastActiveSceneView.camera`  


<br />
<br />
<br />
<br />



# Handles    

CRE：Handles用于在"场景"视图中绘制的各种可交互元素。    

## 常用的方法调用：  

```C#  
Handles.DrawLine();
Handles.DrawWireDisc();
Handles.DrawSolidDisc();  
Handles.ArrowHandleCap(0, currentMouseHit + Vector3.up, Quaternion.LookRotation(-Vector3.up), 0.85f, EventType.Repaint);
Handles.Slider(position, direction, 1f, Handles.ArrowHandleCap, 1);
```  
除了Handles，也可以用Graphics绘制：  
```C#  
Graphics.DrawMesh(Resources.GetBuiltinResource<Mesh>("Cube.fbx"), matrix, mat, 0);
```  


## 射线检测  

`Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);`  



# 参考文章    

> https://zhuanlan.zhihu.com/p/548571466?utm_id=0  

(END)  