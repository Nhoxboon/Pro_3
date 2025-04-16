using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : NhoxBehaviour
{
    public event Action OnWeaponGenerating;

    [SerializeField] protected Weapon weapon;
    
    [SerializeField] protected CombatInputs combatInput;


    protected List<WeaponComponent> componentsAlreadyOnWp = new List<WeaponComponent>();

    protected List<WeaponComponent> componentsAddedToWp = new List<WeaponComponent>();

    protected List<Type> componentDependencies = new List<Type>();

    [SerializeField] protected Animator anim;
    
    [SerializeField] protected WeaponInventory weaponInventory;

    protected override void Start()
    {
        base.Start();
        weaponInventory.OnWeaponDataChanged += HandleWeaponDataChanged;
        
        if (weaponInventory.TryGetWeapon((int)combatInput, out var data))
        {
            GenerateWeapon(data);
        }
    }
    
    protected void OnDestroy()
    {
        weaponInventory.OnWeaponDataChanged -= HandleWeaponDataChanged;
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeapon();
        LoadAnim();
        LoadWeaponInventory();
    }

    protected void LoadWeapon()
    {
        if (weapon != null) return;
        weapon = GetComponent<Weapon>();
        Debug.Log(transform.name + " :LoadWeapon", gameObject);
    }

    protected void LoadAnim()
    {
        if (anim != null) return;
        anim = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + " :LoadAnim", gameObject);
    }
    
    protected void LoadWeaponInventory()
    {
        if (weaponInventory != null) return;
        weaponInventory = FindFirstObjectByType<WeaponInventory>();
        Debug.Log(transform.name + " :LoadWeaponInventory", gameObject);
    }

    public void GenerateWeapon(WeaponDataSO data)
    {
        OnWeaponGenerating?.Invoke();

        weapon.SetData(data);
        
        if (data is null)
        {
            weapon.SetCanEnterAttack(false);
            return;
        }

        componentsAlreadyOnWp.Clear();
        componentsAddedToWp.Clear();
        componentDependencies.Clear();

        componentsAlreadyOnWp = GetComponents<WeaponComponent>().ToList();

        componentDependencies = data.GetAllDependencies();

        foreach (var dependency in componentDependencies)
        {
            if(componentsAddedToWp.FirstOrDefault(component => component.GetType() == dependency) != null) continue;

            var weaponComponent = componentsAlreadyOnWp.FirstOrDefault(component => component.GetType() == dependency);

            if (weaponComponent == null)
            {
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
            }

            weaponComponent.Init();

            componentsAddedToWp.Add(weaponComponent);
        }

        var componentsToRemove = componentsAlreadyOnWp.Except(componentsAddedToWp);

        foreach (var component in componentsToRemove)
        {
            Destroy(component);
        }

        anim.runtimeAnimatorController = data.animatorController;
        
        weapon.SetCanEnterAttack(true);
    }
    
    private void HandleWeaponDataChanged(int inputIndex, WeaponDataSO data)
    {
        if (inputIndex != (int)combatInput) return;
            
        GenerateWeapon(data);
    }
}
