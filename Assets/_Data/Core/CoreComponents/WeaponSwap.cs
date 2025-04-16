using System;
using UnityEngine;

public class WeaponSwap : CoreComponent
{
    [SerializeField] protected InteractableDetector interactableDetector;
    [SerializeField] protected WeaponInventory weaponInventory;

    protected WeaponDataSO newWeaponData;

    protected WeaponPickup weaponPickup;

    protected void OnEnable()
    {
        interactableDetector.OnTryInteract += HandleTryInteract;
    }

    protected void OnDisable()
    {
        interactableDetector.OnTryInteract -= HandleTryInteract;
    }

    public event Action<WeaponSwapChoiceRequest> OnChoiceRequested;
    public event Action<WeaponDataSO> OnWeaponDiscarded;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInteractableDetector();
        LoadWeaponInventory();
    }

    protected void LoadInteractableDetector()
    {
        if (interactableDetector != null) return;
        interactableDetector = core.GetComponentInChildren<InteractableDetector>();
        Debug.Log(transform.name + " :LoadInteractableDetector", gameObject);
    }

    protected void LoadWeaponInventory()
    {
        if (weaponInventory != null) return;
        weaponInventory = core.GetComponentInChildren<WeaponInventory>();
        Debug.Log(transform.name + " :LoadWeaponInventory", gameObject);
    }

    protected void HandleTryInteract(IInteractable interactable)
    {
        if (interactable is not WeaponPickup pickup) return;

        weaponPickup = pickup;
        newWeaponData = weaponPickup.GetContext();

        if (weaponInventory.TryGetEmptyIndex(out var index))
        {
            weaponInventory.TrySetWeapon(newWeaponData, index, out _);
            interactable.Interact();
            newWeaponData = null;
            return;
        }

        OnChoiceRequested?.Invoke(new WeaponSwapChoiceRequest(HandleWeaponSwapChoice,
            weaponInventory.GetWeaponSwapChoices(), newWeaponData));
    }

    protected void HandleWeaponSwapChoice(WeaponSwapChoice choice)
    {
        if (!weaponInventory.TrySetWeapon(newWeaponData, choice.Index, out var oldData)) return;

        newWeaponData = null;

        OnWeaponDiscarded?.Invoke(oldData);

        if (weaponPickup is null) return;

        weaponPickup.Interact();
    }
}