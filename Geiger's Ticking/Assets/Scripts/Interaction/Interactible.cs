using UnityEngine;

public class Interactible : MonoBehaviour
{
    public bool isInteractive;
    public bool isActive;
    public bool isPickable;
    public bool allowsMultipleInteractions;
    public string requirementText;
    public string interactionText;
    public bool consumesRequirements;
    public Interactible[] inventoryRequirements;
    public Interactible[] indirectInteractibles;
    public Interactible[] indirectActivations;

    public void Activate()
    {
        isActive = true;

        PlayActivateAnimation();
    }

    public void Interact()
    {
        PlayInteractAnimation();

        if (isActive)
        {
            InteractIndirects();

            ActivateIndirects();

            if (!allowsMultipleInteractions)
                isInteractive = false;
        }
    }

    private void PlayActivateAnimation()
    {
        Animator animator = GetComponent<Animator>();

        if (animator != null)
            animator.SetTrigger("Activate");
    }

    private void PlayInteractAnimation()
    {
        Animator animator = GetComponent<Animator>();

        if (animator != null)
            animator.SetTrigger("Interact");
    }

    private void InteractIndirects()
    {
        if (indirectInteractibles != null)
        {
            for (int i = 0; i < indirectInteractibles.Length; ++i)
                indirectInteractibles[i].Interact();
        }
    }

    private void ActivateIndirects()
    {
        if (indirectActivations != null)
        {
            for (int i = 0; i < indirectActivations.Length; ++i)
                indirectActivations[i].Activate();
        }
    }

}