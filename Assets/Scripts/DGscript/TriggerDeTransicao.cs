using UnityEngine;

public class TriggerDeTransicao : MonoBehaviour
{
    public Animator transicao;

    public void FadeOut()
    {
        transicao.ResetTrigger("Start");
        transicao.SetTrigger("Start");
    }
}