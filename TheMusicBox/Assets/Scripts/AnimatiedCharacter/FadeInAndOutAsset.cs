using UnityEngine;

[CreateAssetMenu(menuName = "Fade/Fade In Then Out")]
public class FadeInAndOutAsset : FadeMode
{
    public override void ApplyFade(GameObject obj, string objectId, int signalTypeInt, MonoBehaviour context,
        ref bool fadeInReceived, ref bool fadeOutReceived, System.Action advanceToNext,
        float fadeInDuration, float fadeOutDuration)
    {
        Debug.Log(signalTypeInt + "signal type int is ");
        Debug.Log(fadeInReceived + "fade in received bool is ");
        Debug.Log(fadeOutReceived + "fade out received bool is ");


        if (signalTypeInt == 0 && !fadeInReceived)
        {
            Debug.Log("fade in is anyone exe me??");
            executor.Fade(obj, 0, fadeInDuration, context);
            fadeInReceived = true;
        }
        else if (signalTypeInt == 1 && !fadeOutReceived)
        {
            Debug.Log("fade out is anyone exe me??");
            executor.Fade(obj, 1, fadeOutDuration, context);
            fadeOutReceived = true;
            advanceToNext();
        }
    }
}
