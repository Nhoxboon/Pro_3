
using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class InteractableDetector : CoreComponent
{
    public Action<IInteractable> OnTryInteract;
    
    protected List<IInteractable> interactables = new();

    protected IInteractable closestInteractable;
    
    protected float distanceToClosestInteractable = float.PositiveInfinity;
    
    [ContextMenu("TryInteract")]
    public void TryInteract(bool inputValue)
    {
        if(!inputValue || closestInteractable is null) return;
        
        OnTryInteract?.Invoke(closestInteractable);
    }

    protected void Update()
    {
        if (interactables.Count <= 0) return;

        var oldClosestInteractable = closestInteractable;

        UpdateClosestInteractable();

        if (closestInteractable == oldClosestInteractable)
            return;

        HandleInteractableSwitch(oldClosestInteractable, closestInteractable);
    }

    protected void UpdateClosestInteractable()
    {
        closestInteractable = null;
        distanceToClosestInteractable = float.PositiveInfinity;

        foreach (var interactable in interactables)
        {
            float distance = FindDistanceTo(interactable);

            if (distance < distanceToClosestInteractable)
            {
                closestInteractable = interactable;
                distanceToClosestInteractable = distance;
            }
        }
    }

    protected void HandleInteractableSwitch(IInteractable oldInteractable, IInteractable newInteractable)
    {
        oldInteractable?.DisableInteraction();
        newInteractable?.EnableInteraction();
    }

    protected float FindDistanceTo(IInteractable interactable)
    {
        return Vector3.Distance(transform.position, interactable.GetPosition());
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsInteractable(out var interactable))
        {
            interactables.Add(interactable);
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
    }
    
    private void OnDrawGizmosSelected()
    {
        foreach (IInteractable interactable in interactables)
        {
            Gizmos.color = interactable == closestInteractable ? Color.red : Color.white;

            Gizmos.DrawLine(transform.position, interactable.GetPosition());
        }
    }
}
