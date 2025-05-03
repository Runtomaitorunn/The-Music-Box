using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadingTransitionLerp : MonoBehaviour
{
    [Header("Transition Poses in Order")]
    [SerializeField] private List<GameObject> transitionObjectList = new List<GameObject>();

    [Header("Fade Durations")]
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float fadeInDuration = 1f;

    private int fadeOutIndex = 0;
    private int fadeInIndex = 1;

    /// <summary>
    /// Call this from Timeline signal to fade out the current object.
    /// </summary>
    public void TriggerFadeOut()
    {
        if (fadeOutIndex >= 0 && fadeOutIndex < transitionObjectList.Count)
        {
            GameObject obj = transitionObjectList[fadeOutIndex];
            Material mat = GetMaterial(obj);
            if (mat != null)
                StartCoroutine(FadeAlpha(mat, 1f, 0f, fadeOutDuration));

            fadeOutIndex++;
        }
    }

    /// <summary>
    /// Call this from Timeline signal to fade in the next object.
    /// </summary>
    public void TriggerFadeIn()
    {
        if (fadeInIndex >= 0 && fadeInIndex < transitionObjectList.Count)
        {
            GameObject obj = transitionObjectList[fadeInIndex];
            Material mat = GetMaterial(obj);
            if (mat != null)
            {
                SetAlpha(mat, 0f); // Ensure it's transparent before fade-in
                StartCoroutine(FadeAlpha(mat, 0f, 1f, fadeInDuration));
            }

            fadeInIndex++;
        }
    }

    private IEnumerator FadeAlpha(Material mat, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = mat.GetColor("_BaseColor");

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            color.a = alpha;
            mat.SetColor("_BaseColor", color);
            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = to;
        mat.SetColor("_BaseColor", color);
    }

    private Material GetMaterial(GameObject obj)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null)
            return rend.material; // Use instance material
        else
        {
            Debug.LogWarning("No Renderer found on: " + obj.name);
            return null;
        }
    }

    private void SetAlpha(Material mat, float alpha)
    {
        if (mat == null) return;
        Color color = mat.GetColor("_BaseColor");
        color.a = alpha;
        mat.SetColor("_BaseColor", color);
    }
}
