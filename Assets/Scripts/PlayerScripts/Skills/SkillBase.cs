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
    protected PlayerController pc;

    protected virtual void Start()
    {
        pc = GetComponentInParent<PlayerController>();

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

    public virtual bool podeUsar()
    {
        return Time.time >= ultimoUso + cooldown;
    }

    public void tentaUsar()
    {
        if (podeUsar())
        {
            useSkill();
            ultimoUso = Time.time;
        }
    }

    protected abstract void useSkill();
}