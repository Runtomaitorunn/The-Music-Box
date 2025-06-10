using UnityEngine;

public abstract class FadeMode : ScriptableObject
{
    [SerializeField] protected FadeLerpExecutor executor;
    public abstract void ApplyFade(
        GameObject obj,
        string objectId,
        int signalTypeInt,
        MonoBehaviour context,
        ref bool fadeInReceived,
        ref bool fadeOutReceived,
        System.Action advanceToNext,
        float fadeInDuration,
        float fadeOutDuration);
}
