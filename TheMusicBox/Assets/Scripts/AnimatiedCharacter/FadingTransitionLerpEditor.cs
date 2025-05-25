using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FadingTransitionLerp))]
public class FadingTransitionLerpEditor : Editor
{
    SerializedProperty posesListProp;

    private void OnEnable()
    {
        posesListProp = serializedObject.FindProperty("transitionPosesList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultFadeDuration"));

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Transition Poses List", EditorStyles.boldLabel);

        if (posesListProp != null && posesListProp.isArray)
        {
            for (int i = 0; i < posesListProp.arraySize; i++)
            {
                SerializedProperty poseProp = posesListProp.GetArrayElementAtIndex(i);
                SerializedProperty targetProp = poseProp.FindPropertyRelative("target");
                SerializedProperty fadeModeProp = poseProp.FindPropertyRelative("fadeMode");
                SerializedProperty fadeInDurationProp = poseProp.FindPropertyRelative("fadeInDuration");
                SerializedProperty fadeOutDurationProp = poseProp.FindPropertyRelative("fadeOutDuration");

                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.PropertyField(targetProp, new GUIContent("Target GameObject"));
                EditorGUILayout.PropertyField(fadeModeProp, new GUIContent("Fade Mode"));

                FadeMode mode = (FadeMode)fadeModeProp.enumValueIndex;

                // Show fields based on mode
                if (mode == FadeMode.FadeInOnly)
                {
                    EditorGUILayout.PropertyField(fadeInDurationProp, new GUIContent("Fade In Duration"));
                }
                else if (mode == FadeMode.FadeOutOnly)
                {
                    EditorGUILayout.PropertyField(fadeOutDurationProp, new GUIContent("Fade Out Duration"));
                }
                else if (mode == FadeMode.FadeInThenOut || mode == FadeMode.FadeOutThenIn)
                {
                    EditorGUILayout.PropertyField(fadeInDurationProp, new GUIContent("Fade In Duration"));
                    EditorGUILayout.PropertyField(fadeOutDurationProp, new GUIContent("Fade Out Duration"));
                }

                // Optional: Remove button for each pose
                if (GUILayout.Button("Remove Pose"))
                {
                    posesListProp.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
        }

        // Add button
        if (GUILayout.Button("Add New Pose"))
        {
            posesListProp.InsertArrayElementAtIndex(posesListProp.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }
}