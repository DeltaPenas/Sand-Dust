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
    public float ultimoUso;
    public int stacksAtual;
    public int maxStacks;

    protected Transform pontoSkill;
    public PlayerController pc;
    public PlayerVida pv;

    protected virtual void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        pv = GetComponentInParent<PlayerVida>();

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
        cooldown = pc.currentStatus.cooldownSkill;
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