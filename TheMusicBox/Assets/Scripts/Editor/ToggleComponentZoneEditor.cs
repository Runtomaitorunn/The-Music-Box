using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Content.Interaction;

[CustomEditor(typeof(ToggleComponentZone))]
public class ToggleComponentZoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draw the default inspector

        ToggleComponentZone script = (ToggleComponentZone)target;

        if (script.componentToToggle != null && GUILayout.Button("Select Behaviour"))
        {
            // Open the selection window
            ToggleComponentZoneWindow.ShowWindow(script);
        }
    }
}
