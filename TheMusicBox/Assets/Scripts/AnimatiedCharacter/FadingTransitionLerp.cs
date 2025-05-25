using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class TransitionPose
{
    public GameObject target;

    [Tooltip("Transition mode for this pose")]
    public FadeMode fadeMode = FadeMode.FadeInOnly;

    [Tooltip("Fade-in duration for this pose")]
    public float fadeInDuration = 1f;

    [Tooltip("Fade-out duration for this pose")]
    public float fadeOutDuration = 1f;

    //[HideInInspector]
    public bool hasFadedIn = false;
   // [HideInInspector]
    public bool hasFadedOut = false;
}


public class FadingTransitionLerp : MonoBehaviour
{
    [Header("Transition Poses")]
    [SerializeField] private List<TransitionPose> transitionPosesList = new List<TransitionPose>();

    [Tooltip("Default fade duration (used if no specific duration provided)")]
    public float defaultFadeDuration = 1f;

    [SerializeField] private List<Material> materialsToFadeOut = new List<Material>();
    [SerializeField] private List<Material> materialsToFadeIn = new List<Material>();

    [SerializeField] private int currentIndex = 0;


    private void Start()
    {


    }

    /// <summary>
    /// Plays the next transition in the list.
    /// </summary>
    public void PlayCurrentTransition()
    {
        if (currentIndex < 0 || currentIndex >= transitionPosesList.Count)
        {
            Debug.LogWarning("Current index out of range.");
            return;
        }

        TransitionPose pose = transitionPosesList[currentIndex];
        StartCoroutine(PlayTransition(pose));
    }

    /// <summary>
    /// Plays the fade transition for a single pose.
    /// </summary>
    private IEnumerator PlayTransition(TransitionPose pose)
    {
        if (pose == null || pose.target == null)
        {
            Debug.LogWarning("Pose or target is null.");
            yield break;
        }

        float fadeInDuration = pose.fadeInDuration > 0 ? pose.fadeInDuration : defaultFadeDuration;
        float fadeOutDuration = pose.fadeOutDuration > 0 ? pose.fadeOutDuration : defaultFadeDuration;

        switch (pose.fadeMode)
        {
            case FadeMode.FadeInOnly:
                yield return Fade(pose.target, true, fadeInDuration);
                currentIndex++;
                break;

            case FadeMode.FadeOutOnly:
                yield return Fade(pose.target, false, fadeOutDuration);
                currentIndex++;
                break;

            case FadeMode.FadeInThenOut:
                if (!pose.hasFadedIn)
                {
                    yield return Fade(pose.target, true, fadeInDuration);
                    pose.hasFadedIn = true;
                }
                else if (!pose.hasFadedOut)
                {
                    yield return Fade(pose.target, false, fadeOutDuration);
                    pose.hasFadedOut = true;
                }
                else
                {
                    // 两步都完成，自动跳到下一个 pose
                    pose.hasFadedIn = false;
                    pose.hasFadedOut = false;
                    currentIndex++;
                }
                break;

            case FadeMode.FadeOutThenIn:
                if (!pose.hasFadedOut)
                {
                    yield return Fade(pose.target, false, fadeOutDuration);
                    pose.hasFadedOut = true;
                }
                else if (!pose.hasFadedIn)
                {
                    yield return Fade(pose.target, true, fadeInDuration);
                    pose.hasFadedIn = true;
                }
                else
                {
                    // 两步都完成，自动跳到下一个 pose
                    pose.hasFadedIn = false;
                    pose.hasFadedOut = false;
                    currentIndex++;
                }
                break;
        }
    }

    /// <summary>
    /// Fades in or out all transparent materials of the object.
    /// </summary>
    private IEnumerator Fade(GameObject obj, bool fadeIn, float duration)
    {
        List<Material> mats = new List<Material>();
        CollectMaterialsWithTransparency(obj, mats);

        float from = fadeIn ? 1f : 0f;
        float to = fadeIn ? 0f : 1f;

        foreach (Material mat in mats)
        {
            StartCoroutine(FadeMaterialTransparency(mat, from, to, duration));
            Debug.Log("fadein value is" + fadeIn);
        }

        yield return new WaitForSeconds(duration);
    }

