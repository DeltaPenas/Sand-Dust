using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public float skillRange;
    public float skillDmg;
    public float cooldown;
    public float ultimoUso;
    

    
    

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
