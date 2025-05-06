using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadingTransitionLerp : MonoBehaviour
{
    [Header("Transition Poses")]
    [Tooltip("Only GameObjects can be added to this list!")]
    [SerializeField] private List<GameObject> transitionPosesList = new List<GameObject>();

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private List<Material> allMaterials = new List<Material>();


    private void Start()
    {
        CheckMaterialTransparency();


    }
    /// <summary>
    /// Function controls fading out 
    /// </summary>
    public void FadeOut()
    {
        foreach (Material mat in allMaterials)
        {
            if (mat.HasProperty("_Transparent_Value"))
            {
                StartCoroutine(FadeMaterialTransparency(mat, 0f, 1f, fadeDuration));
            }
        }
    }

    /// <summary>
    /// Function controls fading in
    /// </summary>
    public void FadeIn()
    {
        foreach (Material mat in allMaterials)
        {
            if (mat.HasProperty("_Transparent_Value"))
            {
                StartCoroutine(FadeMaterialTransparency(mat, 1f, 0f, fadeDuration));
            }
        }

    }

    /// <summary>
    /// Check the material named 'BaseColor'
    /// </summary>
    public void CheckMaterialTransparency()
    {

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

    /// <summary>
    /// Coroutine to lerp a material's transparency
    /// </summary>
    private IEnumerator FadeMaterialTransparency(Material mat, float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float value = Mathf.Lerp(from, to, elapsed / duration);
            mat.SetFloat("_Transparent_Value", value);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.SetFloat("_Transparent_Value", to); // Ensure final value is set
    }
}

