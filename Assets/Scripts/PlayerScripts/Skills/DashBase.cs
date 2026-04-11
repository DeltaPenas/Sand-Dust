using System.Collections;
using UnityEngine;

public abstract class DashBase : MonoBehaviour
{
    public SoundController soundController;
    public float forçaDash = 10f;
    public float duracaoDash = 0.2f;
    public float cooldown = 1f;
    public float ultimoUso;
    protected PlayerController pc;

    protected virtual void Start()
    {
        pc = GetComponent<PlayerController>();
        soundController = GetComponent<SoundController>();
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