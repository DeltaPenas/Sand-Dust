using System;
using UnityEngine;

public class TriggerDeTransicao : MonoBehaviour
{
    public Animator transicao;

    public Action OnFadeCompleto;

    private bool emTransicao;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void FadeOut()
    {
        if (emTransicao)
            return;

        emTransicao = true;

        transicao.SetTrigger("Start");
    }
    public void FadeIn()
    {
        transicao.SetTrigger("End");
    }
   
    public void FadeFinalizado()
    {
        OnFadeCompleto?.Invoke();
        OnFadeCompleto = null;

        emTransicao = false;
    }
}