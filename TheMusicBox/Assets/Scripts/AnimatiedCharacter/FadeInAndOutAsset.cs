using UnityEngine;

[CreateAssetMenu(menuName = "Fade/Fade In Then Out")]
public class FadeInAndOutAsset : FadeMode
{
    public override void ApplyFade(GameObject obj, string objectId, int signalTypeInt, MonoBehaviour context,
        ref bool fadeInReceived, ref bool fadeOutReceived, System.Action advanceToNext,
        float fadeInDuration, float fadeOutDuration)
    {
        if (signalTypeInt == 0 && !fadeInReceived)
        {
            executor.Fade(obj, 0, fadeInDuration, context);
            fadeInReceived = true;
        }
        else if (signalTypeInt == 1 && fadeInReceived)
        {
            executor.Fade(obj, 1, fadeOutDuration, context);
            advanceToNext();
        }
    }
}
