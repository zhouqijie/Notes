

# 一、组件自定义编辑：

> AAttribute:   `[UnityEditor.CustomPropertyDrawer(typeof(TestClass))]`或者`[UnityEditor.CustomEditor(typeof(TestClass))]`    
> A继承：UnityEditor.Editor

### 需重载函数：
- 重载OnInspectorGUI();

### GUI组件：  

标签：  
`GUI.Label();`  
CheckBox：  
`GUI.Toggle();`  
按钮：  
`GUI.Button();`  
字段：
`EditorGUI.LabelField()`  
`EditorGUI.IntField()`  
`EditorGUI.ObjectField();`    
边框：  
`EditorGUI.BeginVertical("frameBox")`  




### 其他：   

缩进：
`EditorGUI.indentLevel`



<br />
<br />
<br />
<br />



# 二、可序列化字段的自定义编辑：  

> Attribute:   `[UnityEditor.CustomPropertyDrawer(typeof(TestClass))`    
> 继承：`UnityEditor.PropertyDrawer`  

### 可重载函数：
- 重载`OnGUI(Rect position, SerializedProperty property, GUIContent label)` （ 使用EditorGUI.BeginProperty(position, label, property);以及EditorGUI.EndProperty();）
- 重载`GetPropertyHeight(SerializedProperty property, GUIContent label)`


### 对象的成员字段：  

`SerializedProperty intTestp = property.FindPropertyRelative("intTest");`    
`intTestp.intValue = 999;`    




<br />
<br />
<br />
<br />




# 编辑器下执行脚本ExecuteInEditMode

`[ExecuteInEditMode]`  

加在类名前面。只有在窗口变化的时候才会执行方法。    







