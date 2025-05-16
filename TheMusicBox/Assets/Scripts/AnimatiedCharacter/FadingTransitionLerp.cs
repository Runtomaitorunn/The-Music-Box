using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadingTransitionLerp : MonoBehaviour
{
    [Header("Transition Poses")]
    [Tooltip("Only GameObjects can be added to this list!")]
    [SerializeField] private List<GameObject> transitionPosesList = new List<GameObject>();

    [Header("Fade Settings")]
    [Tooltip("Fade-in durations for each transition object")]
    public List<float> fadeInDurations = new List<float>();

    [Tooltip("Fade-out durations for each transition object")]
    public List<float> fadeOutDurations = new List<float>();

    [Tooltip("Default fade duration (used if no specific duration provided)")]
    public float defaultFadeDuration = 1f;

    public List<Material> materialsToFadeOut = new List<Material>();
    public List<Material> materialsToFadeIn = new List<Material>();

    [SerializeField] private int fadeOutIndex = 0;
    [SerializeField] private int fadeInIndex = 0;


    private void Start()
    {


    }
    /// <summary>
    ///  Fades out materials from the current fadeOutIndex object.
    /// </summary>
    public void FadeOut()
    {
        materialsToFadeOut.Clear();

        if (fadeOutIndex < 0 || fadeOutIndex >= transitionPosesList.Count)
        {
            Debug.LogWarning("FadeOut index out of range.");
            return;
        }

        GameObject obj = transitionPosesList[fadeOutIndex];
        float duration = GetFadeOutDuration(fadeOutIndex);
        CollectMaterialsWithTransparency(obj, materialsToFadeOut);

        foreach (Material mat in materialsToFadeOut)
        {
            StartCoroutine(FadeMaterialTransparency(mat, 0f, 1f, duration));
        }

        fadeOutIndex++;
    }

    /// <summary>
    /// Fades in materials from the current fadeInIndex object.
    /// </summary>
    public void FadeIn()
    {
        materialsToFadeIn.Clear();

        if (fadeInIndex < 0 || fadeInIndex >= transitionPosesList.Count)
        {
            Debug.LogWarning("FadeIn index out of range.");
            return;
        }

        GameObject obj = transitionPosesList[fadeInIndex];
        float duration = GetFadeInDuration(fadeOutIndex);
        CollectMaterialsWithTransparency(obj, materialsToFadeIn);

        foreach (Material mat in materialsToFadeIn)
        {
            StartCoroutine(FadeMaterialTransparency(mat, 1f, 0f, duration));
        }

        fadeInIndex++;

    }

    /// <summary>
    /// Collect all materials with "_Transparent_Value" from an object.
    /// </summary>
    private void CollectMaterialsWithTransparency(GameObject obj, List<Material> targetList)
    {
        if (obj == null) return;

        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            Debug.LogWarning($"No MeshRenderer on: {obj.name}");
            return;
        }

        foreach (Material mat in renderer.materials)
        {
            if (mat.HasProperty("_Transparent_Value"))
            {
                targetList.Add(mat);
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

    /// <summary>
    /// Safely get fade-in duration for given index.
    /// </summary>
    private float GetFadeInDuration(int index)
    {
        if (index >= 0 && index < fadeInDurations.Count)
        {
            return fadeInDurations[index];
        }

        return defaultFadeDuration;
    }

    /// <summary>
    /// Safely get fade-out duration for given index.
    /// </summary>
    private float GetFadeOutDuration(int index)
    {
        if (index >= 0 && index < fadeOutDurations.Count)
        {
            return fadeOutDurations[index];
        }

        return defaultFadeDuration;
    }
}

