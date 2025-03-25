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

    private void OnEnable()
    {
        dataSO = target as WeaponDataSO;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        foreach(var dataCompType in dataCompTypes)
        {
            if(GUILayout.Button(dataCompType.Name))
            {
                var comp = Activator.CreateInstance(dataCompType) as ComponentData;

                if (comp == null) return;

                dataSO.AddData(comp);
            }
        }
    }

    [DidReloadScripts]
    protected static void OnRecompile()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(assemblies => assemblies.GetTypes());
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );

        dataCompTypes = filteredTypes.ToList();
    }
}
