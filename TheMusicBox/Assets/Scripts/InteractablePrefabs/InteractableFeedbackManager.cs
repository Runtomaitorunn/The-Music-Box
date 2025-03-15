using UnityEditor;
using UnityEngine;


public class InteractableFeedbackManager : MonoBehaviour
{
    [Header("Highlight Outline")]
    public MeshRenderer outlineRenderer;
    public int materialIndex = 1;
    [SerializeField]
    private Color outlineDefaultColor;
    public Color outlineHighlightColor;

    [Header("Play Loading Circular Anim")]
    public GameObject circularAnimCanvas;

    public void Start()
    {
        if (outlineRenderer != null && outlineRenderer.materials.Length > materialIndex)
        {
            outlineDefaultColor = outlineRenderer.materials[materialIndex].GetColor("_Mix_Color");
        }
    }

    #region Highlight Outline
    public void HighlightOutlineColor()
    {
        if (outlineRenderer != null && outlineRenderer.materials.Length > materialIndex)
        {
            Material targetMaterial = outlineRenderer.materials[materialIndex];

            targetMaterial.SetColor("_Mix_Color", outlineHighlightColor);
        }
        else
        {
            Debug.LogWarning("MeshRenderer is missing or material index is out of range.");
        }
    }

    public void RevertOutlineColor()
    {
        Material targetMaterial = outlineRenderer.materials[materialIndex];

        targetMaterial.SetColor("_Mix_Color", outlineDefaultColor);
    }
    #endregion Highlight Outline

    #region Play Loading Circular Anim
    public void TestPrint()
    {
        Debug.Log("I'm gazed at");
    }

    public void ToggleCanvas(bool state)
    {
        circularAnimCanvas.SetActive(state);
    }
    #endregion Play Loading Circular Anim

}
