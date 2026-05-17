using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{

    [Header("UI")]
    public string skillNome;
    [TextArea] public string skillDescricao;
    public Sprite skillIcone;

    [Header("Status")]
    public float skillRange;
    public float skillDmg;
    public float cooldown;
    public float cooldownBase;
    public float ultimoUso;
    public int stacksAtual;
    public int maxStacks;

    protected Transform pontoSkill;
    public PlayerController pc;
    public SoundController sc;
    public PlayerVida pv;
    public AudioClip ac;

    protected virtual void Start()
    {
        
        pc = GetComponentInParent<PlayerController>();
        pv = GetComponentInParent<PlayerVida>();
        sc = FindAnyObjectByType<SoundController>();
    

        if (pc == null)
        {
            return;
        }

        if (pc.skillPoint == null)
        {
            return;
        }

        pontoSkill = pc.skillPoint;

        DefirnirStatus();
        ultimoUso = -cooldown;
    }

    public virtual void DefirnirStatus()
    {
        skillRange = pc.currentStatus.rangeSkill;
        skillDmg = pc.currentStatus.danoSkill;
        cooldown = pc.currentStatus.cooldownSkill + cooldownBase;
    }

    public virtual bool PodeUsarSkill()
    {
        return Time.time >= ultimoUso + cooldown;
    }

    public void tentaUsar()
    {
        if (!PodeUsarSkill())
        {
        return;
        }

        bool conseguiuUsuar = TentaUsarSkill();

        if (conseguiuUsuar)
        {
            ultimoUso = Time.time;
        }
    }

    protected abstract bool TentaUsarSkill();
}