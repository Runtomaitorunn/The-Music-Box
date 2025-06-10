using UnityEngine;
using System.Collections.Generic;

public class FadeTransitionManager : MonoBehaviour
{
    public List<FadeObjectEntry> transitionEntries = new();

    [System.Serializable]
    public class FadeObjectEntry
    {
        public GameObject target;
        public FadeMode fadeMode;
        public string objectId;
        public float fadeInDuration = 1f;
        public float fadeOutDuration = 1f;

        [HideInInspector] public bool fadeInReceived = false;
        [HideInInspector] public bool fadeOutReceived = false;
    }

    public void FadeOutSignal(string objectId)
    {
        foreach (var entry in transitionEntries)
        {
            if (entry.target == null || entry.fadeMode == null)
                continue;

            if (entry.objectId != objectId)
                continue;

            if (entry.fadeOutReceived) return; 

            entry.fadeMode.ApplyFade(
                entry.target,
                entry.objectId,
                1, // 1 = FadeOut
                context: this,
                ref entry.fadeInReceived,
                ref entry.fadeOutReceived,
                fadeInDuration: entry.fadeInDuration,
                fadeOutDuration: entry.fadeOutDuration,
                advanceToNext: () => { }
            );

            return;
        }
    }


    public void FadeInSignal(string objectId)
    {
        foreach (var entry in transitionEntries)
        {
            if (entry.target == null || entry.fadeMode == null)
                continue;

            if (entry.objectId != objectId)
                continue;

            if (entry.fadeInReceived) return; 

            entry.fadeMode.ApplyFade(
                entry.target,
                entry.objectId,
                0, // 0 = FadeIn
                context: this,
                ref entry.fadeInReceived,
                ref entry.fadeOutReceived,
                fadeInDuration: entry.fadeInDuration,
                fadeOutDuration: entry.fadeOutDuration,
                advanceToNext: () => { }
            );

            return; 
        }
    }

}
