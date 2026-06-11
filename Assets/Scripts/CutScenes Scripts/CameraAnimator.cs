using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    public void SubirATela()
    {
        anim.SetTrigger("SubirTela");
    }
}
