using UnityEngine;

public class FadeController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChamarFade()
    {
        anim.SetTrigger("AtivarFade");
    }
    public void TirarFade()
    {
        anim.SetTrigger("DesativarFade");
    }


}