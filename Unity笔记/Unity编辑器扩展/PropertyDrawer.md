# PropertyDraw    

CRE：对于`public`或者`[SerializableField]`修饰的普通属性，Unity会自动绘制属性的编辑器。对于自定义类型的属性，需要实现属性编辑器绘制。自定义的属性编辑器，继承`UnityEditor.PropertyDrawer`。    


## EditorGUI.Field相关调用    

```C#  
UnityEditor.EditorGUILayout.IntField();
UnityEditor.EditorGUILayout.FloatField();
UnityEditor.EditorGUILayout.PropertyField();
```  


## 简单示例    

```C#  
//在重载的`OnGUI`中绘制相关属性：  
[UnityEditor.CustomPropertyDrawer(typeof(Bonus))]
public class CustomPropertyBonus : UnityEditor.PropertyDrawer
{
    //...
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.FindPropertyRelative("type").intValue = UnityEditor.EditorGUILayout.IntField("类型", property.FindPropertyRelative("type").intValue);
        property.FindPropertyRelative("amount").intValue = UnityEditor.EditorGUILayout.IntField("      数量", property.FindPropertyRelative("amount").intValue);

        //...
    }
}
```

## 数组属性    

获取数组元素：`serializedProperty.GetArrayElementAtIndex(i);`  
修改数组长度：`serializedProperty.arraySize{get; set;}`  

## SerializedObject和SerializedProperty      

> Unity Docs：  
> SerializedObject and SerializedProperty are classes for editing serialized fields on Object|Unity objects in a completely generic way. These classes automatically handle dirtying individual serialized fields so they will be processed by the Undo system and styled correctly for Prefab overrides when drawn in the Inspector.  

#### 成员：    

应用变更：`serializedObject.ApplyModifiedProperties();`    
......

#### Editor.serializedObject字段：  

> Unity Docs：  
> `Editor.serializedObject`：A SerializedObject representing the object or objects being inspected.  



（END）    