using UnityEngine;

public abstract class UltBase : MonoBehaviour
{
    public float ultRange;
    public float ultDmg;
    public float ultCooldown;
    public float ultimoUsoUlt;
    protected PlayerController pc;

    

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        DefirnirStatus();
        ultimoUsoUlt = -ultCooldown;
    }

    public virtual void DefirnirStatus()
    {
        ultRange = pc.currentStatus.rangeUlt;
        ultDmg = pc.currentStatus.danoUlt;
        ultCooldown = pc.currentStatus.cooldownUlt;
    }

    public virtual bool podeUsarUlt()
    {
        return Time.time >= ultimoUsoUlt + ultCooldown;
    }

    public void tentaUsar()
    {
        if (podeUsarUlt())
        {
            
            bool conseguiuUsar = tentaUsarUlt();

            
            if (conseguiuUsar)
            {
                ultimoUsoUlt = Time.time;
            }
        }
    }

    
    protected abstract bool tentaUsarUlt();
}