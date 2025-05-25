
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BtnBaseAbstract : NhoxBehaviour
{
    [SerializeField] protected Button btn;

    protected void OnEnable()
    {
        btn.onClick.AddListener(OnClick);
    }
    
    protected void OnDisable()
    {
        btn.onClick.RemoveListener(OnClick);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtn();
    }

    protected void LoadBtn()
    {
        if(btn != null) return;
        btn = GetComponent<Button>();
        Debug.Log(transform.name + " :LoadBtn", gameObject);
    }

    protected abstract void OnClick();
}
