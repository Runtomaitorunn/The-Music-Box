using UnityEngine;
using UnityEngine.Events;

public enum ObjectType
{
    Required,
    Optional
}
public class InteractableObject : MonoBehaviour
{
    [Header("Object Type")]
    public ObjectType objectType;

    [Header("Required Object Actions")]
    public UnityEvent onRequiredInteraction;

    [Header("Optional Object Actions")]
    public UnityEvent onOptionalInteraction;

    public void Interact()
    {
        if(objectType == ObjectType.Required)
        {
            onRequiredInteraction?.Invoke();
        }
        else
        {
            onOptionalInteraction?.Invoke();
        }
    }

}
