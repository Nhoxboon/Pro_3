using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.Callbacks;
using System.Linq;

[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSOEditor : Editor
{
    protected static List<Type> dataCompTypes = new List<Type>();
    protected WeaponDataSO dataSO;

    protected bool showForceUpdateBtns;
    protected bool showAddComponentBtns;

    private void OnEnable()
    {
        dataSO = target as WeaponDataSO;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SetNumberOfAttacksBtns();
        AddComponentBtns();
        ForceUpdateBtns();
    }

    [DidReloadScripts]
    protected static void OnRecompile()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(assemblies => assemblies.GetTypes());
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentDataAbstract)) && !type.ContainsGenericParameters && type.IsClass
            );

        dataCompTypes = filteredTypes.ToList();
    }

    private void SetNumberOfAttacksBtns()
    {
        if (GUILayout.Button("Set Number of Attacks"))
        {
            foreach (var item in dataSO.componentData)
            {
                item.InitializeAttackData(dataSO.numberOfAttacks);
            }
        }
    }

    private void AddComponentBtns()
    {
        showAddComponentBtns = EditorGUILayout.Foldout(showAddComponentBtns, "Add Components");

        if (showAddComponentBtns)
        {
            foreach (var dataCompType in dataCompTypes)
            {
                if (GUILayout.Button(dataCompType.Name))
                {
                    var comp = Activator.CreateInstance(dataCompType) as ComponentDataAbstract;
                    if (comp == null) return;

                    comp.InitializeAttackData(dataSO.numberOfAttacks);
                    dataSO.AddData(comp);

                    EditorUtility.SetDirty(dataSO);
                }
            }
        }
    }

    private void ForceUpdateBtns()
    {
        showForceUpdateBtns = EditorGUILayout.Foldout(showForceUpdateBtns, "Force Update Btns");

        if (showForceUpdateBtns)
        {
            if (GUILayout.Button("Force update component name"))
            {
                foreach (var item in dataSO.componentData)
                {
                    item.SetComponentName();
                }
            }

            if (GUILayout.Button("Force update attack name"))
            {
                foreach (var item in dataSO.componentData)
                {
                    item.SetAttackDataName();
                }
            }
        }
    }
}
