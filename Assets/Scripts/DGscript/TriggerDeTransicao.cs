using UnityEngine;

public class TriggerDeTransicao : MonoBehaviour
{
    public Animator transicao;
    private void Awake()
    {
    DontDestroyOnLoad(gameObject);
    }

    public void FadeOut()
    {
        if (transicao == null)
        {
          return;  
        }
        transicao.ResetTrigger("Start");
        transicao.SetTrigger("Start");
    }
    
}