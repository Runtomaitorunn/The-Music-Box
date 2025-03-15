using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static InteractableFeedbackManager;


public class InteractableFeedbackManager : MonoBehaviour
{
    [System.Serializable]
    public struct RendererData
    {
        public MeshRenderer renderer;
        public int materialIndex;
        public Color defaultColor;
    }
    [Header("Highlight Outline")]
    public List<RendererData> rendererDatas;
    public Color outlineHighlightColor;

    [Header("Play Loading Circular Anim")]
    public GameObject circularAnimCanvas;

    public void Start()
    {
        for (int i = 0; i < rendererDatas.Count; i++)
        {
            if (rendererDatas[i].renderer != null && rendererDatas[i].renderer.materials.Length > rendererDatas[i].materialIndex)
            {
                RendererData data = rendererDatas[i];
                data.defaultColor = data.renderer.materials[data.materialIndex].GetColor("_Mix_Color");
                rendererDatas[i] = data; // Update the list with the default color
            }
            else
            {
                Debug.LogError("Renderer is missing or material index is out of range for renderer at index " + i);
            }
        }
    }


    #region Highlight Outline
    public void HighlightOutlineColor()
    {
        foreach (var data in rendererDatas)
        {
            if (data.renderer != null && data.renderer.materials.Length > data.materialIndex)
            {
                Material targetMaterial = data.renderer.materials[data.materialIndex];
                targetMaterial.SetColor("_Mix_Color", outlineHighlightColor);
            }
            else
            {
                Debug.LogWarning("Renderer is missing or material index is out of range for renderer " + data.renderer.name);
            }
        }
    }

    public void RevertOutlineColor()
    {
        foreach (var data in rendererDatas)
        {
            if (data.renderer != null && data.renderer.materials.Length > data.materialIndex)
            {
                Material targetMaterial = data.renderer.materials[data.materialIndex];
                targetMaterial.SetColor("_Mix_Color", data.defaultColor);
            }
            else
            {
                Debug.LogWarning("Renderer is missing or material index is out of range for renderer " + data.renderer.name);
            }
        }
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
