using UnityEngine;

public class Pressable : MonoBehaviour, IInteractible
{
    public bool isInteractive;
    public bool allowsMultipleInteractions;
    public string interactionText;
    public GameObject interactable;

    private IIndirectInteractible isInteractable;

    void Start()
    {
        if (interactable != null)
            isInteractable = interactable.GetComponent<IIndirectInteractible>();
    }

    public bool IsInteractive()
    {
        return isInteractive;
    }

    public string GetInteractionText()
    {
        return interactionText;
    }

    public void Interact()
    {
        if (isInteractive)
        {
            GetComponent<Animator>().SetTrigger("Press");

            if (isInteractable != null)
                isInteractable.Interact();

            if (!allowsMultipleInteractions)
                isInteractive = false;
        }
    }

}
