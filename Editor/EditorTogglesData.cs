using UnityEngine;

namespace Kogane
{
    public sealed class EditorTogglesData
    {
        public GUIContent Content { get; set; }
        public bool       Value   { get; set; }

        public EditorTogglesData
        (
            GUIContent content,
            bool       value
        )
        {
            Content = content;
            Value   = value;
        }
    }
}