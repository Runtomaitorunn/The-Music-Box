using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Content.Interaction;

public class ToggleComponentZoneWindow : EditorWindow
{
    private ToggleComponentZone targetSelector;
    private Behaviour[] behaviours;

    // Method to create the window
    public static void ShowWindow(ToggleComponentZone target)
    {
        ToggleComponentZoneWindow window = (ToggleComponentZoneWindow)GetWindow(typeof(ToggleComponentZoneWindow), true, "Select Behaviour");
        window.targetSelector = target;
        window.behaviours = target.componentToToggle.GetComponents<Behaviour>();
        window.Show();
    }

    void OnGUI()
    {
        if (behaviours != null)
        {
            foreach (Behaviour behaviour in behaviours)
            {
                if (GUILayout.Button(behaviour.GetType().Name))
                {
                    targetSelector.componentToToggle = behaviour; // Assign the selected behaviour
                    Debug.Log(behaviour.GetType().Name + " selected.");
                    Close(); // Close the window after selection
                }
            }
        }
    }
}