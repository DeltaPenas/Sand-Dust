using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    
    public static SoundController instance;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Volume")]
    [Range(0,1)] public float musicVolume = 1f;
    [Range(0,2)] public float sfxVolume = 2f;

    [Header("Musicas")]
    [SerializeField] private AudioClip menuTheme;
    [SerializeField] private AudioClip dungeonTheme;
    [SerializeField] private AudioClip bossTheme;

    [SerializeField] private float fadeDuration = 1f;

    public void TocarSom(AudioClip som)
    {
        sfxSource.PlayOneShot(som, sfxVolume);
    }

    private void Awake()
{
    if(instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        CarregarVolumes();

        PlayMenuMusic();
    }
    else
    {
        Destroy(gameObject);
    }
}

     public void PlayMenuMusic()
    {
        PlayMusic(menuTheme);
    }

    public void PlayDungeonMusic()
    {
        PlayMusic(dungeonTheme);
    }

    public void PlayBossMusic()
    {
        PlayMusic(bossTheme);
    }

    public void PlayMusic(AudioClip novaMusica)
    {
        if(musicSource.clip == novaMusica)
            return;

        StopAllCoroutines();
        StartCoroutine(FadeMusic(novaMusica));
    }

    private IEnumerator FadeMusic(AudioClip novaMusica)
    {
        while(musicSource.volume > 0)
        {
            musicSource.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();

        musicSource.clip = novaMusica;
        musicSource.Play();

        while(musicSource.volume < musicVolume)
        {
            musicSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.volume = musicVolume;
    }

    public void CarregarVolumes()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);

        musicSource.volume = musicVolume;
    }

    public void SetMusicVolume(float valor)
    {
        musicVolume = valor;

        musicSource.volume = musicVolume;

        PlayerPrefs.SetFloat("musicVolume", valor);
    }


    public void SetSFXVolume(float valor)
    {
        sfxVolume = valor;

        PlayerPrefs.SetFloat("sfxVolume", valor);
    }



}