using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kogane
{
    public static class KoganeEditorUtility
    {
        private static GUIStyle m_alignmentButtonLeft;
        private static GUIStyle m_alignmentButtonMid;
        private static GUIStyle m_alignmentButtonRight;

        public static void EditorToggles
        (
            Rect                             position,
            GUIContent                       label,
            IReadOnlyList<EditorTogglesData> datas
        )
        {
            m_alignmentButtonLeft ??= new GUIStyle( EditorStyles.miniButtonLeft )
            {
                padding =
                {
                    left   = 4,
                    right  = 4,
                    top    = 2,
                    bottom = 2
                }
            };

            m_alignmentButtonMid ??= new GUIStyle( EditorStyles.miniButtonMid )
            {
                padding =
                {
                    left  = 4,
                    right = 4
                }
            };

            m_alignmentButtonRight ??= new GUIStyle( EditorStyles.miniButtonRight )
            {
                padding =
                {
                    left  = 4,
                    right = 4
                }
            };

            var buttonPosition = EditorGUI.PrefixLabel( position, label );

            var count = datas.Count;
            var width = buttonPosition.width / count;

            for ( var i = 0; i < count; i++ )
            {
                var rect = new Rect( buttonPosition )
                {
                    x     = buttonPosition.x + width * i,
                    width = width,
                };

                var data = datas[ i ];

                var style = i == 0
                        ? m_alignmentButtonLeft
                        : i != count - 1
                            ? m_alignmentButtonMid
                            : m_alignmentButtonRight
                    ;

                data.Value = EditorToggle
                (
                    position: rect,
                    value: data.Value,
                    content: data.Content,
                    style: style
                );
            }
        }

        public static bool EditorToggle
        (
            Rect       position,
            bool       value,
            GUIContent content,
            GUIStyle   style
        )
        {
            var id      = GUIUtility.GetControlID( content, FocusType.Keyboard, position );
            var current = Event.current;

            if ( current.type == EventType.MouseDown && position.Contains( current.mousePosition ) )
            {
                GUIUtility.keyboardControl        = id;
                EditorGUIUtility.editingTextField = false;
                HandleUtility.Repaint();
            }

            return GUI.Toggle
            (
                position: position,
                id: id,
                value: value,
                content: content,
                style: style
            );
        }
    }
}