using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        musicSlider.value = SoundController.instance.musicVolume;
        sfxSlider.value = SoundController.instance.sfxVolume;
    }

    public void SetMusicVolume()
    {
        SoundController.instance.SetMusicVolume(musicSlider.value);
    }

    public void SetSFXVolume()
    {
        SoundController.instance.SetSFXVolume(sfxSlider.value);
    }
}