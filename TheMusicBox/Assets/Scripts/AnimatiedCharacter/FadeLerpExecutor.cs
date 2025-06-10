using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Fade/Fade Lerp Executor")]
public class FadeLerpExecutor : ScriptableObject
{
    public enum FadeSignalType
    {
        FadeIn = 0,
        FadeOut = 1
    }
    public IEnumerator ExecuteFade(Material mat, float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float value = Mathf.Lerp(from, to, elapsed / duration);
            mat.SetFloat("_Transparent_Value", value);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.SetFloat("_Transparent_Value", to);
    }

    public void Fade(GameObject obj, int signalTypeInt, float duration, MonoBehaviour context)
    {
        FadeSignalType signalType = (FadeSignalType)signalTypeInt;

        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer == null) return;

        foreach (var mat in renderer.materials)
        {
            if (!mat.HasProperty("_Transparent_Value")) continue;

            float from = signalType == FadeSignalType.FadeIn ? 1f : 0f;
            float to = signalType == FadeSignalType.FadeIn ? 0f : 1f;
            context.StartCoroutine(ExecuteFade(mat, from, to, duration));
        }
    }

}
