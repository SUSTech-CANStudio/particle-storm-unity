﻿using CANStudio.BulletStorm.Util.EditorAttributes;
using UnityEditor;
using UnityEngine;

namespace CANStudio.BulletStorm.Editor.AttributeDrawers
{
    [CustomPropertyDrawer(typeof(CustomCurveAttribute))]
    internal class CustomCurveDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            if (attribute is CustomCurveAttribute custom)
                EditorGUI.CurveField(position, property, custom.color, custom.range);
        }
    }
}