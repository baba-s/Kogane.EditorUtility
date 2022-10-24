# Kogane Editor Utility

TextMesh Pro のトグルの Editor GUI を使えるようにするクラス

## 使用例

```csharp
using System;
using UnityEngine;

[Flags]
public enum Attribute
{
    FIRE  = 1 << 1,
    AQUA  = 1 << 2,
    GRASS = 1 << 3,
}

public sealed class Example : MonoBehaviour
{
    [SerializeField] private Attribute m_attribute;
}
```

```csharp
using Kogane;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer( typeof( Attribute ) )]
public sealed class AttributeDrawer : PropertyDrawer
{
    public override void OnGUI
    (
        Rect               position,
        SerializedProperty property,
        GUIContent         label
    )
    {
        var intValue = property.intValue;

        var datas = new EditorTogglesData[]
        {
            new( new GUIContent( "くさ" ), ( intValue & ( int )Attribute.GRASS ) != 0 ),
            new( new GUIContent( "ほのお" ), ( intValue & ( int )Attribute.FIRE ) != 0 ),
            new( new GUIContent( "みず" ), ( intValue & ( int )Attribute.AQUA ) != 0 ),
        };

        KoganeEditorUtility.EditorToggles( position, label, datas );

        var newIntValue = 0;

        if ( datas[ 0 ].Value ) newIntValue |= ( int )Attribute.GRASS;
        if ( datas[ 1 ].Value ) newIntValue |= ( int )Attribute.FIRE;
        if ( datas[ 2 ].Value ) newIntValue |= ( int )Attribute.AQUA;

        if ( intValue == newIntValue ) return;

        property.intValue = newIntValue;
    }
}
```

![icon468](https://user-images.githubusercontent.com/6134875/197517903-a34bb305-518f-4448-bce6-364c2e95c648.gif)
