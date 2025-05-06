using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadingTransitionLerp : MonoBehaviour
{
    [Header("Transition Poses")]
    [Tooltip("Only GameObjects can be added to this list!")]
    [SerializeField] private List<GameObject> transitionPosesList = new List<GameObject>();


    private void Start()
    {
        CheckMaterialTransparency();


    }
    /// <summary>
    /// Function controls fading out 
    /// </summary>
    public void FadeOut()
    {

    }

    /// <summary>
    /// Function controls fading in
    /// </summary>
    public void FadeIn()
    {

    }

    /// <summary>
    /// Check the material named 'BaseColor'
    /// </summary>
    public void CheckMaterialTransparency()
    {
        List<Material> allMaterials = new List<Material>();

        foreach (GameObject obj in transitionPosesList)
        {
            if (obj == null) continue;

            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                Debug.LogWarning($"No MeshRenderer on: {obj.name}");
                continue;
            }

            foreach (Material mat in meshRenderer.materials)
            {
                if (!allMaterials.Contains(mat)) // ±‹√‚÷ÿ∏¥
                {
                    allMaterials.Add(mat);
                }
            }
        }

        foreach (Material mat in allMaterials)
        {
            if (mat.HasProperty("_Transparent_Value"))
            {
                float value = mat.GetFloat("_Transparent_Value");
                Debug.Log($"Material '{mat.name}' has Transparent Value: {value}");
            }
            else
            {
                Debug.Log($"Material '{mat.name}' does not have Transparent Value");
            }
        }
    }
}

