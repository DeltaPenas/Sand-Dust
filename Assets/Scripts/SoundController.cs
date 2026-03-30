using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip passosSfx;
    public PlayerController pc;

    void Start()
    {

        StartCoroutine(PlayFootSteps());
    }

    IEnumerator PlayFootSteps()
    {
        while (true)
        {
            if (pc.movimento.magnitude > 0.1f)
            {
            AudioSource.PlayClipAtPoint(passosSfx, pc.transform.position);
            }
            yield return new WaitForSeconds(0.40f);
        }
        
    }
}
