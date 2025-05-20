using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableDetector : CoreComponent
{
    [SerializeField] protected SpriteRenderer interactableSprite;

    protected IInteractableItem closestInteractable;

    protected float distanceToClosestInteractable = float.PositiveInfinity;

    protected IInteractable interactableInRange;

    protected List<IInteractableItem> interactables = new();
    public Action<IInteractableItem> OnTryInteract;

    protected void Update()
    {
        if (interactables.Count <= 0) return;

        var oldClosestInteractable = closestInteractable;

        UpdateClosestInteractable();

        if (closestInteractable == oldClosestInteractable)
            return;

        HandleInteractableSwitch(oldClosestInteractable, closestInteractable);
    }

    protected void OnEnable()
    {
        core.Movement.OnFlip += FlipUI;
    }

    protected void OnDisable()
    {
        core.Movement.OnFlip -= FlipUI;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var interactable in interactables)
        {
            Gizmos.color = interactable == closestInteractable ? Color.red : Color.white;

            Gizmos.DrawLine(transform.position, interactable.GetPosition());
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsInteractable(out var interactable))
        {
            interactables.Add(interactable);
        }

        else if (other.TryGetComponent(out IInteractable interactableInRange) && interactableInRange.CanInteract())
        {
            interactableSprite.gameObject.SetActive(true);
            ;
            this.interactableInRange = interactableInRange;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.IsInteractable(out var interactable))
        {
            interactables.Remove(interactable);

            if (interactable == closestInteractable)
            {
                interactable.DisableInteraction();
                closestInteractable = null;
            }
        }

        else if (other.TryGetComponent(out IInteractable interactableInRange) &&
                 interactableInRange == this.interactableInRange)
        {
            this.interactableInRange = null;
            interactableSprite.gameObject.SetActive(false);
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInteractableSprite();
    }

    protected void LoadInteractableSprite()
    {
        if (interactableSprite != null) return;
        interactableSprite = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(transform.name + " LoadInteractableSprite", gameObject);
    }

    [ContextMenu("TryInteract")]
    public void TryInteract(bool inputValue)
    {
        if (!inputValue) return;

        if (closestInteractable is not null)
            OnTryInteract?.Invoke(closestInteractable);

        else if (interactableInRange is not null) interactableInRange.Interact();
    }

    protected void UpdateClosestInteractable()
    {
        closestInteractable = null;
        distanceToClosestInteractable = float.PositiveInfinity;

        foreach (var interactable in interactables)
        {
            var distance = FindDistanceTo(interactable);

            if (distance < distanceToClosestInteractable)
            {
                closestInteractable = interactable;
                distanceToClosestInteractable = distance;
            }
        }
    }

    protected void HandleInteractableSwitch(IInteractableItem oldInteractable, IInteractableItem newInteractable)
    {
        oldInteractable?.DisableInteraction();
        newInteractable?.EnableInteraction();
    }

    protected float FindDistanceTo(IInteractableItem interactable)
    {
        return Vector3.Distance(transform.position, interactable.GetPosition());
    }

    protected void FlipUI()
    {
        interactableSprite.gameObject.transform.Rotate(0, 180, 0);
    }
}