    /// <summary>
    /// Collect materials with the _Transparent_Value property.
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

        foreach (Material mat in renderer.sharedMaterials)
        {
            if (mat.HasProperty("_Transparent_Value"))
            {
                targetList.Add(mat);
            }
        }
    }

    /// <summary>
    /// Coroutine to lerp the material's transparency.
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

    ///// <summary>
    /////  Fades out materials from the current fadeOutIndex object.
    ///// </summary>
    //public void FadeOut()
    //{
    //    materialsToFadeOut.Clear();

    //    if (fadeOutIndex < 0 || fadeOutIndex >= transitionPosesList.Count)
    //    {
    //        Debug.LogWarning("FadeOut index out of range.");
    //        return;
    //    }

    //    GameObject obj = transitionPosesList[fadeOutIndex];
    //    float duration = GetFadeOutDuration(fadeOutIndex);
    //    CollectMaterialsWithTransparency(obj, materialsToFadeOut);

    //    foreach (Material mat in materialsToFadeOut)
    //    {
    //        StartCoroutine(FadeMaterialTransparency(mat, 0f, 1f, duration));
    //    }

    //    fadeOutIndex++;
    //}

    ///// <summary>
    ///// Fades in materials from the current fadeInIndex object.
    ///// </summary>
    //public void FadeIn()
    //{
    //    materialsToFadeIn.Clear();

    //    if (fadeInIndex < 0 || fadeInIndex >= transitionPosesList.Count)
    //    {
    //        Debug.LogWarning("FadeIn index out of range.");
    //        return;
    //    }

    //    GameObject obj = transitionPosesList[fadeInIndex];
    //    float duration = GetFadeInDuration(fadeOutIndex);
    //    CollectMaterialsWithTransparency(obj, materialsToFadeIn);

    //    foreach (Material mat in materialsToFadeIn)
    //    {
    //        StartCoroutine(FadeMaterialTransparency(mat, 1f, 0f, duration));
    //    }

    //    fadeInIndex++;

    //}

    ///// <summary>
    ///// Collect all materials with "_Transparent_Value" from an object.
    ///// </summary>
    //private void CollectMaterialsWithTransparency(GameObject obj, List<Material> targetList)
    //{
    //    if (obj == null) return;

    //    MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
    //    if (renderer == null)
    //    {
    //        Debug.LogWarning($"No MeshRenderer on: {obj.name}");
    //        return;
    //    }

    //    foreach (Material mat in renderer.materials)
    //    {
    //        if (mat.HasProperty("_Transparent_Value"))
    //        {
    //            targetList.Add(mat);
    //        }
    //    }
    //}

    ///// <summary>
    ///// Coroutine to lerp a material's transparency
    ///// </summary>
    //private IEnumerator FadeMaterialTransparency(Material mat, float from, float to, float duration)
    //{
    //    float elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        float value = Mathf.Lerp(from, to, elapsed / duration);
    //        mat.SetFloat("_Transparent_Value", value);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    mat.SetFloat("_Transparent_Value", to); // Ensure final value is set
    //}

    ///// <summary>
    ///// Safely get fade-in duration for given index.
    ///// </summary>
    //private float GetFadeInDuration(int index)
    //{
    //    if (index >= 0 && index < fadeInDurations.Count)
    //    {
    //        return fadeInDurations[index];
    //    }

    //    return defaultFadeDuration;
    //}

    ///// <summary>
    ///// Safely get fade-out duration for given index.
    ///// </summary>
    //private float GetFadeOutDuration(int index)
    //{
    //    if (index >= 0 && index < fadeOutDurations.Count)
    //    {
    //        return fadeOutDurations[index];
    //    }

    //    return defaultFadeDuration;
    //}
}

