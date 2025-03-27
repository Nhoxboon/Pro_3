using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : NhoxBehaviour
{
    [SerializeField] protected Weapon weapon;
    [SerializeField] protected WeaponDataSO weaponDataSO;

    protected List<WeaponComponent> componentsAlreadyOnWp = new List<WeaponComponent>();

    protected List<WeaponComponent> componentsAddedToWp = new List<WeaponComponent>();

    protected List<Type> componentDependencies = new List<Type>();

    [SerializeField] protected Animator anim;

    protected override void Start()
    {
        base.Start();
        GenerateWeapon(weaponDataSO);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeapon();
        LoadAnim();
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

    [ContextMenu("Test Generate")]
    private void TestGeneration()
    {
        GenerateWeapon(weaponDataSO);
    }

    public void GenerateWeapon(WeaponDataSO data)
    {
        weapon.SetData(data);

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
    }
}
