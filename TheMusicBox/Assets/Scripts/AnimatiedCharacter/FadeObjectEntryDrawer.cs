//using UnityEditor;
//using UnityEngine;

//[CustomPropertyDrawer(typeof(FadeTransitionManager.FadeObjectEntry))]
//public class FadeObjectEntryDrawer : PropertyDrawer
//{
//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        var fadeModeProp = property.FindPropertyRelative("fadeMode");
//        if (fadeModeProp.objectReferenceValue == null)
//            return EditorGUIUtility.singleLineHeight * 5;

//        var mode = fadeModeProp.objectReferenceValue.name;

//        int lines = 4; // target, fadeMode, executor, duration(s)

//        if (mode.Contains("FadeInOnly")) lines += 1;
//        else if (mode.Contains("FadeOutOnly")) lines += 1;
//        else lines += 2;

//        return EditorGUIUtility.singleLineHeight * lines;
//    }

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);
//        float lineHeight = EditorGUIUtility.singleLineHeight;
//        float spacing = 2f;
//        Rect lineRect = new(position.x, position.y, position.width, lineHeight);

//        var targetProp = property.FindPropertyRelative("target");
//        var fadeInDurationProp = property.FindPropertyRelative("fadeInDuration");
//        var fadeOutDurationProp = property.FindPropertyRelative("fadeOutDuration");
//        var fadeModeProp = property.FindPropertyRelative("fadeMode");
//        var executorProp = property.FindPropertyRelative("executor");

//        EditorGUI.PropertyField(lineRect, targetProp);
//        lineRect.y += lineHeight + spacing;

//        EditorGUI.PropertyField(lineRect, fadeModeProp);
//        lineRect.y += lineHeight + spacing;

//        EditorGUI.PropertyField(lineRect, executorProp);
//        lineRect.y += lineHeight + spacing;

//        if (fadeModeProp.objectReferenceValue != null)
//        {
//            string modeName = fadeModeProp.objectReferenceValue.name;

//            if (modeName.Contains("FadeInOnly"))
//            {
//                EditorGUI.PropertyField(lineRect, fadeInDurationProp);
//                lineRect.y += lineHeight + spacing;
//            }
//            else if (modeName.Contains("FadeOutOnly"))
//            {
//                EditorGUI.PropertyField(lineRect, fadeOutDurationProp);
//                lineRect.y += lineHeight + spacing;
//            }
//            else
//            {
//                EditorGUI.PropertyField(lineRect, fadeInDurationProp);
//                lineRect.y += lineHeight + spacing;

//                EditorGUI.PropertyField(lineRect, fadeOutDurationProp);
//                lineRect.y += lineHeight + spacing;
//            }
//        }

//        EditorGUI.EndProperty();
//    }
//}