using UnityEngine;

public enum ObjectType
{
    Static,
    Dynamic,
    Interactive
}
public class InteractableObject : MonoBehaviour
{
    public ObjectType objectType;

    public void Interact()
    {
        if(objectType == ObjectType.Interactive)
        {
            // Have interaction logic here
        }
        else
        {
            // Don't interact
        }
    }

}
