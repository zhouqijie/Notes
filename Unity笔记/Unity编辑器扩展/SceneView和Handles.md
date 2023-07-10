# SceneView和Handles    


## OnSceneGUI    

CRE：OnSceneGUI用来在"场景"视图中处理编辑器事件。    


- 事件：  

```C#  
Event.current.type == EventType.MouseDown  //鼠标按下事件  
Event.current.type == EventType.MouseDrag  //鼠标拖动事件  
Event.current.type == EventType.MouseUp  //鼠标松开事件  
Event.current.type == EventType.MouseMove  //鼠标移动事件
Event.current.type == EventType.Repaint  //重绘事件  

```  

- 按键：  

```C#  
Event.current.button
```

- 设置脏标记：  

```C#  
EditorUtility.SetDirty();    
```  

- 标记撤销：  

```C#
Undo.RecordObject(target, "...");
```


## Handles    

CRE：Handles用于在"场景"视图中绘制的各种可交互元素。    

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



(END)  