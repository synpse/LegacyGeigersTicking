using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxInteractionDistance;
    public int   inventorySize;

    private CanvasManager       _canvasManager;
    private Camera              _camera;
    private RaycastHit          _raycastHit;
    private Interactible        _currentInteractible;
    private List<Interactible>  _inventory;
    private Color               _orange;

    private Interactible lastInteractible;
    private bool saved;

    private void Start()
    {
        _canvasManager = GameObject.Find("ActionCanvas").GetComponent<CanvasManager>();

        _camera = GetComponentInChildren<Camera>();

        _currentInteractible = null;

        _inventory = new List<Interactible>(inventorySize);

        _orange = new Color(1.0f, 0.64f, 0.0f);
    }

    void Update()
    {
        CheckForInteractible();
        CheckForInteractionClick();
    }

    private void CheckForInteractible()
    {
        if (Physics.Raycast(_camera.transform.position,
            _camera.transform.forward, out _raycastHit,
            maxInteractionDistance))
        {
            Interactible newInteractible = _raycastHit.collider.GetComponent<Interactible>();

            if (newInteractible != null && newInteractible.isInteractive)
            {
                SetInteractible(newInteractible);
            }
            else
            {
                ClearInteractible();
            }
        }
        else
        {
            ClearInteractible();
        }
    }

    private void CheckForInteractionClick()
    {
        if (Input.GetKeyDown(KeyCode.E) && _currentInteractible != null)
        {
            if (_currentInteractible.isPickable)
                AddToInventory(_currentInteractible);
            else if (HasRequirements(_currentInteractible))
                Interact(_currentInteractible);
            Debug.Log($"Hit with {_raycastHit.collider} with distance {_raycastHit.distance}");
        }
    }

    private void SetInteractible(Interactible newInteractible)
    {
        _currentInteractible = newInteractible;

        // If the interactible has no Outline component, add it
        if (_currentInteractible.GetComponent<Outline>() == null)
        {
            _currentInteractible.gameObject.AddComponent<Outline>();
        }

        // The object is an interactible and we're raycasting it, so outline it
        Outline outline = _currentInteractible.GetComponent<Outline>();
        outline.OutlineColor = _orange;
        outline.OutlineWidth = 9f;

        // We haven't saved this new interactible, then save it
        // It will be used later to compare new interactibles with the old one
        if (saved == false)
        {
            lastInteractible = newInteractible;
            saved = true;
        }

        // We compare the current object with the last one
        // If it is different, then we set the outline of the last one to 0
        // This will avoid duplicates and having the last object forever highlighted
        // Note: This only applies if we suddenly passed from object x to object y
        // without raycasting a non-interactible one in between
        if (_currentInteractible != lastInteractible)
        {
            Outline lastOutline = lastInteractible.GetComponent<Outline>();
            lastOutline.OutlineWidth = 0f;
        }

        if (HasRequirements(_currentInteractible))
            _canvasManager.ShowInteractionPanel(_currentInteractible.interactionText);
        else
            _canvasManager.ShowInteractionPanel(_currentInteractible.requirementText);
    }

    private void ClearInteractible()
    {
        // We clear the highlights on interactibles we're not using
        // With this we're also reinforcing what we did before
        if (saved == true)
        {
            Outline lastOutline = lastInteractible.GetComponent<Outline>();
            lastOutline.OutlineWidth = 0f;
            Outline outline = _currentInteractible.GetComponent<Outline>();
            outline.OutlineWidth = 0f;
            saved = false;
        }

        _currentInteractible = null;

        _canvasManager.HideInteractionPanel();
    }

    private bool HasRequirements(Interactible interactible)
    {
        for (int i = 0; i < interactible.inventoryRequirements.Length; ++i)
            if (!HasInInventory(interactible.inventoryRequirements[i]))
                return false;

        return true;
    }

    private void Interact(Interactible interactible)
    {
        if (interactible.consumesRequirements)
        {
            for (int i = 0; i < interactible.inventoryRequirements.Length; ++i)
                RemoveFromInventory(interactible.inventoryRequirements[i]);
        }

        interactible.Interact();
        Debug.Log($"Interacted with {interactible}.");
    }

    private void AddToInventory(Interactible pickable)
    {
        if (_inventory.Count < inventorySize)
        {
            _inventory.Add(pickable);
            pickable.gameObject.SetActive(false);
        }
    }

    private bool HasInInventory(Interactible pickable)
    {
        return _inventory.Contains(pickable);
    }

    private void RemoveFromInventory(Interactible pickable)
    {
        _inventory.Remove(pickable);
    }
}