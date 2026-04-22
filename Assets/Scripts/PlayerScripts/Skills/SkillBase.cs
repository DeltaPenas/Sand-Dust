using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public float skillRange;
    public float skillDmg;
    public float cooldown;
    public float ultimoUso;
    public int stacksAtual;
    public int maxStacks;
    public WepAtaque wp;
    public PlayerController pc;



    public void Start()
    {

        pc = GetComponent<PlayerController>();
        DefirnirStatus();
        ultimoUso -= cooldown;
    }

    public virtual void DefirnirStatus()
    {
        skillRange = pc.currentStatus.rangeSkill;
        skillDmg = pc.currentStatus.danoSkill;
        cooldown = pc.currentStatus.cooldownSkill;
    }
    public virtual bool podeUsar()
    {
        
        if(Time.time < ultimoUso + cooldown) return false;

        return true;
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