using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxInteractionDistance;

    private CanvasManager canvasManager;
    private Camera cam;
    private RaycastHit raycastHit;
    private IInteractible currentInteractible;

    private void Start()
    {
        canvasManager = CanvasManager.instance;

        cam = GetComponentInChildren<Camera>();

        currentInteractible = null;
    }

    void Update()
    {
        CheckForInteractible();
        CheckForInteractionClick();
    }

    private void CheckForInteractible()
    {
        if (Physics.Raycast(cam.transform.position,
            cam.transform.forward, out raycastHit,
            maxInteractionDistance))
        {
            IInteractible newInteractible = raycastHit.collider.GetComponent<IInteractible>();

            if (newInteractible != null && newInteractible.IsInteractive())
                SetInteractible(newInteractible);
            else
                ClearInteractible();
        }
        else
            ClearInteractible();
    }

    private void CheckForInteractionClick()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractible != null)
        {
            currentInteractible.Interact();
            Debug.Log($"Interacted with {currentInteractible}.");
        }
    }

    private void SetInteractible(IInteractible newInteractible)
    {
        if (newInteractible != currentInteractible)
        {
            currentInteractible = newInteractible;

            canvasManager.ShowInteractionPanel(currentInteractible.GetInteractionText());
        }
    }

    private void ClearInteractible()
    {
        currentInteractible = null;

        canvasManager.HideInteractionPanel();
    }

}
