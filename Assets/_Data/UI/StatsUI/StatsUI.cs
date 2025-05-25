
using UnityEngine;
using UnityEngine.UI;

public abstract class StatsUI : NhoxBehaviour
{
    [SerializeField] protected Core core;
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] protected Slider slider;

    protected override void Start()
    {
        base.Start();
        UpdateBarUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCore();
        LoadRectTransform();
        LoadSliderBar();
    }

    protected abstract void LoadCore();
    
    protected void LoadRectTransform()
    {
        if (rectTransform != null) return;
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(transform.name + " LoadRectTransform", gameObject);
    }
    
    protected void LoadSliderBar()
    {
        if (slider != null) return;
        slider = GetComponent<Slider>();
        Debug.Log(transform.name + " LoadHPBar", gameObject);
    }

    protected abstract void UpdateBarUI();
}
