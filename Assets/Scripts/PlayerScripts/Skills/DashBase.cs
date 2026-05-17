using System.Collections;
using UnityEngine;

public abstract class DashBase : MonoBehaviour
{
    public SoundController soundController;
    public float forçaDash;
    public float duracaoDash = 0.2f;
    public float cooldown;
    public float ultimoUso;
    protected PlayerController pc;

    protected virtual void Start()
    {
        pc = GetComponent<PlayerController>();
        forçaDash = pc.currentStatus.forcaDash;
        cooldown = pc.currentStatus.dashCooldown;

        soundController = FindAnyObjectByType<SoundController>();
        
    }

    public virtual bool podeUsarDash()
    {
        return Time.time >= ultimoUso + cooldown;
    }

    public void tentaUsarDash()
    {
        if (podeUsarDash())
        {
            StartCoroutine(ExecutarDash());
            
            ultimoUso = Time.time;
        }
    }

    private IEnumerator ExecutarDash()
    {
        pc.DispararOnDash();
        pc.podeMover = false;
        

        // ativa iframe
        StartCoroutine(Iframe());

        // executa o dash
        usadash();

        yield return new WaitForSeconds(duracaoDash);

        // para a bomba do dash
        pc.rig.linearVelocity = Vector2.zero;

        // libera movimento
        pc.podeMover = true;
    }

    protected abstract void usadash();

    private IEnumerator Iframe()
    {
        pc.iframeAtivo = true;

        yield return new WaitForSeconds(pc.iframetempo + pc.iframeTempoBuff);

        pc.iframeAtivo = false;
    }
}