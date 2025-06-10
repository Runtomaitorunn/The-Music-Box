using UnityEngine;

[CreateAssetMenu(menuName = "Fade/Fade In Only")]
public class FadeInOnlyAsset : FadeMode
{
    public override void ApplyFade(GameObject obj, string objectId, int signalTypeInt, MonoBehaviour context,
        ref bool fadeInReceived, ref bool fadeOutReceived, System.Action advanceToNext,
        float fadeInDuration, float fadeOutDuration)
    {
        if (signalTypeInt == 0) // FadeIn
        {
            executor.Fade(obj, signalTypeInt, fadeInDuration, context);
        }
    }
}