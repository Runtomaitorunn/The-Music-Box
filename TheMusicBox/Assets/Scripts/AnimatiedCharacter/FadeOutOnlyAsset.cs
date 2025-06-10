using UnityEngine;

[CreateAssetMenu(menuName = "Fade/Fade Out Only")]
public class FadeOutOnlyAsset : FadeMode
{
    public override void ApplyFade(GameObject obj, string objectId, int signalTypeInt, MonoBehaviour context,
        ref bool fadeInReceived, ref bool fadeOutReceived, System.Action advanceToNext,
        float fadeInDuration, float fadeOutDuration)
    {
        if (signalTypeInt == 1) // 1 = FadeOut
            executor.Fade(obj, 1, fadeOutDuration, context);
    }
}