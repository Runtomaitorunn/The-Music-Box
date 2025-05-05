using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadingTransitionLerp : MonoBehaviour
{
    [Header("Transition Poses")]
    [Tooltip("Only GameObjects can be added to this list!")]
    [SerializeField] private List<GameObject> transitionPosesList = new List<GameObject>();


    private void Start()
    {
        CheckMaterialByName();


    }
    /// <summary>
    /// Function controls fading out 
    /// </summary>
    public void FadeOut()
    {

    }

    /// <summary>
    /// Function controls fading in
    /// </summary>
    public void FadeIn()
    {

    }

    /// <summary>
    /// Check the material named 'BaseColor'
    /// </summary>
    public void CheckMaterialByName()
    {
        foreach (GameObject obj in transitionPosesList)
        {
            if (obj == null) continue;

            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                Debug.LogWarning($"No MeshRenderer on: {obj.name}");
                continue;
            }

            foreach (Material mat in meshRenderer.materials)
            {
                if (mat.name.StartsWith("BaseColor")) 
                {
                    Debug.Log($"yes basecolor got it on: {obj.name}");
                }
            }
        }
    }
}
