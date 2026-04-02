using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public float globalSoundVolume = 1f;

    public void TocarSom(AudioClip som)
    {
        AudioSource.PlayClipAtPoint(som, transform.position, globalSoundVolume);
    }
}
