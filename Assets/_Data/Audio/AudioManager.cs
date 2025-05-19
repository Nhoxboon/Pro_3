
using UnityEngine;

public class AudioManager : NhoxBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;
    
    [SerializeField] protected AudioSource musicSource;
    [SerializeField] protected AudioSource sfxSource;
    
    [SerializeField] protected AudioClip musicClip;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 AudioManager allow to exist");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        PlayMusic();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMusicSource();
        LoadSFXSource();
    }
    
    protected void LoadMusicSource()
    {
        if(musicSource != null) return;
        musicSource = transform.Find("Music").GetComponent<AudioSource>();
        Debug.Log(transform.name + " :LoadMusicSource", gameObject);
    }
    
    protected void LoadSFXSource()
    {
        if(sfxSource != null) return;
        sfxSource = transform.Find("SFX").GetComponent<AudioSource>();
        Debug.Log(transform.name + " :LoadSFXSource", gameObject);
    }

    protected void PlayMusic()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip, float pitch = 1f)
    {
        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(clip);
    }
    
    public void PlaySFXLoop(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    public void StopSFXLoop()
    {
        sfxSource.loop = false;
        sfxSource.Stop();
    }

}
