
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : NhoxBehaviour
{
    [SerializeField] protected AudioMixer myMixer;
    [SerializeField] protected Slider musicSlider;
    [SerializeField] protected Slider sfxSlider;

    protected void OnEnable()
    {
        musicSlider.onValueChanged.AddListener((value) => SetVolume("music", "musicVolume", value));
        sfxSlider.onValueChanged.AddListener((value) => SetVolume("sfx", "sfxVolume", value));
    }

    protected void OnDisable()
    {
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
    }

    protected override void Start()
    {
        base.Start();
        LoadVolume("music", "musicVolume", musicSlider);
        LoadVolume("sfx", "sfxVolume", sfxSlider);
        gameObject.SetActive(false);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMixer();
        LoadMusicSlider();
        LoadSFXSlider();
    }
    
    protected void LoadMixer()
    {
        if (myMixer != null) return;
        myMixer = Resources.Load<AudioMixer>("Audio/Audio");
        Debug.Log(transform.name + " :LoadMixer", gameObject);
    }
    
    protected void LoadMusicSlider()
    {
        if (musicSlider != null) return;
        musicSlider = transform.Find("MusicSlider").GetComponent<Slider>();
        Debug.Log(transform.name + " :LoadMusicSlider", gameObject);
    }
    
    protected void LoadSFXSlider()
    {
        if (sfxSlider != null) return;
        sfxSlider = transform.Find("SFXSlider").GetComponent<Slider>();
        Debug.Log(transform.name + " :LoadSFXSlider", gameObject);
    }
    #endregion

    protected void SetVolume(string mixerParam, string key, float volume)
    {
        myMixer.SetFloat(mixerParam, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(key, volume);
    }

    protected void LoadVolume(string mixerParam, string key, Slider slider)
    {
        float volume = PlayerPrefs.GetFloat(key, 1f);
        slider.value = volume;
        SetVolume(mixerParam, key, volume);
    }
}
