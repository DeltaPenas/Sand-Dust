using UnityEngine;
using UnityEngine.UI;

public class VolumeUi : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider musicSlider;

    void Start()
    {
        
    }

    public void SetVolumeSfx()
    {
        AudioListener.volume = sfxSlider.value;
    }
       public void SetVolumeMusic()
    {
        AudioListener.volume = musicSlider.value;
    }

    public void SalvarVolumeSfx()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    public void SalvarVolumeMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void CarregarVolumeSfx()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

    }

    public void CarregarVolumeMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }


}